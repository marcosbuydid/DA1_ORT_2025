namespace Domain;

public class Person
{
    private string _name;
    private DateTime _birthday;

    public string Name
    {
        get => _name;

        set { _name = value; }
    }

    public DateTime Birthday
    {
        get => _birthday;

        set { _birthday = value; }
    }

    public Person(string name, DateTime birthday)
    {
        Name = name;
        Birthday = birthday;
    }

    public int CalculateAge()
    {
        DateTime today = DateTime.Today;
        int age = today.Year - _birthday.Year;
        if (_birthday > today.AddYears(-age))
        {
            age--;
        }

        return age;
    }

    public string GetWeekDayOfBorn()
    {
        return _birthday.DayOfWeek.ToString();
    }
}