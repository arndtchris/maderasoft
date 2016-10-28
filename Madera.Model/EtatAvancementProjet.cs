using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madera.Model
{
    public class EtatAvancementProjet
    {
        public int EtatAvancementProjetID { get; set; }
        public string libe { get; set; }
        public double pourcentageADebloquer { get; set; }

        public EtatAvancementProjet()
        {

        }
    }
}
