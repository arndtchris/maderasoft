using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madera.Model
{
    public class Employe : Personne
    {
        public int EmployeID { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public Boolean isDeleted { get; set; }
        public string telephone { get; set; }
        public string email { get; set; }
        public virtual TEmploye typeEmploye { get; set; }
        public virtual List<AffectationService> affectationServices { get; set; }

        public Employe()
        {

        }
    }
}
