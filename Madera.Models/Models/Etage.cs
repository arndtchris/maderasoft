using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Madera.Model
{
    public class Etage
    {
        public int id { get; set; }
        public virtual List<PositionModule> listPositionModule { get; set; }
        public virtual Plan plan { get; set; }

        public Etage() { }
    }
}