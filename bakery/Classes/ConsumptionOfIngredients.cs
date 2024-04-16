using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bakery.Classes
{
    public class ConsumptionOfIngredients
    {
        public int IdConsumption { get; set; }
        public int IdPlan { get; set; }
        public ConsumptionOfIngredients(int idConsumption, int idPlan)
        {
            IdConsumption = idConsumption;
            IdPlan = idPlan;
        }
    }
}
