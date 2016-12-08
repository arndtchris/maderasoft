using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Madera.Model;
using MaderaSoft.Models.DTO;
using MaderaSoft.Models.ViewModel;

namespace MaderaSoft.Areas.RessourcesHumaines.Models.ViewModels
{
    public class EmployeIndexViewModel
    {
        public BootstrapTableViewModel tableauEmployes { get; set; }

        public EmployeIndexViewModel()
        {
            tableauEmployes = new BootstrapTableViewModel();
        }
    }

    public class EditEmployeViewModel
    {
        public PersonneDTO personne { get; set; }
        public EditEmployeViewModel()
        {
            personne = new PersonneDTO();
        }
    }
}