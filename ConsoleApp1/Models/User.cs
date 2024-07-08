using ConsoleApp1.Exceptions;
using System.Text.RegularExpressions;

namespace ConsoleApp1.Models
{
    public class User : BaseEntity
    {
        private static int _id = 1;
        private string _password;
        private string _fullname;
        private string _email;
        public string Fullname
        {
            get => _fullname;
            set
            {
                if (CheckFullname(value))
                    _fullname = value;
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                if (CheckEmail(value))
                    _email = value;
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                if (CheckPassword(value))
                    _password = value;
            }
        }
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

        public static bool CheckPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new InvalidCredentialsException("Password cannot be empty.");

            bool hasUpper = false;
            bool hasLower = false;
            bool hasDigit = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c))
                    hasUpper = true;
                else if (char.IsLower(c))
                    hasLower = true;
                else if (char.IsDigit(c))
                    hasDigit = true;
            }

           
            if (!hasUpper || !hasLower || !hasDigit || password.Length < 8)
                throw new InvalidCredentialsException("Password should contain at least 8 characters, including upper and lower case letters, and at least one digit.");

            return true;
            
        }


        public static bool CheckFullname(string fullname)
        {
            if (string.IsNullOrWhiteSpace(fullname))
                throw new InvalidCredentialsException("Fullname cannot be empty.");

            if (!char.IsUpper(fullname[0]))
                throw new InvalidCredentialsException("First letter should be an uppercase letter.");

            return true;
        }

        public static bool CheckEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new InvalidCredentialsException("Email cannot be null.");

            string pattern = @"^(([^<>()[\]\\.,;:\s@\""]+(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$";
            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(email))
            {
                throw new InvalidCredentialsException($"The email address '{email}' is not in a valid format.");
            }
            return true;
        }
    }
}
