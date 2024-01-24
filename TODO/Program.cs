using TODO.Models;

namespace TODO
{

    class Program
    {
        private static ItemList itemList;
        static void Main(string[] args)
        {
            itemList = new ItemList();
            Views.Views.StartView(itemList);
            //Console.WriteLine("\n\n\nPress Any Key to Exit...");
            //Console.ReadKey();
        }
    }
}
