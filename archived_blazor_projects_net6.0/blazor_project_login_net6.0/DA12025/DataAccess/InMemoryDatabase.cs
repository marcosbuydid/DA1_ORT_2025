using Domain;

namespace DataAccess
{
    public class InMemoryDatabase
    {
        public List<Movie> Movies { get; set; }
        public List<User> Users { get; set; }

        public InMemoryDatabase()
        {
            Movies = new List<Movie>();
            Users = new List<User>();
            LoadDefaultMovies();
            LoadDefaultAdministratorUser();
        }

        private void LoadDefaultMovies()
        {
            Movies.Add(new Movie("Black Rain", "Ridley Scott", new DateTime(1989, 09, 22), 30000000));
            Movies.Add(new Movie("Cast Away", "Robert Zemeckis", new DateTime(2000, 12, 22), 25000000));
            Movies.Add(new Movie("Training Day", "Antoine Fuqua", new DateTime(2002, 01, 18), 10000000));
        }

        private void LoadDefaultAdministratorUser()
        {
            Users.Add(new User("Marcos", "Buydid", "marcosb@email.com", "123456", "Administrator"));
        }
    }
}