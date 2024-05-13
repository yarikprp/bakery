using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bakery.Classes
{
    public class ReceiptWarehouse
    {
        public int IdBalance { get; set; }
        public string NameIngredients { get; set; }
        public string NameProduct { get; set; }
        public string NameCompany { get; set; }
        public DateTime DateOfReceipt { get; set; }
        public string Quantity { get; set; }
        public ReceiptWarehouse(int idBalance, string nameIngredients, string nameProduct, string nameCompany, DateTime dateOfReceipt, string quantity)
        {
            IdBalance = idBalance;
            NameIngredients = nameIngredients;
            NameProduct = nameProduct;
            NameCompany = nameCompany;
            DateOfReceipt = dateOfReceipt;
            Quantity = quantity;
        }
    }
}
