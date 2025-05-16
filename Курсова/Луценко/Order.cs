using System;
using System.Collections.Generic;
using System.Linq;


namespace Луценко
{
    public enum OrderStatus
    {
        Pending,
        Processing,
        Completed,
        Cancelled
    }

    public class Order
    {
        // Всі ці властивості мають мати public get; public set;
        // для коректної роботи ReferenceHandler.Preserve при десеріалізації
        public string OrderNumber { get; set; }
        public Customer Customer { get; set; } // Ключове поле для циклу
        public List<Shoe> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
        public string StoreName { get; set; }
        public string DeliveryAddress { get; set; }
        public string Comments { get; set; }

        // Публічний конструктор без параметрів ОБОВ'ЯЗКОВИЙ для десеріалізатора
        public Order()
        {
            Items = new List<Shoe>(); // Ініціалізація списків
            // Ініціалізація іншими значеннями за замовчуванням, якщо потрібно
            OrderNumber = string.Empty; // Або генерувати при першому доступі, якщо порожній
            Status = OrderStatus.Pending;
            OrderDate = DateTime.MinValue; // Або DateTime.UtcNow
            StoreName = string.Empty;
            DeliveryAddress = string.Empty;
            Comments = string.Empty;

        }

        // Ваш існуючий конструктор для програмного створення
        public Order(Customer customer, List<Shoe> items, string storeName, string deliveryAddress, string comments) : this() // Викликаємо конструктор без параметрів
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer), "Клієнт не може бути null.");
            if (items == null || !items.Any())
                throw new ArgumentException("Список товарів у замовленні не може бути порожнім.", nameof(items));

            OrderNumber = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper(); // Краще генерувати тут, якщо не в конструкторі без параметрів
            Customer = customer;
            Items = new List<Shoe>(items); // Створюємо копію
            Status = OrderStatus.Pending;  // Вже встановлено в конструкторі без параметрів, можна залишити для ясності
            OrderDate = DateTime.Now;      // Вже встановлено, можна залишити
            StoreName = storeName;
            DeliveryAddress = deliveryAddress;
            Comments = comments;
            CalculateTotalAmount();
        }

        public void UpdateStatus(OrderStatus newStatus)
        {
            Status = newStatus;
            Console.WriteLine($"Статус замовлення {OrderNumber} змінено на {Status}.");
        }

        public void CalculateTotalAmount()
        {
            TotalAmount = Items?.Sum(shoe => shoe.Price) ?? 0;
        }

        public override string ToString()
        {
            return $"Замовлення №{OrderNumber} [{OrderDate:dd.MM.yyyy HH:mm}] - Клієнт: {Customer?.FullName}, Сума: {TotalAmount:C}, Статус: {Status}";
        }
    }
}