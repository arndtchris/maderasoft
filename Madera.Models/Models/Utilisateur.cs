using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Madera.Model
{
    public class Utilisateur
    {
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public DateTime dCreation { get; set; }
        public DateTime dConnexion { get; set; }
        public bool isActive { get; set; }
        public bool isFirstConnexion { get; set; }
        public bool isDeleted { get; set; }

        public Utilisateur()
        {

        }
    }
}