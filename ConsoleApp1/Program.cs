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
