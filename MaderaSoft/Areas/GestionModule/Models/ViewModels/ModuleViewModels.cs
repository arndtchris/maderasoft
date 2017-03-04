using MaderaSoft.Areas.GestionModule.Models.DTOs;
using MaderaSoft.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaderaSoft.Areas.GestionModule.Models.ViewModels
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
        public List<SelectListItem> lesGammes { get; set; }


        public CreateModuleViewModel()
        {
            module = new ModuleDTO();
            lesGammes = new List<SelectListItem>();


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