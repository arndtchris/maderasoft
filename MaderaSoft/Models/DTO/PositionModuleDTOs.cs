using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaderaSoft.Models.DTO
{
    public class PositionModuleDTO
    {
        public int id { get; set; }

        public int x1 { get; set; }
        public int x2 { get; set; }

        public int y1 { get; set; }
        public int y2 { get; set; }

        public string lineId { get; set; }

        public ModuleDTO module { get; set; }
        public EtageDTO etage { get; set; }

        public PositionModuleDTO()
        {
            module = new ModuleDTO();
        }
    }
}