namespace Domain;

public class Contractor : Person
{
    private decimal _hourlyRate;
    private int _workedHours;

    public decimal HourlyRate
    {
        get => _hourlyRate;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Hourly rate cannot be less than 0");
            }

            _hourlyRate = value;
        }
    }

    public int WorkedHours
    {
        get => _workedHours;
        set { _workedHours = value; }
    }

    public Contractor(string name, int age, decimal baseSalary, decimal hourlyRate, int workedHours) : base(name, age,
        baseSalary)
    {
        HourlyRate = hourlyRate;
        WorkedHours = workedHours;
    }

    public override decimal CalculateSalary()
    {
        return HourlyRate * WorkedHours;
    }
}