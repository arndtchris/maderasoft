using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madera.Model
{
    public class HistoriqueProjet
    {
        public int HistoriqueProjetID { get; set; }
        public virtual Projet projet { get; set; }
        public virtual EtatAvancementProjet avancementProjet { get; set; }

        public HistoriqueProjet()
        {

        }
    }
}
