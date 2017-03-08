using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaderaSoft.Models.DTO;

namespace MaderaSoft.Areas.RessourcesHumaines.Models.ViewModels
{
    public class GestionDroitViewModel
    {
        public List<DroitDTO> lesDroits { get; set; }

        public GestionDroitViewModel()
        {
            lesDroits = new List<DroitDTO>();
        }
    }
}