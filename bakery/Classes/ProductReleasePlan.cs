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
        public string NameProduct { get; set; }
        public string Fio { get; set; }
        public DateTime PlannedReleaseDate { get; set; }
        public ProductReleasePlan(int idPlan, string nameProduct, string fio, DateTime plannedReleaseDate)
        {
            IdPlan = idPlan;
            NameProduct = nameProduct;
            Fio = fio;
            PlannedReleaseDate = plannedReleaseDate;
        }
    }
}
