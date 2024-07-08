
namespace ConsoleApp1.Models
{
    public class User : BaseEntity
    {
        private static int _id = 1;
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Medicine[] Medicines;
        public bool IsAdmin { get; set; }
        public User(string fullname, string email, string password, bool isAdmin)
        {
            Id = _id++;
            Fullname = fullname;
            Email = email;
            Password = password;
            Medicines = new Medicine[0];
            IsAdmin = isAdmin;
        }
        public override string ToString()
        {
            return $"{Id} {Fullname} {Email}";
        }

        public void CheckPassword(string password)
        {
            if(password ) { }
        } 
    }
}
