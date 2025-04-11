namespace Domain;

public abstract class Person
{
    private string _name;
    private int _age;
    private decimal _baseSalary;

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

    public int Age
    {
        get => _age;
        set
        {
            if (value < 18)
            {
                throw new ArgumentException("Age must be at least 18");
            }

            _age = value;
        }
    }

    public decimal BaseSalary
    {
        get => _baseSalary;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Base salary cannot be zero or negative");
            }

            _baseSalary = value;
        }
    }

    public Person(string name, int age, decimal baseSalary)
    {
        Name = name;
        Age = age;
        BaseSalary = baseSalary;
    }

    public abstract decimal CalculateSalary();
}