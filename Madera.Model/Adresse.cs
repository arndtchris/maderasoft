﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madera.Model
{
    public class Adresse
    {
        public int AdresseID { get; set; }
        public string numRue { get; set; }
        public string nomRue { get; set; }
        public string codePostal { get; set; }
        public string ville { get; set; }
        public string pays { get; set; }

        public Adresse()
        {

        }
    }
}
