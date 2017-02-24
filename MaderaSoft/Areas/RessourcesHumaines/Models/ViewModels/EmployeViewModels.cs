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
        public EditEmployeDTO personne { get; set; }
        public BootstrapTableViewModel lesAffectationsEmploye { get; set; }
        public List<SelectListItem> lesServices { get; set; }
        public List<SelectListItem> lesDroits { get; set; }
        public List<SelectListItem> lesCivilites { get; set; }
        public List<SelectListItem> lesTypesEmployes { get; set; }

        public EditEmployeViewModel()
        {
            personne = new EditEmployeDTO();
            lesTypesEmployes = new List<SelectListItem>();
            lesAffectationsEmploye = new BootstrapTableViewModel();
            lesAffectationsEmploye.messageSiVide = "Cet employé n'a pas encore d'affectation";
            lesAffectationsEmploye.avecActionCrud = false;
            lesCivilites = new List<SelectListItem> {
                new SelectListItem {Value = "",Text = "--- Sélectionnez ---" },
                new SelectListItem {Value = "1",Text = "Madame" },
                new SelectListItem {Value = "2",Text = "Monsieur" }
            };
        }
    }

    public class CardEmployeViewModel
    {
        public EmployeSimpleDTO employe { get; set; }

        public List<SelectListItem> lesTypesEmployes { get; set; }

    }


    public class DetailEmployeViewModel
    {
        public CardAffectationServiceViewModel cardAffectations { get; set; }

        public CardEmployeViewModel cardEmploye { get; set; }

        public AdresseDTO adresse { get; set; }
    }
}