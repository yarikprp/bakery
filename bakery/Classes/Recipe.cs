using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bakery.Classes
{
    public class Recipe
    {
        public int IdRecipe { get; set; }
        public string NameRecipe { get; set; }
        public string NameDescription { get; set; }
        public int IdProduct { get; set; }
        public int IdIngredients { get; set; }
        public Recipe(int idRecipe, string nameRecipe, string nameDescription, int idProduct, int idIngredients)
        {
            IdRecipe = idRecipe;
            NameRecipe = nameRecipe;
            NameDescription = nameDescription;
            IdProduct = idProduct;
            IdIngredients = idIngredients;
        }
    }
}
