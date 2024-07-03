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
            while (currentUser == null)
            {
                ShowUserMenu();
            }

            while (currentUser != null)
            {
                ShowMainMenu();
            }
        }

        static void ShowMainMenu()
        {
            Console.WriteLine("=== Main menu ===");
            Console.WriteLine("[1] User menu");
            Console.WriteLine("[2] Medicine menu");
            Console.WriteLine("[3] Category menu");
            Console.WriteLine("[4] Log out");
            Console.WriteLine("[0] Exit program");
            Console.WriteLine();

            int command = int.Parse(Console.ReadLine());

            switch (command)
            {
                case 1:
                    ShowLoggedUserMenu();
                    break;
                case 2:
                    ShowMedicineMenu();
                    break;
                case 3:
                    ShowCategoryMenu();
                    break;
                case 4:
                    currentUser = null;
                    ShowUserMenu();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }
        }
        static void ShowCategoryMenu()
        {

            Console.WriteLine("[1] Create and add category to DB");
            Console.WriteLine("[2] Go back to the main menu");
            Console.WriteLine("[0] Exit program");
            Console.WriteLine();

            int command = int.Parse(Console.ReadLine());

            switch (command)
            {
                case 1:
                    Category category = CreateCategory();
                    categoryService.AddCategory(category);
                    Console.WriteLine($"{category} added to the database");
                    break;
                case 2:
                    ShowMainMenu();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }
        }

        static void ShowMedicineMenu()
        {
            //  var medicineService = new MedicineService();

            Console.WriteLine("=== Medicine menu ===");
            Console.WriteLine("[1] Create and add medicine to DB");
            Console.WriteLine("[2] Show all medicines");
            Console.WriteLine("[3] Search medicine by ID");
            Console.WriteLine("[4] Search medicine by Name");
            Console.WriteLine("[5] Search medicines by Category ID");
            Console.WriteLine("[6] Remove medicine");
            Console.WriteLine("[7] Update medicine");
            Console.WriteLine("[0] Exit");
            Console.WriteLine();

            int command = int.Parse(Console.ReadLine());

            switch (command)
            {
                case 1:
                    Medicine medicine = CreateMedicine();
                    medicineService.AddMedicine(medicine);
                    Console.WriteLine($"{medicine} added to the database.");
                    break;
                case 2:
                    var medicines = medicineService.GetAllMedicines();
                    foreach (var med in medicines)
                    {
                        Console.WriteLine(med);
                    }
                    break;
                case 3:
                    Console.WriteLine("Enter medicine ID:");
                    int medicineId = int.Parse(Console.ReadLine());
                    Console.WriteLine(medicineService.GetMedicineById(medicineId));
                    break;
                case 4:
                    Console.WriteLine("Enter medicine name:");
                    string medicineName = Console.ReadLine();
                    Console.WriteLine(medicineService.GetMedicineByName(medicineName));
                    break;
                case 5:
                    Console.WriteLine("Enter medicine category ID:");
                    int medicineCategoryId = int.Parse(Console.ReadLine());
                    Console.WriteLine(medicineService.GetMedicineByCategory(medicineCategoryId));
                    break;
                case 6:
                    Console.WriteLine("Enter medicine ID:");
                    int removedMedicineId = int.Parse(Console.ReadLine());
                    medicineService.RemoveMedicine(removedMedicineId);
                    break;
                case 7:
                    Console.WriteLine("Enter ID of medicine you what to update:");
                    int updatedMedicineId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter new medicine:");
                    var newMedicine = CreateMedicine();
                    medicineService.UpdateMedicine(updatedMedicineId, newMedicine);
                    break;
                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }
        }

        static void ShowLoggedUserMenu()
        {
            Console.WriteLine("[1] User registration (Add user to database)");
            Console.WriteLine("[2] Go back to the main menu");
            Console.WriteLine();

            int command = int.Parse(Console.ReadLine());

            switch (command)
            {
                case 1:
                    UserRegistration();
                    break;
                case 2:
                    ShowMainMenu();
                    break;
            }
        }
        static void ShowUserMenu()
        {
            Console.WriteLine("[1] User registration (Add user to database)");
            Console.WriteLine("[2] User login");
            Console.WriteLine("[0] Exit program");
            Console.WriteLine();

            int command = int.Parse(Console.ReadLine());

            switch (command)
            {
                case 1:
                    UserRegistration();
                    break;
                case 2:
                    UserLogin();
                    break;
                case 0:
                    return;
            }
        }
        public static void UserRegistration()
        {
            Console.WriteLine("=== Registration ===");

            //  var userService = new UserService();

            var user = CreateUser();
            userService.AddUser(user);

            Console.WriteLine($"User {user.Fullname} created and added to the database.");
            Console.WriteLine();
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
