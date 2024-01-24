using System;
using System.Collections.Generic;
using TODO.Models;

namespace TODO.Views
{
    internal class Views
    {
        public static void StartView(ItemList itemList)
        {
            string header = "\nPlease Select an Options from Below\n\n";
            string footer = "\n\nPress Up & Down to Navigate.\nPress CTRL + X to Exit Menu.\nPress Enter to Continue...";
            List<Option> options = new List<Option>
            {
                new Option("Add", () => AddView(itemList)),
                new Option("Show", () => ListView(itemList)),
            };

            // Set the default index of the selected item to be the first
            int index = 0;

            // Write the menu out
            Print.Menu(options, options[index], header, footer);

            // Store key info in here
            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey(true);

                // Handle each key input (down arrow will write the menu again with a different selected item)
                if (keyinfo.Key == ConsoleKey.DownArrow)
                {
                    if (index + 1 < options.Count)
                    {
                        index++;
                        Print.Menu(options, options[index], header, footer);
                    }
                }
                if (keyinfo.Key == ConsoleKey.UpArrow)
                {
                    if (index - 1 >= 0)
                    {
                        index--;
                        Print.Menu(options, options[index], header, footer);
                    }
                }
                // Handle different action for the option
                if (keyinfo.Key == ConsoleKey.Enter)
                {
                    options[index].Selected.Invoke();
                    index = 0;
                    Print.Menu(options, options[index], header, footer);
                }
            }
            while (!IsExitClicked(keyinfo));
        }

