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
        public List<SelectListItem> lesDroits { get; set; }

        public DefinitionDroitViewModel definitionDesDroits { get; set; }

        public GestionDroitViewModel()
        {
            lesDroits = new List<SelectListItem>();
        }
    }

    public class DefinitionDroitViewModel
    {
        public int idGroupe { get; set; }

        public List<PermissionGroupeDTO> lesPermissionsGroupe { get; set; }

        public DefinitionDroitViewModel()
        {
            lesPermissionsGroupe = new List<PermissionGroupeDTO>();
        }

    }
}