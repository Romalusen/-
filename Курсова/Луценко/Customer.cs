using System;
using System.Collections.Generic;
// Якщо ви плануєте використовувати [JsonConstructor] або інші атрибути серіалізації,
// може знадобитися: using System.Text.Json.Serialization;

namespace Луценко
{
    public class Customer
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public List<Order> OrderHistory { get; set; } // Має бути public set для десеріалізації з ReferenceHandler

        // Публічний конструктор без параметрів
        public Customer()
        {
            OrderHistory = new List<Order>();
            FullName = string.Empty;
            PhoneNumber = string.Empty;
        }

        public Customer(string fullName, string phoneNumber) : this() // Викликаємо конструктор без параметрів
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentException("ПІБ клієнта не може бути порожнім.");
            }
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentException("Номер телефону клієнта не може бути порожнім.");
            }
            FullName = fullName;
            PhoneNumber = phoneNumber;
        }

        public void AddOrderToHistory(Order order)
        {
            // Перевірка, щоб уникнути додавання того самого замовлення двічі або null
            if (order != null && !OrderHistory.Contains(order))
            {
                // Переконуємося, що замовлення належить цьому клієнту (або встановлюємо)
                if (order.Customer == null) order.Customer = this;
                else if (order.Customer != this)
                {
                    // Можливо, тут варто кинути виняток або залогувати, 
                    // бо це дивна ситуація: намагаємося додати чуже замовлення до історії клієнта
                    Console.WriteLine($"Попередження: Спроба додати замовлення {order.OrderNumber} клієнта {order.Customer.FullName} до історії іншого клієнта {this.FullName}.");
                    // return; // Якщо не дозволяти
                }
                OrderHistory.Add(order);
            }
        }

        public List<Order> ViewOrderHistory()
        {
            return new List<Order>(OrderHistory);
        }

        public override string ToString()
        {
            return $"{FullName} ({PhoneNumber})";
        }
    }
}