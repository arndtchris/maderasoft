using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaderaSoft.Models.DTO
{
    public class PlanDTO
    {
        public int largeur { get; set; }
        public int longueur { get; set; }

        public List<EtageDTO> lesEtages { get; set; }

        public PlanDTO()
        {
            lesEtages = new List<EtageDTO>();
        }

    }
    public class ComposantPlan
    {
        public string Id { get; set; }
        public int quantite { get; set; }
    }
}