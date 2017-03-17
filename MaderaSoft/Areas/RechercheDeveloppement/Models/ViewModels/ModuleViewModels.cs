using MaderaSoft.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaderaSoft.Models.DTO;

namespace MaderaSoft.Areas.RechercheDeveloppement.Models.ViewModels
{

    public class ModuleIndexViewModel
    {
        public BootstrapTableViewModel tableauModules { get; set; }
        public ModuleIndexViewModel()
        {
            tableauModules = new BootstrapTableViewModel();

        }
    }

    public class CreateModuleViewModel
    {

        public virtual ModuleDTO module { get; set; }
        public int idComposant { get; set; }
        public List<SelectListItem> lesGammes { get; set; }
        public List<SelectListItem> lesComposants { get; set; }


        public CreateModuleViewModel()
        {
            module = new ModuleDTO();
            lesGammes = new List<SelectListItem>();
            lesComposants = new List<SelectListItem>();


        }
    }
    public class EditModuleViewModel : CreateModuleViewModel
    {
        public override ModuleDTO module { get; set; }

        public EditModuleViewModel()
        {
            module = new EditModuleDTO();
        }
    }
}