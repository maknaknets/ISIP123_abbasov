using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement
{
    // Enum for genres
    public enum Genre
    {
        Fiction,
        NonFiction,
        Mystery,
        ScienceFiction,
        Biography
    }

    // Book class
    public class Book
    {
        private static int nextId = 1;

        public int Id { get; private set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public Genre Genre { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }

        public Book(string title, string author, Genre genre, int year, decimal price)
        {
            Id = nextId++;
            Title = title;
            Author = author;
            Genre = genre;
            Year = year;
            Price = price;
        }
        public override string ToString()
        {
            return $"ID: {Id}, Title: {Title}, Author: {Author}, Genre: {Genre}, Year: {Year}, Price: {Price:C}";
        }
    }

    class Program
    {
        static List<Book> books = new List<Book>();

        static void Main(string[] args)
        {
            // Initialize with 5 test books
            InitializeTestData();

            while (true)
            {
                DisplayMenu();
                string choice = Console.ReadLine();
                HandleChoice(choice);
            }
        }
        static void InitializeTestData()
        {
            books.Add(new Book("The Great Gatsby", "F. Scott Fitzgerald", Genre.Fiction, 1925, 10.99m));
            books.Add(new Book("1984", "George Orwell", Genre.ScienceFiction, 1949, 8.99m));
            books.Add(new Book("To Kill a Mockingbird", "Harper Lee", Genre.Fiction, 1960, 12.50m));
            books.Add(new Book("Sapiens", "Yuval Noah Harari", Genre.NonFiction, 2011, 15.00m));
            books.Add(new Book("The Da Vinci Code", "Dan Brown", Genre.Mystery, 2003, 9.99m));
            Console.WriteLine("Test data initialized with 5 books.");
        }

        static void DisplayMenu()
        {
            Console.WriteLine("\nLibrary Management System");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Delete Book by ID");
            Console.WriteLine("3. Find Books by Title");
            Console.WriteLine("4. Find Books by Author");
            Console.WriteLine("5. Find Books by Genre");
            Console.WriteLine("6. Sort Books by Title");
            Console.WriteLine("7. Sort Books by Year");
            Console.WriteLine("8. Show Most Expensive and Cheapest Book");
            Console.WriteLine("9. Group Books by Author and Show Counts");
            Console.WriteLine("10. List All Books");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");
        }
        static void HandleChoice(string choice)
        {
            try
            {
                switch (choice)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        DeleteBook();
                        break;
                    case "3":
                        FindByTitle();
                        break;
                    case "4":
                        FindByAuthor();
                        break;
                    case "5":
                        FindByGenre();
                        break;
                    case "6":
                        SortByTitle();
                        break;
                    case "7":
                        SortByYear();
                        break;
                    case "8":
                        ShowExtremePrices();
                        break;
                    case "9":
                        GroupByAuthor();
                        break;
                    case "10":
                        ListAllBooks();
                        break;
                    case "0":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        static void AddBook()
        {
            Console.Write("Enter Title: ");
            string title = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(title))
            {
                Console.WriteLine("Title cannot be empty.");
                return;
            }

            Console.Write("Enter Author: ");
            string author = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(author))
            {
                Console.WriteLine("Author cannot be empty.");
                return;
            }

            Genre genre = GetGenreFromUser();

            int year = GetValidInt("Enter Year: ", 1, DateTime.Now.Year);

            decimal price = GetValidDecimal("Enter Price: ", 0.01m);

            Book newBook = new Book(title, author, genre, year, price);
            books.Add(newBook);
            Console.WriteLine("Book added successfully.");
            Console.WriteLine(newBook);
        }

        static Genre GetGenreFromUser()
        {
            Console.WriteLine("Select Genre:");
            var genres = Enum.GetValues(typeof(Genre));
            for (int i = 0; i < genres.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {genres.GetValue(i)}");
            }
            int selection = GetValidInt("Enter number: ", 1, genres.Length);
            return (Genre)genres.GetValue(selection - 1);
        }

        static int GetValidInt(string prompt, int min, int max = int.MaxValue)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
                {
                    return value;
                }
                Console.WriteLine($"Invalid input. Must be integer between {min} and {max}.");
            }
        }
