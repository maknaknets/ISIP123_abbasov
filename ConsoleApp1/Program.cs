using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreApp
{
    // Перечисление категорий товаров
    public enum Category
    {
        Electronics,
        Clothing,
        Food,
        Books,
        HomeAppliances
    }

    // Класс для представления товара
    public class Product
    {
        private static int _nextCode = 1; // Статический счетчик для уникального кода
        public string Code { get; }
        public string Name { get; }
        public decimal Price { get; }
        public int Quantity { get; private set; }
        public bool InStock => Quantity > 0;
        public Category Category { get; }

        public Product(string name, decimal price, int quantity, Category category)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название товара не может быть пустым.");
            if (price < 0)
                throw new ArgumentException("Цена не может быть отрицательной.");
            if (quantity < 0)
                throw new ArgumentException("Количество не может быть отрицательным.");

            Code = $"1{_nextCode++:D4}"; // Формат кода: 1XXXX
            Name = name;
            Price = price;
            Quantity = quantity;
            Category = category;
        }

        // Метод для добавления товара на склад
        public void AddStock(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Количество для добавления должно быть положительным.");
            Quantity += amount;
        }

        // Метод для продажи товара
        public void Sell(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Количество для продажи должно быть положительным.");
            if (amount > Quantity)
                throw new ArgumentException("Недостаточно товара на складе.");
            Quantity -= amount;
        }

        public override string ToString()
        {
            return $"Код: {Code}, Название: {Name}, Цена: {Price:C}, Количество: {Quantity}, " +
                   $"В наличии: {(InStock ? "Да" : "Нет")}, Категория: {Category}";
        }
    }

    // Класс для управления складом
    public class Store
    {
        private readonly List<Product> _products = new List<Product>();

        // Добавление товара
        public void AddProduct(string name, decimal price, int quantity, Category category)
        {
            try
            {
                var product = new Product(name, price, quantity, category);
                _products.Add(product);
                Console.WriteLine($"Товар {name} успешно добавлен.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        // Удаление товара по коду
        public void RemoveProduct(string code)
        {
            var product = _products.FirstOrDefault(p => p.Code == code);
            if (product == null)
            {
                Console.WriteLine("Товар с таким кодом не найден.");
                return;
            }
            _products.Remove(product);
            Console.WriteLine($"Товар {product.Name} успешно удален.");
        }

        // Поставка товара
        public void SupplyProduct(string code, int quantity)
        {
            var product = _products.FirstOrDefault(p => p.Code == code);
            if (product == null)
            {
                Console.WriteLine("Товар с таким кодом не найден.");
                return;
            }
            try
            {
                product.AddStock(quantity);
                Console.WriteLine($"Поставка {quantity} единиц товара {product.Name} выполнена.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        // Продажа товара
        public void SellProduct(string code, int quantity)
        {
            var product = _products.FirstOrDefault(p => p.Code == code);
            if (product == null)
            {
                Console.WriteLine("Товар с таким кодом не найден.");
                return;
            }
            try
            {
                product.Sell(quantity);
                Console.WriteLine($"Продано {quantity} единиц товара {product.Name}.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        // Поиск товаров
        public void SearchProducts(string searchTerm = null, Category? category = null)
        {
            var results = _products.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                results = results.Where(p => p.Code.ToLower().Contains(searchTerm) ||
                                            p.Name.ToLower().Contains(searchTerm));
            }
            if (category.HasValue)
            {
                results = results.Where(p => p.Category == category.Value);
            }

            var foundProducts = results.ToList();
            if (!foundProducts.Any())
            {
                Console.WriteLine("Товары не найдены.");
                return;
            }

            Console.WriteLine("\nНайденные товары:");
            foreach (var product in foundProducts)
            {
                Console.WriteLine(product);
            }
        }
    }

    // Основной класс программы
    class Program
    {
        static void Main(string[] args)
        {
            var store = new Store();
            InitializeTestData(store);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Система учёта товаров");
                Console.WriteLine("1. Добавить товар");
                Console.WriteLine("2. Удалить товар");
                Console.WriteLine("3. Заказать поставку товара");
                Console.WriteLine("4. Продать товар");
                Console.WriteLine("5. Поиск товаров");
                Console.WriteLine("6. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddProduct(store);
                        break;
                    case "2":
                        RemoveProduct(store);
                        break;
                    case "3":
                        SupplyProduct(store);
                        break;
                    case "4":
                        SellProduct(store);
                        break;
                    case "5":
                        SearchProducts(store);
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите Enter для продолжения.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void InitializeTestData(Store store)
        {
            store.AddProduct("Смартфон", 599.99m, 10, Category.Electronics);
            store.AddProduct("Футболка", 19.99m, 50, Category.Clothing);
            store.AddProduct("Яблоки", 2.99m, 100, Category.Food);
            store.AddProduct("Книга по C#", 49.99m, 20, Category.Books);
            store.AddProduct("Микроволновка", 89.99m, 5, Category.HomeAppliances);
        }

        static void AddProduct(Store store)
        {
            Console.Write("Введите название товара: ");
            string name = Console.ReadLine();
            Console.Write("Введите цену: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Неверный формат цены.");
                Console.ReadLine();
                return;
            }
            Console.Write("Введите количество: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Неверный формат количества.");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Выберите категорию (0 - Электроника, 1 - Одежда, 2 - Еда, 3 - Книги, 4 - Бытовые товары): ");
            if (!int.TryParse(Console.ReadLine(), out int categoryIndex) || !Enum.IsDefined(typeof(Category), categoryIndex))
            {
                Console.WriteLine("Неверная категория.");
                Console.ReadLine();
                return;
            }

            store.AddProduct(name, price, quantity, (Category)categoryIndex);
            Console.WriteLine("Нажмите Enter для продолжения.");
            Console.ReadLine();
        }

        static void RemoveProduct(Store store)
        {
            Console.Write("Введите код товара для удаления: ");
            string code = Console.ReadLine();
            store.RemoveProduct(code);
            Console.WriteLine("Нажмите Enter для продолжения.");
            Console.ReadLine();
        }

        static void SupplyProduct(Store store)
        {
            Console.Write("Введите код товара: ");
            string code = Console.ReadLine();
            Console.Write("Введите количество для поставки: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Неверный формат количества.");
                Console.ReadLine();
                return;
            }
            store.SupplyProduct(code, quantity);
            Console.WriteLine("Нажмите Enter для продолжения.");
            Console.ReadLine();
        }

        static void SellProduct(Store store)
        {
            Console.Write("Введите код товара: ");
            string code = Console.ReadLine();
            Console.Write("Введите количество для продажи: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Неверный формат количества.");
                Console.ReadLine();
                return;
            }
            store.SellProduct(code, quantity);
            Console.WriteLine("Нажмите Enter для продолжения.");
            Console.ReadLine();
        }

        static void SearchProducts(Store store)
        {
            Console.Write("Введите поисковый запрос (или оставьте пустым): ");
            string searchTerm = Console.ReadLine();
            Console.Write("Введите категорию для фильтра (0 - Электроника, 1 - Одежда, 2 - Еда, 3 - Книги, 4 - Бытовые товары, или -1 для всех): ");
            Category? category = null;
            if (int.TryParse(Console.ReadLine(), out int categoryIndex) && categoryIndex >= 0 && Enum.IsDefined(typeof(Category), categoryIndex))
            {
                category = (Category)categoryIndex;
            }
            store.SearchProducts(searchTerm, category);
            Console.WriteLine("Нажмите Enter для продолжения.");
            Console.ReadLine();
        }
    }
}