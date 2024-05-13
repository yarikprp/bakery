using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bakery.Classes
{
    public class Supplier
    {
        public int IdSupplier { get; set; }
        public string Ingredient { get; set; }
        public string NameCompany { get; set; }
        public Supplier(int idSupplier, string ingredient, string nameCompany)
        {
            IdSupplier = idSupplier;
            Ingredient = ingredient;
            NameCompany = nameCompany;
        }
    }
}
