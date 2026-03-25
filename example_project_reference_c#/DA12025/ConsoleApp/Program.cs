using System.Globalization;
using Domain;

namespace ConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Type your name: ");
        string? inputName = Console.ReadLine();

        Console.Write("Type your birthday (yyyy-MM-dd): ");
        string? inputBirthday = Console.ReadLine();

        if (!DateTime.TryParseExact(inputBirthday, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime birthday) || String.IsNullOrEmpty(inputName))
        {
            Console.WriteLine("Input name, birthday or both are invalid. Run the program again.");
        }
        else
        {
            Person person = new Person(inputName, birthday);

            Console.WriteLine($"\nHello, {person.Name}!");
            Console.WriteLine($"You born on {person.GetWeekDayOfBorn()}.");
            Console.WriteLine($"Your are {person.CalculateAge()} years old.");
        }
    }
}