using ConsoleApp1.Models;

namespace ConsoleProject.Services
{
    public class CategoryService
    {
        public CategoryService() { }
        public void AddCategory(Category category)
        {
            Array.Resize(ref DB.categories, DB.categories.Length + 1);
            DB.categories[DB.categories.Length - 1] = category;

            Console.WriteLine($"{category.Name} is added.");
        }

        public Medicine[] GetMedicinesByCategory(int id)
        {
            foreach (var cat in DB.categories)
            {
                if(cat.Id == id)
                {
                    return cat.Medicines;
                }
            }
            throw new NullReferenceException();
        }
    }
}
