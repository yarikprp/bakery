using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bakery.Classes
{
    public class ProductReleasePlan
    {
        public int IdPlan { get; set; }
        public int IdProduct { get; set; }
        public int IdEmployee { get; set; }
        public DateTime PlannedReleaseDate { get; set; }
        public ProductReleasePlan(int idPlan, int idProduct, int idEmployee, DateTime plannedReleaseDate)
        {
            IdPlan = idPlan;
            IdProduct = idProduct;
            IdEmployee = idEmployee;
            PlannedReleaseDate = plannedReleaseDate;
        }
    }
}
