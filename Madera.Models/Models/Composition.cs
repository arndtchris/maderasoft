﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madera.Model
{
    public class Composition
    {
        public int id { get; set; }
        public int qte { get; set; }
        public virtual Composant composant { get; set; }

        public Composition()
        {

        }
    }
}
