using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madera.Model
{
    public class AffectationService
    {
        public int AffectationServiceID { get; set; }

        public virtual Employe employe { get; set; }
        public virtual Service service { get; set; }
        public virtual Droit groupe { get; set; }
        public Boolean isPrincipal { get; set; }
    }
}
