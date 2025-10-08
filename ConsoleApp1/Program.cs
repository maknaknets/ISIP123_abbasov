  using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<TextStatistics> statisticsList = new List<TextStatistics>();

        while (true)
        {
            Console.WriteLine("Введите текст длиной минимум 100 символов:");
            string inputText = Console.ReadLine();

            if (inputText.Length >= 100)
            {
                TextStatistics stats = ProcessText(inputText);

                statisticsList.Add(stats);

                DisplayStats(stats);

                Console.WriteLine("\nХотите обработать ещё один текст? (Да/Нет)");
                string answer = Console.ReadLine().ToLower();
                if (answer != "да")
                    break;
            }
            else
            {
                Console.WriteLine("Ошибка: введённый текст меньше 100 символов.");
            }
        }
    }
}
