using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Madera.Model;
using Madera.Service;
using MaderaSoft.Models;

namespace MaderaSoft.Controllers
{
    public class AdresseController : Controller
    {
        private readonly IAdresseService adresseService;

        public AdresseController(IAdresseService adresseService)
        {
            this.adresseService = adresseService;
        }

        // GET: Adresse
        public ActionResult Index()
        {
            IEnumerable<AdresseModel> adresseModels = Mapper.Map<IEnumerable<Adresse>, IEnumerable<AdresseModel>>(adresseService.GetAdresses().ToList());

            return View(adresseModels);
        }
    }
}