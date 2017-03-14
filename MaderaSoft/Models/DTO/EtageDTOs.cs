using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaderaSoft.Models.DTO
{
    public class EtageDTO
    {
        public int id { get; set; }
        public virtual List<PositionModuleDTO> lesModules { get; set; }
        public virtual PlanDTO plan { get; set; }

        public EtageDTO()
        {
            lesModules = new List<PositionModuleDTO>();
            plan = new PlanDTO();
        }
    }
}