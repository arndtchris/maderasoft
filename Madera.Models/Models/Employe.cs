using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madera.Model
{
    public class Employe : Personne
    {
        public virtual TEmploye typeEmploye { get; set; }
        public virtual List<AffectationService> affectationServices { get; set; }

        public Employe()
        {

        }
    }
}
