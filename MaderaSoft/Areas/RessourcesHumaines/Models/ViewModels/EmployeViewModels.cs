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
        public PersonneEmployeDTO personne { get; set; }
        public BootstrapTableViewModel lesAffectationsEmploye { get; set; }
        public List<SelectListItem> lesServices { get; set; }
        public List<SelectListItem> lesDroits { get; set; }
        public List<SelectListItem> lesCivilites { get; set; }
        public List<SelectListItem> lesTypesEmployes { get; set; }

        public EditEmployeViewModel()
        {
            personne = new PersonneEmployeDTO();
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

    public class DetailEmployeViewModel
    {
        public PersonneEmployeDTO personne { get; set; }
        public BootstrapTableViewModel lesAffectationsEmploye { get; set; }
        public List<SelectListItem> lesServices { get; set; }
        public List<SelectListItem> lesDroits { get; set; }
        public List<SelectListItem> lesTEmployes { get; set; }
        public List<SelectListItem> lesCivilites { get; set; }

        public DetailEmployeViewModel()
        {
            personne = new PersonneEmployeDTO();
            lesAffectationsEmploye = new BootstrapTableViewModel();
            lesServices = new List<SelectListItem>();
            lesDroits = new List<SelectListItem>();
            lesTEmployes = new List<SelectListItem>();

            lesCivilites = new List<SelectListItem> {
                new SelectListItem {Value = "",Text = "--- Sélectionnez ---" },
                new SelectListItem {Value = "1",Text = "Madame" },
                new SelectListItem {Value = "2",Text = "Monsieur" }
            };
        }
    }

 }