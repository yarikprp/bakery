using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bakery.Classes
{
    public class Company
    {
        public int IdCompany { get; set; }
        public string NameCompany { get; set; }
        public string Fio { get; set; }
        public string NamePhone { get; set; }
        public string Adress { get; set; }
        public Company(int idCompany, string nameCompany, string fio, string namePhone, string adress)
        {
            IdCompany = idCompany;
            NameCompany = nameCompany;
            Fio = fio;
            NamePhone = namePhone;
            Adress = adress;
        }
    }
}
