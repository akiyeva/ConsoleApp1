using ConsoleApp1.Exceptions;
using ConsoleApp1.Helpers;
using ConsoleApp1.Models;
using ConsoleProject.Services;
using System.Linq.Expressions;

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
            TextColor.WriteLine("Welcome to the program!", ConsoleColor.Cyan);
            TextColor.WriteLine("(Tap ENTER to continue)", ConsoleColor.DarkGray);

            Console.ReadLine();
            TextColor.WriteLine("Firstly you should register admin.", ConsoleColor.Green);
            Console.WriteLine();
            AdminRegistration();

            while (true)
            {
                try
                {
                    if (currentUser == null)
                        ShowUnsignedMenu();
                    else if (currentUser.IsAdmin)
                        ShowAdminMenu();
                    else if (!currentUser.IsAdmin)
                        ShowUserMainMenu();
                }
                catch (Exception ex)
                {
                    TextColor.WriteLine($"Error: {ex.Message}", ConsoleColor.Red);
                }
            }
        }

        static void ShowUnsignedMenu()
        {
            TextColor.WriteLine("|Menu|", ConsoleColor.Blue);
            Console.WriteLine("[1] Registration");
            Console.WriteLine("[2] Login");
            Console.WriteLine("[0] Exit program");
            Console.WriteLine();

            try
            {
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
                        Environment.Exit(0);
                        break;
                    default:
                        TextColor.WriteLine("Invalid input format. Please enter a valid number", ConsoleColor.Red);
                        break;
                }
            }
            catch (FormatException)
            {
                TextColor.WriteLine("Invalid input format. Please enter a valid number.", ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                TextColor.WriteLine($"Error: {ex.Message}", ConsoleColor.Red);
            }
        }

        static void ShowAdminMenu()
        {
            TextColor.WriteLine("|Admin menu|", ConsoleColor.Blue);
            Console.WriteLine("[1] User menu");
            Console.WriteLine("[2] Category menu");
            Console.WriteLine("[3] Medicine menu");
            Console.WriteLine("[4] Log out");
            Console.WriteLine("[0] Exit program");
            Console.WriteLine();

            try
            {
                int command = int.Parse(Console.ReadLine());

                switch (command)
                {
                    case 1:
                        ShowAdminUserMenu();
                        break;
                    case 2:
                        ShowAdminCategoryMenu();
                        break;
                    case 3:
                        ShowAdminMedicineMenu();
                        break;
                    case 4:
                        currentUser = null;
                        ShowUnsignedMenu();
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                    default:
                        TextColor.WriteLine("Invalid input format. Please enter a valid number", ConsoleColor.Red);
                        break;
                }
            }
            catch (FormatException)
            {
                TextColor.WriteLine("Invalid input format. Please enter a valid number.", ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                TextColor.WriteLine($"Error: {ex.Message}", ConsoleColor.Red);
            }
        }

        static void ShowUserMainMenu()
        {
            TextColor.WriteLine("|Main menu|", ConsoleColor.Cyan);
            Console.WriteLine("[1] Add medicine to my database");
            Console.WriteLine("[2] Show my medicines");
            Console.WriteLine("[3] Search medicine by ID");
            Console.WriteLine("[4] Search medicine by Name");
            Console.WriteLine("[5] Search medicines by Category ID");
            Console.WriteLine("[6] Remove medicine");
            Console.WriteLine("[7] Log out");
            Console.WriteLine("[0] Exit program");
            Console.WriteLine();

            try
            {
                int command = int.Parse(Console.ReadLine());

                switch (command)
                {
                    case 1:
                        Console.WriteLine($"Choose medicine from the list for adding into the {currentUser.Fullname} database.");
                        var allMedicines = medicineService.GetAllMedicines();
                        foreach (var med in allMedicines)
                        {
                            Console.WriteLine(med);
                        }
                        Console.WriteLine("Please enter ID of the medicine you want to add");

                        int inputId = int.Parse(Console.ReadLine());

                        try
                        {
                            medicineService.AddMedicine(currentUser, inputId);
                            Medicine addedMedicine = null;

                            foreach (var med in allMedicines)
                            {
                                if (med.Id == inputId)
                                {
                                    addedMedicine = med;
                                    break;
                                }
                            }
                            if (addedMedicine != null)
                                Console.WriteLine($"{addedMedicine.Name} added.");
                        }
                        catch (NotFoundException ex)
                        {
                            TextColor.WriteLine(ex.Message, ConsoleColor.Red);
                        }
                        break;
                    case 2:
                        var medicines = userService.GetAllMedicines(currentUser);
                        foreach (var med in medicines)
                            Console.WriteLine(med);
                        break;
                    case 3:
                        Console.WriteLine("Enter medicine ID:");

                        int medicineId = int.Parse(Console.ReadLine());
                        Console.WriteLine(userService.GetMedicineById(medicineId, currentUser));

                        break;

                    case 4:
                        Console.WriteLine("Enter medicine name:");

                        string medicineName = Console.ReadLine();
                        Console.WriteLine(userService.GetMedicineByName(medicineName, currentUser));
                        break;
                    case 5:
                        Console.WriteLine("Enter category ID:");

                        foreach (var category in DB.categories)
                        {
                            Console.WriteLine(category);
                        }

                        int medicineCategoryId = int.Parse(Console.ReadLine());

                        var medicinesPart = categoryService.GetMedicinesByCategory(medicineCategoryId);
                        foreach (var med in medicinesPart)
                        {
                            Console.WriteLine(med);
                        }
                        Console.WriteLine();

                        break;
                    case 6:
                        Console.WriteLine("Enter medicine ID you want to delete:");
                        var medicinesAll = userService.GetAllMedicines(currentUser);
                        foreach (var med in medicinesAll)
                        {
                            Console.WriteLine(med);
                        }
                        int removedMedicineId = int.Parse(Console.ReadLine());
                        userService.RemoveMedicine(removedMedicineId, currentUser);
                        break;
                    case 7:
                        currentUser = null;
                        ShowUnsignedMenu();
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                    default:
                        TextColor.WriteLine("Invalid input format. Please enter a valid number", ConsoleColor.Red);
                        break;
                }
            }
            catch (FormatException)
            {
                TextColor.WriteLine("Invalid input format. Please enter a valid number.", ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                TextColor.WriteLine($"Error: {ex.Message}", ConsoleColor.Red);
            }
        }

        static void ShowAdminCategoryMenu()
        {
            TextColor.WriteLine("|Category menu|", ConsoleColor.DarkMagenta);
            Console.WriteLine("[1] Create category");
            Console.WriteLine("[2] Go back to the main menu");
            Console.WriteLine("[0] Exit program");
            Console.WriteLine();

            try
            {
                int command = int.Parse(Console.ReadLine());

                switch (command)
                {
                    case 1:
                        Category category = CreateCategory();
                        categoryService.AddCategory(category);
                        Console.WriteLine($"{category} added to the database");
                        break;
                    case 2:
                        ShowUserMainMenu();
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                    default:
                        TextColor.WriteLine("Invalid input format. Please enter a valid number", ConsoleColor.Red);
                        break;
                }
            }
            catch (FormatException)
            {
                TextColor.WriteLine("Invalid input format. Please enter a valid number.", ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void ShowAdminMedicineMenu()
        {
            TextColor.WriteLine("|Medicine menu|", ConsoleColor.Yellow);
            Console.WriteLine("[1] Create medicine (recommended to create category first)");
            Console.WriteLine("[2] Show all medicines");
            Console.WriteLine("[3] Search medicine by ID");
            Console.WriteLine("[4] Search medicine by Name");
            Console.WriteLine("[5] Search medicines by Category ID");
            Console.WriteLine("[6] Remove medicine");
            Console.WriteLine("[7] Update medicine");
            Console.WriteLine("[8] Go back to the main menu");
            Console.WriteLine("[0] Exit");
            Console.WriteLine();

            try
            {
                int command = int.Parse(Console.ReadLine());

                switch (command)
                {
                    case 1:
                        Medicine medicine = CreateMedicine();
                        medicineService.AddMedicine(medicine);
                        break;
                    case 2:
                        var allMedicines = medicineService.GetAllMedicines();
                        foreach (var med in allMedicines)
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
                        Console.WriteLine("Enter category ID:");

                        foreach (var category in DB.categories)
                        {
                            Console.WriteLine(category);
                        }
                        int medicineCategoryId = int.Parse(Console.ReadLine());

                        var medicinesPart = categoryService.GetMedicinesByCategory(medicineCategoryId);
                        foreach (var med in medicinesPart)
                        {
                            Console.WriteLine(med);
                        }
                        Console.WriteLine();
                        break;
                    case 6:
                        Console.WriteLine("Enter medicine ID you what to delete:");
                        foreach (var med in DB.medicines)
                        {
                            Console.WriteLine(med);
                        }
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
                    case 8:
                        ShowAdminMenu();
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                    default:
                        TextColor.WriteLine("Invalid input format. Please enter a valid number", ConsoleColor.Red);
                        break;
                }
            }
            catch (FormatException)
            {
                TextColor.WriteLine("Invalid input format. Please enter a valid number.", ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                TextColor.WriteLine($"Error: {ex.Message}", ConsoleColor.Red);
            }
        }

        static void ShowAdminUserMenu()
        {
            TextColor.WriteLine("|User menu|", ConsoleColor.Blue);
            Console.WriteLine("[1] Add new user");
            Console.WriteLine("[2] Delete user");
            Console.WriteLine("[3] Add new admin");
            Console.WriteLine("[4] Back to the main menu");
            Console.WriteLine("[0] Exit program");
            Console.WriteLine();

            try
            {
                int command = int.Parse(Console.ReadLine());

                switch (command)
                {
                    case 1:
                        UserRegistration();
                        break;
                    case 2:
                        foreach (var user in DB.users)
                        {
                            Console.WriteLine(user);
                        }
                        Console.WriteLine("Enter user ID:");
                        int removedUserId = int.Parse(Console.ReadLine());
                        userService.RemoveUser(removedUserId);
                        break;
                    case 3:
                        AdminRegistration();
                        ShowAdminMenu();
                        break;
                    case 4:
                        ShowAdminMenu();
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                    default:
                        TextColor.WriteLine("Invalid input format. Please enter a valid number", ConsoleColor.Red);
                        break;
                }
            }
            catch (FormatException)
            {
                TextColor.WriteLine("Invalid input format. Please enter a valid number.", ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                TextColor.WriteLine($"Error: {ex.Message}", ConsoleColor.Red);
            }
        }

        public static void UserRegistration()
        {
            TextColor.WriteLine("|User registration|", ConsoleColor.Blue);

            try
            {
                var user = CreateUser();
                userService.AddUser(user);

                Console.WriteLine($"User {user.Fullname} created.");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                TextColor.WriteLine($"Error: {ex.Message}", ConsoleColor.Red);
            }
        }
        public static void UserLogin()
        {
            TextColor.WriteLine("|Log In|", ConsoleColor.Blue);

            bool isLoggedIn = false;

            while (!isLoggedIn)
            {
                try
                {
                    Console.WriteLine("Enter email:");
                    string email = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(email))
                    {
                        Console.WriteLine("Email cannot be empty. Please enter a valid email.");
                        continue;
                    }

                    Console.WriteLine("Enter password:");
                    string password = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(password))
                    {
                        Console.WriteLine("Password cannot be empty. Please enter a valid password.");
                        continue;
                    }

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
                            Console.WriteLine();
                        }
                    }
                    catch (NotFoundException)
                    {
                        TextColor.WriteLine("User not found or incorrect credentials.", ConsoleColor.Red);
                        Console.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    TextColor.WriteLine($"Error: {ex.Message}", ConsoleColor.Red);
                }
            }
        }

        public static void AdminRegistration()
        {
            TextColor.WriteLine("|Admin Registration|", ConsoleColor.Blue);

            while (true)
            {
                try
                {
                    var admin = CreateUser(true);
                    userService.AddUser(admin);

                    Console.WriteLine($"Admin {admin.Fullname} created.");
                    Console.WriteLine();
                    break;
                }
                catch (Exception ex)
                {
                    TextColor.WriteLine(ex.Message, ConsoleColor.Red);
                }
            }
        }
        static User CreateUser(bool isAdmin = false)
        {

            string name = null;
            string email = null;
            string password = null;

            try
            {

                while (true)
                {
                    Console.WriteLine("Enter fullname:");
                    name = TakeInputName();
                    break;
                }

                while (true)
                {
                    Console.WriteLine("Enter email:");
                    email = TakeInputEmail();

                    try
                    {

                        if (DB.users.Any(e => e.Email == email))
                        {
                            Console.WriteLine("This email is already used by another user.");
                            email = null;
                            continue;
                        }

                        break;
                    }
                    catch (InvalidCredentialsException ex)
                    {
                        TextColor.WriteLine(ex.Message, ConsoleColor.Red);
                    }
                }

                while (true)
                {
                    Console.WriteLine("Enter password:");
                    password = TakeInputPassword();
                    break;
                }
            }
            catch (Exception ex)
            {
                TextColor.WriteLine(ex.Message, ConsoleColor.Red);
            }

            return new User(name, email, password, isAdmin);
        }

        static Category CreateCategory()
        {
            Console.WriteLine("|Create a new category|");

            string name = null;
            while (string.IsNullOrWhiteSpace(name))
            {
                try
                {
                    Console.WriteLine("Enter category name:");
                    name = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Console.WriteLine("Category name cannot be empty. Please enter a valid category name.");
                    }
                    else
                    {
                        foreach (var c in DB.categories)
                        {
                            if (c.Name == name)
                            {
                                throw new Exception("Category with this name already exists.");
                            }
                        }

                        return new Category(name);
                    }
                }
                catch (Exception ex)
                {
                    TextColor.WriteLine($"Error: {ex.Message}", ConsoleColor.Red);
                }
            }
            return null;
        }

        static Medicine CreateMedicine()
        {
            Console.WriteLine("|Create a new medicine|");

            string name = null;
            while (string.IsNullOrWhiteSpace(name))
            {
                try
                {
                    Console.WriteLine("Enter medicine name:");
                    name = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Console.WriteLine("Medicine name cannot be empty. Please enter a valid medicine name.");
                        continue;
                    }

                    foreach (var m in DB.medicines)
                    {
                        if (m.Name == name)
                        {
                            throw new Exception("This medicine already exists in the database.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    TextColor.WriteLine($"Error: {ex.Message}", ConsoleColor.Red);
                    name = null;
                }
            }

            decimal price = 0;
            while (price <= 0)
            {
                try
                {
                    Console.WriteLine("Enter medicine price:");
                    price = decimal.Parse(Console.ReadLine());
                    if (price <= 0)
                    {
                        Console.WriteLine("Price must be a positive number. Please enter a valid price.");
                    }
                }
                catch (FormatException)
                {
                    TextColor.WriteLine("Invalid price format. Please enter a valid number.", ConsoleColor.Red);
                }
            }

            int categoryId = 0;
            while (categoryId <= 0)
            {
                try
                {
                    Console.WriteLine("Choose a category for the medicine and enter a category ID from the list:");

                    foreach (var category in DB.categories)
                    {
                        Console.WriteLine(category);
                    }

                    categoryId = int.Parse(Console.ReadLine());

                    foreach (var item in DB.categories)
                    {
                        if (categoryId == item.Id)
                        {
                            var category = item;
                            Medicine newMedicine = new Medicine(name, price, category);

                            Array.Resize(ref category.Medicines, category.Medicines.Length + 1);
                            category.Medicines[^1] = newMedicine;

                            return newMedicine;
                        }
                    }

                    throw new NotFoundException("Error: ID not found.");
                }
                catch (FormatException)
                {
                    TextColor.WriteLine("Invalid input format. Please enter a valid number.", ConsoleColor.Red);
                }
                catch (Exception ex)
                {
                    TextColor.WriteLine($"Error: {ex.Message}", ConsoleColor.Red);
                    categoryId = 0;
                }
            }

            return null;
        }

        static string TakeInputName()
        {
            while (true)
            {
                try
                {
                    string name = Console.ReadLine();
                    if (User.CheckFullname(name))
                        return name;

                }
                catch (InvalidCredentialsException ex)
                {
                    TextColor.WriteLine(ex.Message, ConsoleColor.Red);
                }
            }
        }
        static string TakeInputEmail()
        {
            while (true)
            {
                try
                {
                    string email = Console.ReadLine();
                    if (User.CheckEmail(email))
                        return email;

                }
                catch (InvalidCredentialsException ex)
                {
                    TextColor.WriteLine(ex.Message, ConsoleColor.Red);
                }
            }
        }
        static string TakeInputPassword()
        {
            while (true)
            {
                try
                {
                    string pass = Console.ReadLine();
                    if (User.CheckPassword(pass))
                        return pass;

                }
                catch (InvalidCredentialsException ex)
                {
                    TextColor.WriteLine(ex.Message, ConsoleColor.Red);
                }
            }
        }
    }
    }

