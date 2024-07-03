namespace ConsoleApp1.Models
{
    public class Category : BaseEntity
    {
        private static int _id;
        public string Name { get; set; }
        public Category(string name)
        {
            Id = ++_id;
            Name = name;
        }
        public override string ToString()
        {
            return $"{Id}. {Name}";
        }
    }
}
