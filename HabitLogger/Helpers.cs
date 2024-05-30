using System.Globalization;

namespace HabitLogger
{
    internal static class Helpers
    {
        internal static string GetHabitDate(string message)
        {
            Console.Write(message);
            string dateInput = Console.ReadLine();
            while (!DateTime.TryParseExact(dateInput, "MM/dd/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                Console.WriteLine("Invalid date format.");
                Console.Write(message);
                dateInput = Console.ReadLine();
            }
            return dateInput;
        }

        internal static int GetHabitQuantity(string message)
        {
            Console.Write(message);
            string quantityInput = Console.ReadLine();
            while (!Int32.TryParse(quantityInput, out _) || Convert.ToInt32(quantityInput) < 0)
            {
                Console.WriteLine("Quantity must be greater than zero.");
                Console.Write(message);
                quantityInput = Console.ReadLine();
            }
            int quantityFinal = Convert.ToInt32(quantityInput);
            return quantityFinal;
        }

        internal static string GetHabitName(string message)
        {
            Console.Write(message);
            string nameInput = Console.ReadLine();
            while (string.IsNullOrEmpty(nameInput))
            {
                Console.WriteLine("Habit name cannot be empty.");
                Console.Write(message);
                nameInput = Console.ReadLine();
            }
            return nameInput;
        }

        internal static string GetHabitUnit(string message)
        {
            Console.Write(message);
            string unitInput = Console.ReadLine();
            while (string.IsNullOrEmpty(unitInput))
            {
                Console.WriteLine("Entry cannot be empty.");
                Console.Write(message);
                unitInput = Console.ReadLine();
            }
            return unitInput;
        }

        internal static void PressEnter()
        {
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
            } while (key.Key != ConsoleKey.Enter);
        }

        internal static object GetHabitById(string message)
        {
            Console.Write(message);
            string idInput = Console.ReadLine();
            while (!Int32.TryParse(idInput, out _) || Convert.ToInt32(idInput) < 0)
            {
                Console.WriteLine("Quantity must be greater than zero.");
                Console.Write(message);
                idInput = Console.ReadLine();
            }
            int idFinal = Convert.ToInt32(idInput);
            return idFinal;
        }
    }
}
