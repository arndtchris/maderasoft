using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaderaSoft.Areas.GestionStocks.Controllers
{
    public class StocksController : Controller
    {
        // GET: GestionStocks/Stocks
        public class StocksDTO
        {
            [DisplayName("Numéro Devis")]
            public int id { get; set; }
            [DisplayName("Devis signé")]
            public Boolean isSigned { get; set; }
            [DisplayName("Devis supprimé")]
            public Boolean isDeleted { get; set; }
            [DisplayName("Numéro projet")]
            public virtual Projet projet { get; set; }
            [DisplayName("Référent")]
            public virtual Employe employe { get; set; }

            public StocksDTO()
            {
            }
        }
    }
}