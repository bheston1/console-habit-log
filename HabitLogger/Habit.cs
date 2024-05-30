namespace HabitLogger
{
    internal class Habit
    {
        internal int Id { get; set; }
        internal DateTime Date { get; set; }
        internal string Name { get; set; }
        internal string UnitOfMeasurement {  get; set; }
        internal int Quantity {  get; set; }
    }
}
