namespace Domain;

public class Student : Person
{
    private string _career;

    public string Career
    {
        get => _career;
        set { _career = value; }
    }

    public Student(int id, string name, string career) : base(id, name)
    {
        Career = career;
    }
}