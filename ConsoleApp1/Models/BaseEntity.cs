namespace ConsoleApp1.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
        public BaseEntity()
        {

        }
    }
}
