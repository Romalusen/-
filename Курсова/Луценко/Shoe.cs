using System;

namespace Луценко
{
    public class Shoe
    {
        public string Model { get; set; }
        public string Brand { get; set; }
        public int Size { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Type { get; set; }

        // Параметрлес конструктор для JSON десеріалізатора
        public Shoe() { }

        // Існуючий конструктор для програмного створення
        public Shoe(string model, string brand, int size, decimal price, int stock, string type)
        {
            if (size <= 0 || price < 0 || stock < 0)
            {
                throw new ArgumentException("Розмір, ціна та залишок не можуть бути нульовими або від'ємними.");
            }
            if (string.IsNullOrWhiteSpace(model) || string.IsNullOrWhiteSpace(brand) || string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentException("Модель, бренд та тип не можуть бути порожніми.");
            }
            Model = model;
            Brand = brand;
            Size = size;
            Price = price;
            Stock = stock;
            Type = type;
        }

        public void ChangePrice(decimal newPrice)
        {
            if (newPrice < 0)
            {
                // Розгляньте можливість кидати виняток або повертати bool для індикації успіху/невдачі
                Console.WriteLine("Помилка: Нова ціна не може бути від'ємною.");
                return;
            }
            Price = newPrice;
            Console.WriteLine($"Ціну для {Brand} {Model} (Розмір: {Size}) змінено на {Price:C}");
        }

        public bool UpdateStock(int change)
        {
            if (Stock + change < 0)
            {
                Console.WriteLine($"Помилка оновлення залишку для {Brand} {Model} (Розмір: {Size}): Недостатньо товару. Поточний залишок: {Stock}, Спроба змінити на: {change}");
                return false;
            }
            Stock += change;
            // Console.WriteLine($"Залишок для {Brand} {Model} (Розмір: {Size}) оновлено. Новий залишок: {Stock}");
            return true;
        }

        public override string ToString()
        {
            return $"{Brand} {Model} ({Type}) - Розмір: {Size}, Ціна: {Price:C}, Залишок: {Stock} шт.";
        }
    }
}