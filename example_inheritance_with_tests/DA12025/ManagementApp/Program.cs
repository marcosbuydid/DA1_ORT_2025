using Domain;

namespace ManagementApp;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Person employee = new Employee("John", 30, 3000, 1000);
            Person contractor = new Contractor("Anna", 25, 1000, 50, 160);

            Proyect proyect = new Proyect("Software Proyect", 50000, new DateTime(2025, 08, 01));

            proyect.AddContributors(employee);
            proyect.AddContributors(contractor);

            proyect.ShowContributorsSalary();
        }
        catch (ArgumentException e)
        {
            Console.WriteLine($"An unexpected error ocurred: {e.Message}");
        }
    }
}