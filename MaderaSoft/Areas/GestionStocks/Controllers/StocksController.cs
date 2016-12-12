using Madera.Model;
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
            [DisplayName("Code produit")]
            public int id { get; set; }
            [DisplayName("Nom du composant")]
            public String libe { get; set; }
            [DisplayName("Quantité")]
            public int qte { get; set; }
            [DisplayName("Gamme")]
            public virtual Gamme gamme { get; set; }
            [DisplayName("Prix fournisseur")]
            public double prixHT { get; set; }
            [DisplayName("Nom fournisseur")]
            public virtual Utilisateur fournisseur { get; set; }

            public StocksDTO()
            {
            }
        }
    }
}