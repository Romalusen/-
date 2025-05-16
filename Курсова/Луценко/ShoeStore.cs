using System;
using System.Collections.Generic;
using System.Linq;

namespace Луценко
{
    public class ShoeStore
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactInfo { get; set; }
        public List<Shoe> Inventory { get; set; }
        public List<Order> Orders { get; set; }       
        public List<Customer> Customers { get; set; } 

        public ShoeStore() // Параметрлес конструктор для JSON десеріалізатора
        {
            Inventory = new List<Shoe>();
            Orders = new List<Order>();
            Customers = new List<Customer>();
        }

        public ShoeStore(string name, string address, string contactInfo) : this() // Викликаємо конструктор без параметрів
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Назва магазину не може бути порожньою.");

            Name = name;
            Address = address;
            ContactInfo = contactInfo;
        }

        public void AddShoe(Shoe shoe)
        {
            if (shoe != null)
            {
                Inventory.Add(shoe);
                // Console.WriteLine($"Товар '{shoe}' додано до асортименту магазину '{Name}'.");
            }
        }

        public void AddShoe(string model, string brand, int size, decimal price, int stock, string type)
        {
            try
            {
                AddShoe(new Shoe(model, brand, size, price, stock, type));
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка створення товару: {ex.Message}");
            }
        }

        public List<Shoe> ViewInventory()
        {
            return new List<Shoe>(Inventory ?? new List<Shoe>());
        }

        public List<Shoe> SortShoesByNameAscending()
        {
            return (Inventory ?? new List<Shoe>()).OrderBy(shoe => shoe.Model).ToList();
        }

        public List<Shoe> SortShoesByNameDescending()
        {
            return (Inventory ?? new List<Shoe>()).OrderByDescending(shoe => shoe.Model).ToList();
        }

        public List<Shoe> FilterShoesInStock()
        {
            return (Inventory ?? new List<Shoe>()).Where(shoe => shoe.Stock > 0).ToList();
        }

        public List<Shoe> FilterShoesOutOfStock()
        {
            return (Inventory ?? new List<Shoe>()).Where(shoe => shoe.Stock == 0).ToList();
        }

        public List<Shoe> FilterShoes(string brand = null, string type = null, int? size = null)
        {
            IEnumerable<Shoe> filteredShoes = Inventory?.AsEnumerable() ?? Enumerable.Empty<Shoe>();

            if (!string.IsNullOrWhiteSpace(brand))
            {
                filteredShoes = filteredShoes.Where(s => s.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrWhiteSpace(type))
            {
                filteredShoes = filteredShoes.Where(s => s.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
            }
            if (size.HasValue && size > 0)
            {
                filteredShoes = filteredShoes.Where(s => s.Size == size.Value);
            }
            return filteredShoes.ToList();
        }
        public List<Shoe> SortShoesBySizeAscending()
        {
            return (Inventory ?? new List<Shoe>()).OrderBy(shoe => shoe.Size).ToList();
        }

        public List<Shoe> SortShoesBySizeDescending()
        {
            return (Inventory ?? new List<Shoe>()).OrderByDescending(shoe => shoe.Size).ToList();
        }

        public bool ProcessOrder(string orderNumber, OrderStatus newStatus)
        {
            Order orderToUpdate = Orders.FirstOrDefault(o => o.OrderNumber.Equals(orderNumber, StringComparison.OrdinalIgnoreCase));
            if (orderToUpdate == null)
            {
                Console.WriteLine($"Помилка: Замовлення з номером '{orderNumber}' не знайдено.");
                return false;
            }

            if (newStatus == OrderStatus.Cancelled && orderToUpdate.Status != OrderStatus.Cancelled)
            {
                Console.WriteLine($"Скасування замовлення {orderNumber}. Повернення товарів на склад...");
                foreach (var item in orderToUpdate.Items)
                {
                    var inventoryItem = Inventory.FirstOrDefault(invShoe =>
                        invShoe.Model == item.Model &&
                        invShoe.Brand == item.Brand &&
                        invShoe.Size == item.Size);
                    if (inventoryItem != null) inventoryItem.UpdateStock(1); // Припускаємо, що кількість в замовленні була 1 для кожного унікального Shoe
                    else Console.WriteLine($"Попередження: Товар {item} з скасованого замовлення {orderNumber} не знайдено в інвентарі.");
                }
            }
            orderToUpdate.UpdateStatus(newStatus);
            return true;
        }


        public Customer AddCustomer(string fullName, string phoneNumber)
        {
            var existingCustomer = Customers.FirstOrDefault(c => c.PhoneNumber.Equals(phoneNumber, StringComparison.OrdinalIgnoreCase));
            if (existingCustomer != null)
            {
                Console.WriteLine($"Клієнт з номером {phoneNumber} вже існує.");
                return existingCustomer;
            }
            try
            {
                Customer newCustomer = new Customer(fullName, phoneNumber);
                Customers.Add(newCustomer);
                Console.WriteLine($"Клієнта '{newCustomer}' додано.");
                return newCustomer;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка створення клієнта: {ex.Message}");
                return null;
            }
        }
        public List<string> GetUniqueBrands()
        {
            if (Inventory == null || !Inventory.Any())
            {
                return new List<string>();
            }
            return Inventory.Select(shoe => shoe.Brand).Where(brand => !string.IsNullOrEmpty(brand)).Distinct().OrderBy(brand => brand).ToList();
        }


        public Order CreateOrder(Customer customer, List<Shoe> selectedShoes, string deliveryAddress, string comments) // Забрав storeName, бо він є в this.Name
        {
            if (customer == null || selectedShoes == null || !selectedShoes.Any())
            {
                Console.WriteLine("Помилка створення замовлення: Не вказано клієнта або товари.");
                return null;
            }

            var itemsToProcessForStock = selectedShoes
       .GroupBy(s => new { s.Model, s.Brand, s.Size })
       .Select(g => new {
           ShoeKeyForLookup = g.Key, // Збережемо ключ для виведення, якщо ShoeInInventory буде null
           ShoeInInventory = Inventory.FirstOrDefault(inv => inv.Model == g.Key.Model && inv.Brand == g.Key.Brand && inv.Size == g.Key.Size),
           RequestedCount = g.Count()
       })
       .ToList();

            foreach (var itemEntry in itemsToProcessForStock)
            {
                if (itemEntry.ShoeInInventory == null)
                {
                    // Використовуємо ShoeKeyForLookup для отримання Brand та Model
                    Console.WriteLine($"Помилка: Товар '{itemEntry.ShoeKeyForLookup.Brand} {itemEntry.ShoeKeyForLookup.Model}' (Розмір: {itemEntry.ShoeKeyForLookup.Size}) не знайдено в асортименті.");
                    return null;
                }
                if (itemEntry.ShoeInInventory.Stock < itemEntry.RequestedCount)
                {
                    Console.WriteLine($"Помилка: Недостатньо товару '{itemEntry.ShoeInInventory}'. В наявності: {itemEntry.ShoeInInventory.Stock}, Замовлено: {itemEntry.RequestedCount}");
                    return null;
                }
            }

            Order newOrder;
            try
            {
                // Передаємо this.Name як назву магазину
                newOrder = new Order(customer, new List<Shoe>(selectedShoes), this.Name, deliveryAddress, comments);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка створення об'єкту Order: {ex.Message}");
                return null;
            }

            // Якщо "Згода" віднімає товар, то тут не списуємо, а робимо це після Callback від бота.
            // Поки що залишимо списання тут для простоти, а статус замовлення буде Pending.
            // При "Відхилити" (якщо буде реалізовано) треба буде повертати товар.
            List<KeyValuePair<Shoe, int>> updatedItemsForRollback = new List<KeyValuePair<Shoe, int>>();
            bool stockUpdateSuccess = true;
            foreach (var itemEntry in itemsToProcessForStock)
            {
                if (!itemEntry.ShoeInInventory.UpdateStock(-itemEntry.RequestedCount))
                {
                    stockUpdateSuccess = false;
                    Console.WriteLine($"Критична помилка: Не вдалося списати {itemEntry.RequestedCount} од. товару {itemEntry.ShoeInInventory}!");
                    break;
                }
                updatedItemsForRollback.Add(new KeyValuePair<Shoe, int>(itemEntry.ShoeInInventory, itemEntry.RequestedCount));
            }

            if (!stockUpdateSuccess)
            {
                foreach (var pair in updatedItemsForRollback) { pair.Key.UpdateStock(pair.Value); }
                Console.WriteLine($"Замовлення {newOrder.OrderNumber} НЕ створено через помилку оновлення залишків.");
                return null;
            }

            this.Orders.Add(newOrder);
            customer.AddOrderToHistory(newOrder);
            Console.WriteLine($"Замовлення {newOrder.OrderNumber} успішно створено для клієнта {customer.FullName} в магазині {this.Name}.");
            return newOrder;
        }
    }
}