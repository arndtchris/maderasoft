using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madera.Model
{
    public class Projet
    {
        public int id { get; set; }
        public string libe { get; set; }
        public float prixHT { get; set; }
        public float prixTotalTTC { get; set; }
        public bool isDeleted { get; set; }
        public bool isPaid { get; set; }

        public virtual Personne client { get; set; }
        public virtual Adresse adresse { get; set; }
        public virtual Employe referent { get; set; }

        public Projet(int i, string l, float pHT, float pTTC, bool deleted, bool paid, Personne cl, Adresse ad, Employe referent)
        {
            this.id = i;
            this.libe = l;
            this.prixHT = pHT;
            this.prixTotalTTC = pTTC;
            this.isDeleted = deleted;
            this.isPaid = paid;
            this.client = cl;
            this.adresse = ad;
            this.referent = referent;
        }
    }
}
