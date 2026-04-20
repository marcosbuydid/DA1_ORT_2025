using Domain;
using Services.Interfaces.Repositories;

namespace DataAccess;

public class InMemoryUserRepository: IUserRepository
{
        private List<User> Users { get; }

        public InMemoryUserRepository()
        {
            Users = new List<User>();
            LoadDefaultAdministratorUser();
        }

        private void LoadDefaultAdministratorUser()
        {
            Users.Add(new User(null, "Marcos", "Buydid", "marcosb@email.com",
                "fTjXowEsKZzCX8K9YYq3FXG9eztFfl2fuOa6buT59tUHUutO8C7z49UnTUIaA0Zr", "Administrator"));
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