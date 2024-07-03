using ConsoleApp1.Exceptions;
using ConsoleApp1.Models;
using ConsoleProject.Services;

namespace ConsoleApp1
{
    public class Program
    {
        private static User currentUser = null;
        private static UserService userService = new UserService();
        private static MedicineService medicineService = new MedicineService();
        private static CategoryService categoryService = new CategoryService();

        static void Main(string[] args)
        {
        }

        public static void UserLogin()
        {
            Console.WriteLine("=== Log In ===");

            bool isLoggedIn = false;

            while (!isLoggedIn)
            {
                Console.WriteLine("Enter email:");
                string email = Console.ReadLine();

                Console.WriteLine("Enter password:");
                string password = Console.ReadLine();

                try
                {
                    isLoggedIn = userService.Login(email, password);
                    foreach (var user in DB.users)
                    {
                        if (user.Email == email && user.Password == password)
                        {
                            currentUser = user;
                            break;
                        }
                    }
                    if (currentUser != null)
                    {
                        Console.WriteLine($"Welcome, {currentUser.Fullname}");
                    }
                }
                catch (NotFoundException)
                {
                    Console.WriteLine("User not found or incorrect credentials.");

                }
            }
        }

        static User CreateUser()
        {
            Console.WriteLine("Enter fullname:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter email:");
            string email = Console.ReadLine();

            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            return new User(name, email, password);
        }

        static Category CreateCategory()
        {
            Console.WriteLine("=== Create a new category ===");

            Console.WriteLine("Enter category name:");
            string name = Console.ReadLine();

            return new Category(name);
        }

        static Medicine CreateMedicine()
        {
            Console.WriteLine("=== Create a new medicine ===");

            Console.WriteLine("Enter medicine name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter medicine price:");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Choose a category for the medicine and enter a category ID from the list:");

            foreach (var category in DB.categories)
            {
                Console.WriteLine(category);
            }

            int categoryId = int.Parse(Console.ReadLine());

            foreach (var item in DB.categories)
            {
                if (categoryId == item.Id)
                {
                    var category = item;
                    return new Medicine(name, price, category);
                }
            }

            throw new NotFoundException("Error: ID not found.");
        }

    }
}
