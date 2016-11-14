using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaderaSoft.Models.Bootstrap
{
    public class BootstrapDeleteModalModel
    {
        public int idToDelete { get; set; }

        public string typeObjet { get; set; }

        public string message { get; set; }

        public BootstrapDeleteModalModel()
        {
            
        }
    }
}