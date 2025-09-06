using System.Text.RegularExpressions;

namespace Domain;

public class User
{
    private string _name;
    private string _email;
    private string _password;

    public string Name
    {
        get => _name;
        set
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException("username cannot be empty");
            }

            _name = value;
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException("email cannot be empty");
            }

            string validEmailPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                       + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
            if (!Regex.IsMatch(value, validEmailPattern))
            {
                throw new ArgumentException("email format is invalid");
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
                {
                    throw new ArgumentException("password cannot be empty");
                }
            }

            if (value.Length < 8)
            {
                {
                    throw new ArgumentException("password must have at least 8 characters");
                }
            }

            string validSpecialCharsPattern = @"[!@#$%^&*._]";
            if (!Regex.IsMatch(value, validSpecialCharsPattern))
            {
                throw new ArgumentException("password must have at least one special char");
            }

            _password = value;
        }
    }

    public User(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
}