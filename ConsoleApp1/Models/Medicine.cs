namespace ConsoleApp1.Models
{
    public class Medicine : BaseEntity
    {
        private static int _id = 1;
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedTime { get; set; }
        public Medicine(string name, decimal price, Category category)
        {
            Id = _id++;
            Name = name;
            Price = price;
            Category = category;
            CreatedTime = DateTime.Now;
        }
        public override string ToString()
        {
            return $"{Id}. {Name} ({Category.Name}) | {CreatedTime}";
        }
    }
}
