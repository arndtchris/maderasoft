using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaderaSoft.Models.DTO
{
    public class PlanDTO
    {
            public List<ComposantPlan> listComposants { get; set; }

    }
    public class ComposantPlan
    {
        public string Id { get; set; }
        public int quantite { get; set; }
    }
}