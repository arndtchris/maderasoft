using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Madera.Model
{
    public class PositionModule
    {
        public int id { get; set; }
        public int x1 { get; set; }
        public int x2 { get; set; }
        public int y1 { get; set; }
        public int y2 { get; set; }

        public int lineId { get; set; }

        public virtual Module module { get; set; }
        public virtual Etage etage { get; set; }

        public PositionModule() { }

    }
}