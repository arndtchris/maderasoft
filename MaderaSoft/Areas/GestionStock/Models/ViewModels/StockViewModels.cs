﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Madera.Model;
using MaderaSoft.Models.DTO;
using MaderaSoft.Models.ViewModel;

namespace MaderaSoft.Areas.GestionStock.Models.ViewModels
{

    public class StockIndexViewModel
    {
        public BootstrapTableViewModel tableauComposants { get; set; }
        public StockIndexViewModel()
        {
            tableauComposants = new BootstrapTableViewModel();

        }
    }

    public class CreateStockViewModel
    {

        public virtual ComposantDTO composant { get; set; }
        public List<SelectListItem> lesGammes { get; set; }
        public List<SelectListItem> lesFournisseurs { get; set; }


        public CreateStockViewModel()
        {
            composant = new ComposantDTO();
            lesGammes = new List<SelectListItem>();
            lesFournisseurs = new List<SelectListItem>();


        }
    }


}