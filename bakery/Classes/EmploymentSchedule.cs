using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bakery.Classes
{
    public class EmploymentSchedule
    {
        public int IdEmploymentSchedule {  get; set; }
        public string NameEmploymentSchedule { get; set; }
        public EmploymentSchedule(int idEmploymentSchedule, string nameEmploymentSchedule) 
        {
            IdEmploymentSchedule = idEmploymentSchedule;
            NameEmploymentSchedule= nameEmploymentSchedule;
        }
    }
}
