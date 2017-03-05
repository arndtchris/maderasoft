using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madera.Model
{
    public class Personne
    {
        public int id { get; set; }
        public string civ { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string email { get; set; }
        public string tel1 { get; set; }
        public string tel2 { get; set; }
        public Boolean isFournisseur { get; set; }
        public Boolean isClient { get; set; }
        public Boolean isDeleted { get; set; }
        public virtual Adresse adresse { get; set; }
        public virtual Utilisateur utilisateur { get; set; }

        public Personne()
        {

        }
    }
}
