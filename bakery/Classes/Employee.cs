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
        public string PostName { get; set; }
        public string Salary { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public Employee(int idEmployee, string fio, string postName, string salary, DateTime dateOfEmployment)
        {
            IdEmployee = idEmployee;
            Fio = fio;
            PostName = postName;
            Salary = salary;
            DateOfEmployment = dateOfEmployment;
        }
    }   
}
