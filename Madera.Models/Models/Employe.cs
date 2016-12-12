using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madera.Model
{
    public class Employe
    {
        public int id { get; set; }
        public Boolean isDeleted { get; set; }
        public virtual TEmploye typeEmploye { get; set; }
        public virtual List<AffectationService> affectationServices { get; set; }
        //public virtual Personne personne { get; set;}

        public Employe()
        {

        }
    }
}
