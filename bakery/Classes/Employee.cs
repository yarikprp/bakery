using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bakery.Classes
{
    public class Employee
    {
        public int IdEmployee { get; set; }
        public string Fio { get; set; }
        public int IdPost { get; set; }
        public decimal Salary { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public Employee(int idEmployee, string fio, int idPost, decimal salary, DateTime dateOfEmployment)
        {
            IdEmployee = idEmployee;
            Fio = fio;
            IdPost = idPost;
            Salary = salary;
            DateOfEmployment = dateOfEmployment;
        }
    }   
}
