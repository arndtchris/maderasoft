using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madera.Model
{
    public class Composition
    {
        public int id { get; set; }
        public virtual Composant composant { get; set; }
        public virtual Module module { get; set; }
        public Composition()
        {

        }
    }
}
