
using Domain;

namespace DataAccess
{
    public class InMemoryDatabase
    {
        public List<Movie> Movies { get; set; }

        public InMemoryDatabase()
        {
            Movies = new List<Movie>();
            LoadDefaultMovies();
        }

        private void LoadDefaultMovies()
        {
            Movies.Add(new Movie("Black Rain", "Ridley Scott", new DateTime(1989, 09, 22)));
            Movies.Add(new Movie("Cast Away", "Robert Zemeckis", new DateTime(2000, 12, 22)));
            Movies.Add(new Movie("Training Day", "Antoine Fuqua", new DateTime(2002, 01, 18)));
        }
    }
}
