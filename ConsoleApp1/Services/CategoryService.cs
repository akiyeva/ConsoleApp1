using ConsoleApp1.Exceptions;
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
        
            throw new NullReferenceException("No medicine found");
        }

        //public Medicine[] GetMedicinesByCategory(int id, User user)
        //{
        //    List<Medicine> medicines = new List<Medicine>();

        //    foreach (var cat in user.Medicines)
        //    {
        //        if (cat.CategoryId == id)
        //        {
        //            medicines.Add(cat);
        //        }
        //    }

        //    if (medicines.Count == 0)
        //    {
        //        throw new NullReferenceException("No medicines found in the specified category");
        //    }

        //    return medicines.ToArray();
        }
        //public Medicine[] GetMedicinesByCategory(int id, User user)
        //{
        //    foreach (var cat in user.Medicines)
        //    {
        //        if (cat.CategoryId == id)
        //        {
        //            return cat; 
        //        }
        //    }
        //    throw new NullReferenceException("No medicine found");
        //}

        //public void RemoveMedicine(int id, Category category) {

        //    for (int i = 0; i < category.Medicines.Length; i++)
        //    {
        //        var medicine = category.Medicines[i];

        //        if (medicine.Id == id)
        //        {
        //            for (int j = i; j < category.Medicines.Length - 1; j++)
        //            {
        //                category.Medicines[j] = category.Medicines[j + 1];
        //            }
        //            Array.Resize(ref category.Medicines, category.Medicines.Length - 1);
        //            return;
        //        }
        //    }
        //    throw new NotFoundException("Error: ID not found.");
        //}
    }
}
