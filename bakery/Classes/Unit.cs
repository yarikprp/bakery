using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bakery.Classes
{
    public class Unit
    {
        public int IdUnit { get; set; }
        public string NameUnit { get; set; }
        public Unit(int idUnit, string nameUnit)
        {
            IdUnit = idUnit;
            NameUnit = nameUnit;
        }
    }
}
