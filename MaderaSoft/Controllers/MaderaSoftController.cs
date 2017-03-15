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

        public MaderaSoftController(IUtilisateurService utilisateurService, IEmployeService employeService)
        {
            this._utilisateurService = utilisateurService;
            this._employeService = employeService;
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

            return RedirectToAction("Login");
        }

        public ActionResult logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "MaderaSoft");
        }
    }
}