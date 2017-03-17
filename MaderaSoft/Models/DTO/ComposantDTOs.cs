using Madera.Model;
using MaderaSoft.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaderaSoft.Models.DTO
{

        // GET: GestionStock/Stocks
        public class ComposantDTO
        {
            public int id { get; set; }
            [DisplayName("Nom du composant")]
            public String libe { get; set; }
            [DisplayName("Quantité stock")]
            public int qteStock { get; set; }
            [DisplayName("Gamme")]
            public virtual Gamme gamme { get; set; }
            [DisplayName("Prix fournisseur")]
            public double prixHT { get; set; }
            [DisplayName("Nom fournisseur")]
            public virtual Personne fournisseur { get; set; }

            public ComposantDTO()
            {
            }
        }
    }
