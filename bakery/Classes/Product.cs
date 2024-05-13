using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bakery.Classes
{
    public class Product
    {
        public int IdProduct { get; set; }
        public string NameProduct { get; set; }
        public string Fio { get; set; }
        public DateTime Releasses { get; set; }
        public decimal Price { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public int Quantity { get; set; }
        public Product(int idProduct,  string nameProduct, string fio, DateTime releasses, decimal price, DateTime dateOfManufacture, int quantity)
        {
            IdProduct = idProduct;
            NameProduct = nameProduct;
            Fio = fio;
            Releasses = releasses;
            Price = price;
            DateOfManufacture = dateOfManufacture;
            Quantity = quantity;
        }
    }
}
