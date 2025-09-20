using DataAccess.Interfaces;
using Domain;

namespace DataAccess
{
    public class InMemoryDatabase : IMovieRepository, IUserRepository
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

        private void LoadDefaultMovies()
        {
            Movies.Add(new Movie(null, "Black Rain", "Ridley Scott", new DateTime(1989, 09, 22), 30000000));
            Movies.Add(new Movie(null, "Cast Away", "Robert Zemeckis", new DateTime(2000, 12, 22), 25000000));
            Movies.Add(new Movie(null, "Training Day", "Antoine Fuqua", new DateTime(2002, 01, 18), 10000000));
        }

        private void LoadDefaultAdministratorUser()
        {
            Users.Add(new User(null, "Marcos", "Buydid", "marcosb@email.com", "123456", "Administrator"));
        }

        public List<Movie> GetMovies()
        {
            return Movies;
        }

        public Movie? GetMovie(Func<Movie, bool> filter)
        {
            return Movies.Where(filter).FirstOrDefault();
        }

        public void AddMovie(Movie movie)
        {
            Movies.Add(movie);
        }

        public void UpdateMovie(Movie movieToUpdate)
        {
            Movie? movie = Movies.Find(m => m.Title == movieToUpdate.Title);
            var movieToUpdateIndex = Movies.IndexOf(movie);
            movieToUpdate.Budget = movie.Budget;
            Movies[movieToUpdateIndex] = movieToUpdate;
        }

        public void DeleteMovie(Movie movie)
        {
            Movies.Remove(movie);
        }

        public List<User> GetUsers()
        {
            return Users;
        }

        public User? GetUser(Func<User, bool> filter)
        {
            return Users.Where(filter).FirstOrDefault();
        }

        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public void UpdateUser(User userToUpdate)
        {
            User? user = Users.Find(u => u.Email == userToUpdate.Email);
            var userToUpdateIndex = Users.IndexOf(user);
            userToUpdate.Password = user.Password;
            Users[userToUpdateIndex] = userToUpdate;
        }

        public void DeleteUser(User user)
        {
            Users.Remove(user);
        }
    }
}