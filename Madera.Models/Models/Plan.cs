using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Madera.Model
{
    public class Plan
    {
        public int id { get; set; }
        public virtual List <Etage> listEtages { get; set; }

        public Plan() { }

    }
}