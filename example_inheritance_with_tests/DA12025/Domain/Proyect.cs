namespace Domain;

public class Proyect
{
    private string _name;
    private decimal _budget;
    private DateTime _startDate;
    private List<Person> _staffList;

    public string Name
    {
        get => _name;
        set
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Name cannot be null or empty");
            }

            _name = value;
        }
    }

    public decimal Budget
    {
        get => _budget;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Budget cannot be less or equal to zero");
            }

            _budget = value;
        }
    }

    public DateTime StartDate
    {
        get => _startDate;
        set
        {
            if (value > DateTime.Now)
            {
                throw new ArgumentException("Start date must be earlier than today");
            }

            _startDate = value;
        }
    }

    public List<Person> StaffList
    {
        get => _staffList;
    }

    public Proyect(string name, decimal budget, DateTime startDate)
    {
        Name = name;
        Budget = budget;
        StartDate = startDate;
        _staffList = new List<Person>();
    }

    public void AddContributors(Person person)
    {
        _staffList.Add(person);
    }

    public void ShowContributorsSalary()
    {
        if (StaffList.Count < 1)
        {
            throw new ArgumentException("At least one contributor is required on the proyect");
        }

        Console.WriteLine($"Proyect: {Name}, Budget: ${Budget}");

        foreach (var person in StaffList)
        {
            Console.WriteLine($"{person.Name} has a salary of: ${person.CalculateSalary()}");
        }
    }
}