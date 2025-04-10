using Domain;

namespace DataAccess;

public class InMemoryDatabase
{
    public List<Movie> Movies { get; set; }

    public InMemoryDatabase()
    {
        Movies = new List<Movie>();
    }
}