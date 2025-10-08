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
