using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madera.Model
{
    public class Fournisseur : Personne
    {
        public int FournisseurID { get; set; }
        public string telephone { get; set; }

        public Fournisseur()
        {

        }
    }
}
