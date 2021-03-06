﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madera.Model
{
    public class Composant
    {

        public int id { get; set; }
        public string libe { get; set; }
        public int qteStock { get; set; }
        public Boolean isDeleted { get; set; }
        public double prixHT { get; set; }
        public virtual Gamme gamme { get; set; }
        public virtual Personne fournisseur { get; set; }

        public Composant()
        {

        }
    }
}