        private static bool IsExitClicked(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control) && keyInfo.Key == ConsoleKey.X) { return true; }
            else return false;
        }

        public static void AddView(ItemList itemList)
        {
            Console.Clear();
            Item item = new Item();
            Console.WriteLine("\n\n\nPlease Enter New Item Details...\n");
            Console.Write("Title                                   : ");
            do
            {
                try
                {
                    item.Title = Console.ReadLine();
                    break;
                }
                catch { Console.WriteLine("Title must be less than or equal to 20 characters"); }
            } while (true);

            Console.Write("Description                             : ");
            do
            {
                try
                {
                    item.Description = Console.ReadLine();
                    break;
                }
                catch { Console.WriteLine("Description must be less than or equal to 100 characters"); }
            } while (true);
            Console.Write("Expected Date of Completion(DD-MM-YYYY) : ");
            do
            {
                try
                {
                    item.ExpectedEndDate = DateTime.ParseExact(Console.ReadLine(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    
                    if(item.ExpectedEndDate < item.Startdate) { throw new Exception(); }
                    break;
                }
                catch (Exception e) { Console.WriteLine(e.ToString() + "\n\nPlease re-enter a valid date in correct format"); continue; }
            } while (true);

            Console.Write("Priority(H/L)                           : ");
            do
            {
                string p = Console.ReadLine();
                if (p == "H" || p == "h") { item.Priority = Priority.HIGH; break; }
                else if (p == "L" || p == "l") { item.Priority = Priority.LOW; break; }
                else { Console.WriteLine("Please Enter priority in Correct format(H - High, L - Low):"); }
            } while (true);


            itemList.Add(item);
            Console.WriteLine("\nItem Added Successfully");
            Console.WriteLine("\nPress any key to continue...");
            ConsoleKeyInfo k = Console.ReadKey();
        }
        public static void ListView(ItemList itemList)
        {
            Console.Clear();
            string header = "                     Item List                        \n\n| Sl No |        Title        | Priority |  Status  |";
            string footer = "\nPress Enter to Expand...";
            List<Item> items = itemList.GetAll();
            int selectedIndex = 0;
            // Write the menu out
            Print.List(items, selectedIndex, header, footer);

            // Store key info in here
            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey(true);

                // Handle each key input (down arrow will write the menu again with a different selected item)
                if (keyinfo.Key == ConsoleKey.DownArrow)
                {
                    if (selectedIndex + 1 < items.Count)
                    {
                        selectedIndex++;
                        Print.List(items, selectedIndex, header, footer);
                    }
                }
                if (keyinfo.Key == ConsoleKey.UpArrow)
                {
                    if (selectedIndex - 1 >= 0)
                    {
                        selectedIndex--;
                        Print.List(items, selectedIndex, header, footer);
                    }
                }
                // Handle different action for the option
                if (keyinfo.Key == ConsoleKey.Enter && items.Count > 0)
                {
                    SingleItemView(itemList, items[selectedIndex]);
                    Print.List(items, selectedIndex, header, footer);
                }

                if (keyinfo.Key == ConsoleKey.U && keyinfo.Modifiers.HasFlag(ConsoleModifiers.Control))
                {

                }
            }
            while (keyinfo.Key != ConsoleKey.X);
        }

        private static void SingleItemView(ItemList items, Item item)
        {
            ConsoleKeyInfo keyInfo;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Press to X to go Back...\n\n");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n\nSelected Item Details...\n\n");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\tID                                      : " + item.Id);
                Console.WriteLine("\tTitle                                  : " + item.Title);
                Console.WriteLine("\tDescription                             : " + item.Description);
                Console.WriteLine("\tStart Date                              : " + item.Startdate);
                Console.WriteLine("\tExpected Date of Completion(DD-MM-YYYY) : " + item.ExpectedEndDate.ToString());
                Console.WriteLine("\tPriority(H/L)                           : " + item.Priority);
                Console.Write("\tStatus                                  : ");
                if ((DateTime.Now - item.ExpectedEndDate).TotalSeconds > 0) Console.WriteLine(Status.OVERDUE);
                else Console.WriteLine(Status.PENDING);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n\nOptions:\n");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\tCTRL + T : Update Title");
                Console.WriteLine("\tCTRL + D : Update Description");
                Console.WriteLine("\tCTRL + E : Update Expected Date of Completion");
                Console.WriteLine("\tCTRL + P : Update Priority");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\tCTRL + Enter : Mark as Completed");
                Console.ForegroundColor = ConsoleColor.White;

                keyInfo = Console.ReadKey();
                if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control) && keyInfo.Key == ConsoleKey.T)
                {
                    Console.Clear();
                    Console.WriteLine("\n\n\nPrevious Title : " + item.Title);
                    Console.Write("New Title  :");
                    do
                    {
                        try
                        {
                            item.Title = Console.ReadLine();
                            break;
                        }
                        catch { Console.WriteLine("Title must be less than or equal to 20 characters"); }
                    } while (true);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nTitle Changed Succesfully!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\n\nPress any key to Continue...");
                    Console.ReadKey();
                }
                if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control) && keyInfo.Key == ConsoleKey.D)
                {
                    Console.Clear();
                    Console.WriteLine("\n\n\nPrevious Description : " + item.Description);
                    Console.Write("New Description  :");
                    do
                    {
                        try
                        {
                            item.Description = Console.ReadLine();
                            break;
                        }
                        catch { Console.WriteLine("Description must be less than or equal to 100 characters"); }
                    } while (true);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nDescription Changed Succesfully!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\n\nPress any key to Continue...");
                    Console.ReadKey();
                }
                if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control) && keyInfo.Key == ConsoleKey.E)
                {
                    Console.Clear();
                    Console.WriteLine("\n\n\nPrevious Expected End Of date : " + item.ExpectedEndDate);
                    Console.Write("New Expected End Of date      :");
                    do
                    {
                        try
                        {
                            item.ExpectedEndDate = DateTime.ParseExact(Console.ReadLine(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            if ((item.ExpectedEndDate - item.Startdate).TotalSeconds < 0) { throw new Exception(); }
                            break;
                        }
                        catch (Exception e) { Console.WriteLine(e.ToString() + "\n\nPlease re-enter a valid date in correct format"); continue; }
                    } while (true);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nExpected End of Date Changed Succesfully!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\n\nPress any key to Continue...");
                    Console.ReadKey();
                }
                if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control) && keyInfo.Key == ConsoleKey.P)
                {
                    Console.Clear();
                    Console.WriteLine("\n\n\nPrevious Priority(H/L) : " + item.Priority);
                    Console.Write("\nNew Priority(H/L)   :");
                    do
                    {
                        string p = Console.ReadLine();
                        if (p == "H" || p == "h") { item.Priority = Priority.HIGH; break; }
                        else if (p == "L" || p == "l") { item.Priority = Priority.LOW; break; }
                        else { Console.WriteLine("Please Enter priority in Correct format(H - High, L - Low):"); }
                    } while (true);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nPriority Changed Succesfully!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\n\nPress any key to Continue...");
                    Console.ReadKey();
                }
                if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control) && keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Console.WriteLine("Do you want mark it as Completed (Y/N)\n");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\t*This action can't be reversed!\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    do
                    {
                        string input = Console.ReadLine();
                        if (input == "Y" || input == "y")
                        {
                            items.Remove(item);
                            break;
                        }
                        if (input == "N" || input == "n")
                        {
                            break;
                        }
                    } while (true);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n\n\nAction Completed successfully!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\n\nPress any key to Continue...");
                    Console.ReadKey();
                    break;
                }
            } while (keyInfo.Key != ConsoleKey.X);
        }
    }
}
