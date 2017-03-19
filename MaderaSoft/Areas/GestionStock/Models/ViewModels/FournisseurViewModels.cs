using MaderaSoft.Models.DTO;
using MaderaSoft.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaderaSoft.Areas.GestionStock.Models.ViewModels
{
    public class FournisseurIndexViewModel
    {

        public BootstrapTableViewModel tableauFournisseurs { get; set; }
        public FournisseurIndexViewModel()
        {
            tableauFournisseurs = new BootstrapTableViewModel();

        }
    }

    public class CreateFournisseurViewModel
    {

        public virtual PersonneDTO fournisseur { get; set; }

        public CreateFournisseurViewModel()
        {
            fournisseur = new PersonneDTO();



        }
    }
}