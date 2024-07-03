using ConsoleApp1.Exceptions;
using ConsoleApp1.Models;

namespace ConsoleProject.Services
{
    public class MedicineService
    {
        public MedicineService() { }
        public void AddMedicine(Medicine medicine)
        {
            Array.Resize(ref DB.medicines, DB.medicines.Length + 1);
            DB.medicines[DB.medicines.Length - 1] = medicine;

            Console.WriteLine($"{medicine.Name} is added to database.");
        }

        public Medicine[] GetAllMedicines()
        {
            return DB.medicines;
        }

        public Medicine GetMedicineById(int id)
        {
            foreach (var item in DB.medicines)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            throw new NotFoundException("Error: ID not found");
        }

        public Medicine GetMedicineByName(string name)
        {
            foreach (var item in DB.medicines)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }
            throw new NotFoundException("Error: Name not found");
        }

        public IEnumerable<Medicine> GetMedicineByCategory(int id)
        {
            foreach (var item in DB.medicines)
            {
                if (item.CategoryId == id)
                {
                    yield return item;
                }
            }
            throw new NotFoundException("Error: Category not found");
        }

        public void RemoveMedicine(int id)
        {
            for (int i = 0; i < DB.medicines.Length; i++)
            {
                var medicine = DB.medicines[i];

                if (medicine.Id == id)
                {
                    for (int j = i; j < DB.medicines.Length; j++)
                    {
                        DB.medicines[j] = DB.medicines[j + 1];
                    }
                    Array.Resize(ref DB.medicines, DB.medicines.Length - 1);
                    Console.WriteLine($"{medicine.Name} is removed");
                    return;
                }
            }
            throw new NotFoundException("Error: ID not found.");
        }

        public void UpdateMedicine(int id, Medicine newMedicine)
        {
            foreach (var item in DB.medicines)
            {
                if (item.Id == id)
                {
                    item.Name = newMedicine.Name;
                    item.Price = newMedicine.Price;
                    Console.WriteLine($"Medicine with {item.Id} ID successfully updated.");
                    return;
                }
            }

            throw new NotFoundException("Error: invalid ID or medicine not found");
        }
    }


}
