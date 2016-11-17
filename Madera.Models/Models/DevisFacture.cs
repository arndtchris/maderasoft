using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madera.Model
{
    public class DevisFacture
    {
        public int id { get; set; }
        public Boolean isSigned { get; set; }
        public Boolean isDeleted { get; set; }
        public virtual Projet projet { get; set; }
        public virtual Employe referent { get; set; }

        public DevisFacture()
        {

        }
    }
}
