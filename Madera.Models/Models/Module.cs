using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madera.Model
{
    public class Module
    {
        public int id { get; set; }
        public string libe { get; set; }
        public string coupePrincipe { get; set; }
        public virtual TModule typeModule { get; set; }

        public Module()
        {

        }
    }
}
