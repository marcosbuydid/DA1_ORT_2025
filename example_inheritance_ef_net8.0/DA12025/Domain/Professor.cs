namespace Domain;

public class Professor : Person
{
    private int _salary;

    public int Salary
    {
        get => _salary;
        set { _salary = value; }
    }

    public Professor(int id, string name, int salary) : base(id, name)
    {
        Salary = salary;
    }
}