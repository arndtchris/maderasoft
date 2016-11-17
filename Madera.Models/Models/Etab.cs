using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madera.Model
{
    public class Etab
    {
        public int id { get; set; }
        public string libe { get; set; }
        public virtual TEtab type { get; set; }
        public virtual Adresse adresse { get; set; }

        public Etab()
        {

        }
    }
}
