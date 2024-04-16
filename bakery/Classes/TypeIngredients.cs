using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bakery.Classes
{
    public class TypeIngredients
    {
        public int IdType { get; set; }
        public string NameIngredients { get; set; }
        public TypeIngredients(int idType, string nameIngredients)
        {
            IdType = idType;
            NameIngredients = nameIngredients;
        }
    }
}
