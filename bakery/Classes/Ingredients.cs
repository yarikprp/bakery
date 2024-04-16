using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bakery.Classes
{
    public class Ingredients
    {
        public int IdIngredients { get; set; }
        public int IdType { get; set; }
        public int IdProduct { get; set; }
        public int IdUnit { get; set; }
        public int Quantity { get; set; }
        public string Warehouse { get; set; }
        public string NameIngredients { get; set; }
        public Ingredients(int idIngredients, int idType, int idProduct, int idUnit, int quantity, string warehouse, string nameIngredients)
        {
            IdIngredients = idIngredients;
            IdType = idType;
            IdProduct = idProduct;
            IdUnit = idUnit;
            Quantity = quantity;
            Warehouse = warehouse;
            NameIngredients = nameIngredients;
        }
    }
}
