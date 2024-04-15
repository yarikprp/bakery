using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulinaria_app_v2.Classes
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirthday { get; set; }
        public string Login { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }
     
        public User(int userId, string firstName, string lastName, string patronymic, DateTime dateOfBirthday,
            string login, string phone, string adress, int roleId, string email)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            DateOfBirthday = dateOfBirthday;
            Login = login;
            Phone = phone;
            Adress = adress;
            RoleId = roleId;
            Email = email;
        }
    }
}
