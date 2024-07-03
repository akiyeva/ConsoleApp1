using ConsoleApp1.Exceptions;
using ConsoleApp1.Models;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
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
