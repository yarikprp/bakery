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
        public string TypeIngredients { get; set; }
        public string NameProduct { get; set; }
        public string NameUnit { get; set; }
        public int Quantity { get; set; }
        public string Warehouse { get; set; }
        public string NameIngredients { get; set; }
        public object Adress { get; internal set; }

        public Ingredients(int idIngredients, string typeIngredients, string nameProduct, string nameUnit, int quantity, string warehouse, string nameIngredients)
        {
            IdIngredients = idIngredients;
            TypeIngredients = typeIngredients;
            NameProduct = nameProduct;
            NameUnit = nameUnit;
            Quantity = quantity;
            Warehouse = warehouse;
            NameIngredients = nameIngredients;
        }
    }
}
