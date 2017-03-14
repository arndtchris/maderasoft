using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaderaSoft.Models.DTO
{
    public class PlanDTO
    {
        public int id { get; set; }
        public int largeur { get; set; }
        public int longueur { get; set; }
        public string nom { get; set; }

        public virtual List<EtageDTO> lesEtages { get; set; }

        public PlanDTO()
        {
            lesEtages = new List<EtageDTO>();
        }

    }
}