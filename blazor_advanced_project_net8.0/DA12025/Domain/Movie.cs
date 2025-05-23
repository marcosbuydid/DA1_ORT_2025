namespace Domain;

public class Movie
{
    private int? _id;
    private string _title;
    private string _director;
    private DateTime _releaseDate;
    private int _budget;

    public int? Id
    {
        get => _id;
        set { _id = value; }
    }

    public string Title
    {
        get => _title;

        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Title cannot be null or empty");
            }

            _title = value;
        }
    }

    public string Director
    {
        get => _director;

        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Director cannot be null or empty");
            }

            _director = value;
        }
    }

    public DateTime ReleaseDate
    {
        get => _releaseDate;
        set { _releaseDate = value; }
    }

    public int Budget
    {
        get => _budget;

        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Budget must be a positive number");
            }

            _budget = value;
        }
    }

    public Movie() {}

    public Movie(int? id, string title, string director, DateTime releaseYear, int budget)
    {
        Id = id;
        Title = title;
        Director = director;
        ReleaseDate = releaseYear;
        Budget = budget;
    }
}