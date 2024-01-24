using System;
using System.Collections.Generic;
using TODO.Models;
using TODO.Views;

namespace TODO
{
    internal static class Print
    {
        public static void Menu(List<Option> options, Option selectedOption, string header = "header\n", string footer = "footer\n")
        {
            Console.Clear();
            Console.WriteLine("\n\n--------------------------------------");
            Console.WriteLine("        TODO Console Application      ");
            Console.WriteLine("--------------------------------------\n");

            Console.WriteLine(header);
            foreach (Option option in options)
            {
                if (option == selectedOption)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("> ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" ");
                }

                Console.WriteLine(option.Name);
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(footer);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void List(List<Item> items, int selectedIndex, string header, string footer)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Press X To Go Back\n\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(header);
            int i = 0;
            foreach (Item item in items)
            {
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.Write("| " + item.Id);
                int n = 10000 - item.Id;
                while (n > 0)
                {
                    Console.Write(" ");
                    n /= 10;
                }
                if (item.Id != 0) Console.Write(" ");
                Console.Write("| " + item.Title);
                n = 20 - item.Title.Length;
                while (n > 0)
                {
                    Console.Write(" ");
                    n--;
                }
                Console.Write(" | ");
                if (item.Priority == Priority.HIGH) { Console.Write("  High   | "); }
                if (item.Priority == Priority.LOW) { Console.Write("  Low    | "); }
                if ((DateTime.Now - item.ExpectedEndDate).TotalSeconds > 0) Console.Write(Status.OVERDUE + "  |\n");
                else Console.Write(Status.PENDING + "  |\n");
                i++;
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(footer);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
