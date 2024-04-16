using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bakery.Classes
{
    public class Sale
    {
        public int IdSale { get; set; }
        public int IdPlan { get; set; }
        public int IdEmployee { get; set; }
        public DateTime DateOfSale { get; set; }
        public int Quantity { get; set; }
        public Sale(int idSale, int idPlan, int idEmployee, DateTime dateOfSale, int quantity)
        {
            IdSale = idSale;
            IdPlan = idPlan;
            IdEmployee = idEmployee;
            DateOfSale = dateOfSale;
            Quantity = quantity;
        }
    }
}
