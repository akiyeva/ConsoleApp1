namespace ConsoleApp1.Models
{
    public class Category : BaseEntity
    {
        private static int _id = 1;
        public string Name { get; set; }
        public Medicine[] Medicines;
        public Category(string name)
        {
            Id = _id++;
            Name = name;
            Medicines = new Medicine[0];
        }
        public override string ToString()
        {
            return $"{Id}. {Name}";
        }
    }
}
