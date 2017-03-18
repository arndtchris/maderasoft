using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Madera.Model;
using Madera.Service;
using MaderaSoft.Areas.RessourcesHumaines.Models.ViewModels;
using MaderaSoft.Models.DTO;
using Vereyon.Web;

namespace MaderaSoft.Areas.RessourcesHumaines.Controllers
{
    public class GestionDroitController : Controller
    {
        private readonly IDroitService _droitService;
        private readonly IEmployeService _employeService;

        public GestionDroitController(IDroitService droitService, IEmployeService employeService)
        {
            this._droitService = droitService;
            this._employeService = employeService;
        }

        // GET: RessourcesHumaines/GestionDroit
        [HttpGet]
        public ActionResult Index()
        {
            GestionDroitViewModel modelout = new GestionDroitViewModel();

            modelout.lesDroits = Mapper.Map<List<Droit>, List<DroitDTO>>(_droitService.DonneTous().ToList());

            return View(modelout);
        }

        [HttpPost]
        public ActionResult Edit(DroitDTO modelin)
        {
            DroitDTO modelout = new DroitDTO();

            try
            {
                _droitService.Update(Mapper.Map<DroitDTO, Droit>(modelin), _donneNomPrenomUtilisateur());
                _droitService.Save();
                _updateSession();

                FlashMessage.Confirmation("Mise à jour du groupe utilisateur avec succès");
            }
            catch (Exception e)
            {
                FlashMessage.Confirmation("Erreur de la mise à jour du groupe utilisateur");

                return PartialView("~/Areas/RessourcesHumaines/Views/GestionDroit/_ParametreDroitPartial.cshtml", modelin);
            }

            modelout = Mapper.Map<Droit, DroitDTO>(_droitService.Get(modelin.id));

            return PartialView("~/Areas/RessourcesHumaines/Views/GestionDroit/_ParametreDroitPartial.cshtml", modelout);

        }

        private string _donneNomPrenomUtilisateur()
        {
            EmployeDTO emp = (EmployeDTO)HttpContext.Session["utilisateur"];

            if (emp != null)
                return string.Format("{0} {1}", emp.nom.ToUpperFirst(), emp.prenom.ToUpperFirst());
            else
                return "";

        }

        private void _updateSession()
        {
            EmployeDTO emp = (EmployeDTO)HttpContext.Session["utilisateur"];
            Session["utilisateur"] = Mapper.Map<Employe, EmployeDTO>(_employeService.Get(emp.id));

        }
    }
}