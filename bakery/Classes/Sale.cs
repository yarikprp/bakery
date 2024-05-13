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
        public string Fio { get; set; }
        public DateTime DateOfSale { get; set; }
        public int Quantity { get; set; }
        public Sale(int idSale, int idPlan, string fio, DateTime dateOfSale, int quantity)
        {
            IdSale = idSale;
            IdPlan = idPlan;
            Fio = fio;
            DateOfSale = dateOfSale;
            Quantity = quantity;
        }
    }
}
