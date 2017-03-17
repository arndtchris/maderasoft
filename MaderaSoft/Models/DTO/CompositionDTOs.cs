using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaderaSoft.Models.DTO
{
    public class CompositionDTO
    {
        public int id { get; set; }
        public virtual ComposantDTO composant { get; set; }
        public virtual ModuleDTO module { get; set; }


        public CompositionDTO()
        {
            composant = new ComposantDTO();
            module = new ModuleDTO();
        }
    }
}