using Domain;

namespace DataAccess
{
    public class InMemoryDatabase
    {
        private List<Movie> Movies { get; }
        private List<User> Users { get; }

        public InMemoryDatabase()
        {
            Movies = new List<Movie>();
            Users = new List<User>();
            LoadDefaultMovies();
            LoadDefaultAdministratorUser();
        }

        public List<Movie> GetMovies()
        {
            return Movies;
        }

        public Movie? GetMovie(string title)
        {
            Movie? movie = Movies.FirstOrDefault(movie => movie.Title == title);
            return movie;
        }

        public void AddMovie(Movie movie)
        {
            Movies.Add(movie);
        }

        public void DeleteMovie(Movie movie)
        {
            Movies.Remove(movie);
        }

        public void UpdateMovie(Movie movieToUpdate)
        {
            Movie? movie = Movies.Find(m => m.Title == movieToUpdate.Title);
            var movieToUpdateIndex = Movies.IndexOf(movie);
            Movies[movieToUpdateIndex] = movieToUpdate;
        }

        public List<User> GetUsers()
        {
            return Users;
        }

        public User? GetUser(string email)
        {
            User? user = Users.FirstOrDefault(user => user.Email == email);
            return user;
        }

        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public void DeleteUser(User user)
        {
            Users.Remove(user);
        }

        public void UpdateUser(User userToUpdate)
        {
            User? user = Users.Find(u => u.Email == userToUpdate.Email);
            var userToUpdateIndex = Users.IndexOf(user);
            Users[userToUpdateIndex] = userToUpdate;
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