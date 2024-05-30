namespace HabitLogger
{
    internal static class Menu
    {
        internal static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine($"Hello. Today is {DateTime.Now.ToString("D")}");
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine(@"Select action
0 - Close application
1 - View all records
2 - Add new record
3 - Delete a record
4 - Update a record");
            Console.WriteLine("-------------------------------------------------------------------");

            var menuSelection = Console.ReadLine();
            switch (menuSelection.Trim())
            {
                case "0":
                    Environment.Exit(1);
                    break;

                case "1":
                    Database.ViewRecords();
                    break;

                case "2":
                    Database.AddRecord();
                    break;

                case "3":
                    Database.DeleteRecord();
                    break;

                case "4":
                    Database.UpdateRecord();
                    break;

                default:
                    Console.Clear();
                    break;
            }
        }
    }
}
