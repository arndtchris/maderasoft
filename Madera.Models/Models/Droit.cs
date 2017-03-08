using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madera.Model
{
    public class Droit
    {
        public int id { get; set; }
        public Boolean create { get; set; }
        public Boolean update { get; set; }
        public Boolean read { get; set; }
        public Boolean delete { get; set; }
        public Boolean softDelete { get; set; }
        public string libe { get; set; }

        public Droit()
        {

        }
    }
}
