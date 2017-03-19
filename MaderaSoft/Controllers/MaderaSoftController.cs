using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MaderaSoft.Models.ViewModel;
using AutoMapper;
using Madera.Service;
using MaderaSoft.Models;
using MaderaSoft.Models.DTO;
using System.Web.Mvc;
using Madera.Model;

namespace MaderaSoft.Controllers
{
    public class MaderaSoftController : Controller
    {
        private readonly IUtilisateurService _utilisateurService;
        private readonly IEmployeService _employeService;
        private readonly IApplicationTraceService _traceService;

        public MaderaSoftController(IUtilisateurService utilisateurService, IEmployeService employeService, IApplicationTraceService traceService)
        {
            this._utilisateurService = utilisateurService;
            this._employeService = employeService;
            this._traceService = traceService;
        }

        // GET: MaderaSoft
        public ActionResult Index()
        {
            Models.ViewModel.LoginViewModel model = new Models.ViewModel.LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(UtilisateurLoginDTO loginUtilisateur)
        {
            Models.ViewModel.LoginViewModel model = new Models.ViewModel.LoginViewModel();
            EmployeDTO util = Mapper.Map<Employe, EmployeDTO>(_employeService.TrouveUtilisateur(loginUtilisateur.login, loginUtilisateur.password));

            if (util != null)
            {
                Session["utilisateur"] = util;

                _traceService.create(new ApplicationTrace
                {
                    utilisateur = _donneNomPrenomUtilisateur(),
                    action = Parametres.Action.Connexion.ToString(),
                    description = string.Format("Connexion de {0} {1}", util.nom.ToUpperFirst(), util.prenom.ToUpperFirst())
                });

                _traceService.save();

                return RedirectToAction("Index", "MaderaSoft");
            }

            model.loginUtilisateur.login = loginUtilisateur.login;
            model.loginUtilisateur.password = loginUtilisateur.password;

            model.notifications.Add(new Models.Notification
            {
                dureeNotification = Parametres.DureeNotification.Always,
                message = "Couple identifiant/mot de passe incorect",
                typeNotification = Parametres.TypeNotification.Danger
            });

            return RedirectToAction("Index", "MaderaSoft");
        }

        public ActionResult logout()
        {

            _traceService.create(new ApplicationTrace
            {
                utilisateur = _donneNomPrenomUtilisateur(),
                action = Parametres.Action.Deconnexion.ToString(),
                description = string.Format("Déconnexion de {0}", _donneNomPrenomUtilisateur())
            });

            _traceService.save();

            Session.Abandon();

            return RedirectToAction("Index", "MaderaSoft");
        }

        private string _donneNomPrenomUtilisateur()
        {
            EmployeDTO emp = (EmployeDTO)HttpContext.Session["utilisateur"];

            if (emp != null)
                return string.Format("{0} {1}", emp.nom.ToUpperFirst(), emp.prenom.ToUpperFirst());
            else
                return "";

        }
    }
}