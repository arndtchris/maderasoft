using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madera.Model
{
    public class Projet
    {
        public int ProjetID { get; set; }
        public string libe { get; set; }
        public float prixHT { get; set; }
        public float prixTotalTTC { get; set; }
        public bool isDeleted { get; set; }
        public bool isPaid { get; set; }

        public virtual Personne client { get; set; }
        public virtual Adresse adresse { get; set; }
        public virtual Employe referent { get; set; }

        public Projet()
        {

        }
    }
}
