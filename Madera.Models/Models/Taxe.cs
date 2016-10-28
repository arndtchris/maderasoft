using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madera.Model
{
    public class Taxe
    {
        public int TaxeID { get; set; }
        public double pourcentage { get; set; }
        public string libe { get; set; }
        public Boolean isReduction { get; set; }

        public Taxe()
        {
            
        }
    }
}
