using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madera.Model
{
    public class Composent
    {

        public int ComposentID { get; set; }
        public string libe { get; set; }
        public Boolean isDeleted { get; set; }
        public double prixHT { get; set; }
        public virtual Gamme gamme { get; set; }
        public virtual Fournisseur fournisseur { get; set; }

        public Composent()
        {

        }
    }
}
