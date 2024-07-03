namespace ConsoleApp1.Models
{
    public class User : BaseEntity
    {
        private static int _id;
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public User(string fullname, string email, string password)
        {
            Id = ++_id;
            Fullname = fullname;
            Email = email;
            Password = password;
        }
    }
}
