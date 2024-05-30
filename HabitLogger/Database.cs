using Microsoft.Data.Sqlite;
using System.Globalization;

namespace HabitLogger
{
    internal static class Database
    {
        static string connectionString = @"Data Source=habitlog.db";

        internal static void CreateTable()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "CREATE TABLE IF NOT EXISTS Habits (Id INTEGER PRIMARY KEY AUTOINCREMENT, Date TEXT, Name TEXT, Unit TEXT, Quantity INTEGER)";
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        internal static void AddRecord()
        {
            Console.Clear();
            string habitDate = Helpers.GetHabitDate("Enter date (format: mm/dd/yyyy): ");
            string habitName = Helpers.GetHabitName("Enter habit name: ");
            string habitUnitOfMeasurement = Helpers.GetHabitUnit("Enter habit unit of measurement (cigarettes smoked, cans of soda, etc.): ");
            int habitQuantity = Helpers.GetHabitQuantity("Enter quantity: ");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"INSERT INTO Habits(Date, Name, Unit, Quantity) VALUES('{habitDate}', '{habitName}', '{habitUnitOfMeasurement}', {habitQuantity})";
                command.ExecuteNonQuery();
                connection.Close();
            }

            Menu.ShowMenu();
        }

        internal static void UpdateRecord()
        {
            ViewRecords();
            var recordId = Helpers.GetHabitById("Enter ID of record to update: ");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var checkCommand = connection.CreateCommand();
                checkCommand.CommandText = $"SELECT EXISTS(SELECT 1 FROM Habits WHERE Id = {recordId}";
                int checkQuery = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (checkQuery == 0)
                {
                    Console.WriteLine($"Record with ID {recordId} does not exist");
                    connection.Close();
                    UpdateRecord();
                }
                int newQuantity = Helpers.GetHabitQuantity("Enter new quantity: ");
                var command = connection.CreateCommand();
                command.ExecuteNonQuery();
                connection.Close();
            }

            Menu.ShowMenu();
        }

        internal static void DeleteRecord()
        {
            ViewRecords();
            var recordId = Helpers.GetHabitById("Enter ID of record to delete: ");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"DELETE FROM Habits WHERE Id = {recordId}";
                command.ExecuteNonQuery();
                connection.Close();
            }

            Menu.ShowMenu();
        }

        internal static void ViewRecords()
        {
            Console.Clear();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Habits";
                List<Habit> records = new List<Habit>();
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        records.Add(new Habit
                        {
                            Id = reader.GetInt32(0),
                            Date = DateTime.ParseExact(reader.GetString(1), "MM/dd/yyyy", new CultureInfo("en-US")),
                            Name = reader.GetString(2),
                            UnitOfMeasurement = reader.GetString(3),
                            Quantity = reader.GetInt32(4),
                        });
                    }
                }
                else
                {
                    Console.WriteLine("Nothing recorded to database. Press enter to return to menu.");
                    Helpers.PressEnter();
                    Menu.ShowMenu();
                }
                connection.Close();

                Console.WriteLine("-------------------------------------------------------------------");
                foreach (var record in records)
                {
                    Console.WriteLine($"{record.Id}. {record.Date.ToString("MM/dd/yyyy")} | {record.Name} - {record.UnitOfMeasurement}: {record.Quantity}");
                }
                Console.WriteLine("-------------------------------------------------------------------");
            }
        }
    }
}
