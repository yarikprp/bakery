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
        public int IdIngredients { get; set; }
        public int IdProduct { get; set; }
        public int IdSupplier { get; set; }
        public DateTime DateOfReceipt { get; set; }
        public int Quantity { get; set; }
        public ReceiptWarehouse(int idBalance, int idIngredients, int idProduct, int idSupplier, DateTime dateOfReceipt, int quantity)
        {
            IdBalance = idBalance;
            IdIngredients = idIngredients;
            IdProduct = idProduct;
            IdSupplier = idSupplier;
            DateOfReceipt = dateOfReceipt;
            Quantity = quantity;
        }
    }
}
