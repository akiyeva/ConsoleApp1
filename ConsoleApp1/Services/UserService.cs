using ConsoleApp1.Exceptions;
using ConsoleApp1.Models;

namespace ConsoleProject.Services
{
    public class UserService
    {
        public UserService() { }
        public bool Login(string email, string password)
        {
            foreach (var item in DB.users)
            {
                if (item.Email == email && item.Password == password)
                {
                    return true;
                }
            }
            throw new NotFoundException();
        }
        public void AddUser(User user)
        {
            Array.Resize(ref DB.users, DB.users.Length + 1);
            DB.users[DB.users.Length - 1] = user;
        }
    }
}

