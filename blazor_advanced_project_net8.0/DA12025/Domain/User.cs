namespace Domain;

public class User
{
    private int? _id;
    private string _name;
    private string _lastname;
    private string _email;
    private string _password;
    private string _role;

    public int? Id
    {
        get => _id;
        set { _id = value; }
    }

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

    public string LastName
    {
        get => _lastname;
        set
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException("LastName cannot be null or empty");
            }

            _lastname = value;
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Email cannot be null or empty");
            }

            _email = value;
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Password cannot be null or empty");
            }

            _password = value;
        }
    }

    public string Role
    {
        get => _role;
        set
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Role cannot be null or empty");
            }

            _role = value;
        }
    }

    public User() {}

    public User(int? id, string name, string lastname, string email, string password, string role)
    {
        Id = id;
        Name = name;
        LastName = lastname;
        Email = email;
        Password = password;
        Role = role;
    }
}