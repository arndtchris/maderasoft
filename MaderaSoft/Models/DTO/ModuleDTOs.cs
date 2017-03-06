using Madera.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaderaSoft.Models.DTO
{
    public class ModuleDTO
    {
        public string libe { get; set; }

        //il faudra le passer en int pour la suite
        public int id { get; set; }
        public string coupePrincipe { get; set; }
        public TModule typeModule { get; set; }
    }
}