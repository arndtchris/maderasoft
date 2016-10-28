using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madera.Model
{
    public class Personne
    {
        public int PersonneID { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public Boolean isMoral { get; set; }
        public Boolean isDeleted { get; set; }
        public virtual Adresse adresse { get; set; }

        public Personne()
        {

        }
    }
}
