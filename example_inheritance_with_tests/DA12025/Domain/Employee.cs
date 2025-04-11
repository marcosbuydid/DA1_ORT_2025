namespace Domain;

public class Employee : Person
{
    private decimal _anualBond;

    public decimal AnualBond
    {
        get => _anualBond;
        set { _anualBond = value; }
    }

    public Employee(string name, int age, decimal baseSalary, decimal anualBond) : base(name, age, baseSalary)
    {
        AnualBond = anualBond;
    }

    public override decimal CalculateSalary()
    {
        return BaseSalary + AnualBond;
    }
}