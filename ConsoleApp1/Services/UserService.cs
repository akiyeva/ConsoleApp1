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

        public void RemoveUser(int id)
        {
            for (int i = 0; i < DB.users.Length; i++)
            {
                var user = DB.users[i];

                if (user.Id == id)
                {
                    for (int j = i; j < DB.users.Length - 1; j++)
                    {
                        DB.users[j] = DB.users[j + 1];
                    }
                    Array.Resize(ref DB.users, DB.users.Length - 1);
                    Console.WriteLine($"{user.Fullname} user is removed");
                    return;
                }
            }
            throw new NotFoundException("Error: ID not found.");
        }


        public Medicine[] GetAllMedicines(User user)
        {
            return user.Medicines;
        }
        public Medicine GetMedicineById(int id, User user)
        {
            foreach (var item in user.Medicines)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            throw new NotFoundException("Error: ID not found");
        }

        public Medicine GetMedicineByName(string name, User user)
        {
            foreach (var item in user.Medicines)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }
            throw new NotFoundException("Error: Name not found");
        }

        public void RemoveMedicine(int id, User user)
        {
            for (int i = 0; i < user.Medicines.Length; i++)
            {
                var medicine = user.Medicines[i];

                if (medicine.Id == id)
                {
                    for (int j = i; j < user.Medicines.Length - 1; j++)
                    {
                        user.Medicines[j] = user.Medicines[j + 1];
                    }
                    var medicines = user.Medicines;
                    Array.Resize(ref medicines, medicines.Length - 1);

                    Console.WriteLine($"{medicine.Name} medicine is removed");
                    return;
                }
            }
            throw new NotFoundException("Error: ID not found.");
        }
    }
}

