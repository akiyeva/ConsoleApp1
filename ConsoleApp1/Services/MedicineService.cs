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

            Console.WriteLine($"{medicine.Name} is added.");

        }
        public void AddMedicine(User user, int medicineId)
        {
            bool found = false;

            foreach (var med in DB.medicines)
            {
                if (med.Id == medicineId)
                {
                    found = true;

                    Array.Resize(ref user.Medicines, user.Medicines.Length + 1);
                    user.Medicines[^1] = med;

                    Console.WriteLine($"{med.Name} is added to {user.Fullname}.");
                    break;
                }
            }
            if (!found)
            {
                throw new NotFoundException("Error: ID not found");
            }
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


        public void RemoveMedicine(int id)
        {
            for (int i = 0; i < DB.medicines.Length; i++)
            {
                var medicine = DB.medicines[i];

                if (medicine.Id == id)
                {
                    for (int j = i; j < DB.medicines.Length - 1; j++)
                    {
                        DB.medicines[j] = DB.medicines[j + 1];
                    }
                    Array.Resize(ref DB.medicines, DB.medicines.Length - 1);
                    Console.WriteLine($"{medicine.Name} medicine is removed");
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
