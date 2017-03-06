using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Madera.Model
{
    public class Plan
    {
        public int id { get; set; }
        public int largeur { get; set; }
        public int longueur { get; set; }
        public string nom { get; set; }
        public virtual List<Etage> listEtages { get; set; }

        public Plan() { }

    }
}