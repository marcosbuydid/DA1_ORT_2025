using Domain;

namespace ConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Type your name: ");
        string name = Console.ReadLine();

        Console.Write("Type your birthday (yyyy-MM-dd): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime birthday))
        {
            Person person = new Person(name, birthday);
            
            Console.WriteLine($"\nHello, {person.Name}!");
            Console.WriteLine($"You born on {person.GetWeekDayOfBorn()}.");
            Console.WriteLine($"Your are {person.CalculateAge()} years old.");
        }
        else
        {
            Console.WriteLine("Invalid date format. Run the program again.");
        }
    }
}