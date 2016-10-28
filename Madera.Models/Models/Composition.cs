using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madera.Model
{
    public class Composition
    {
        public int CompositionID { get; set; }
        public int qte { get; set; }
        public virtual Module module { get; set; }
        public virtual Composent composent { get; set; }

        public Composition()
        {

        }
    }
}
