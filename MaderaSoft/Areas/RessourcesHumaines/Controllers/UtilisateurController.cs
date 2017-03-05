using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Madera.Model;
using Madera.Service;
using MaderaSoft.Models.DTO;
using MaderaSoft.Models.ViewModel;
using Vereyon.Web;

namespace MaderaSoft.Areas.RessourcesHumaines.Controllers
{
    public class UtilisateurController : Controller
    {
        private readonly IEmployeService _employeService;
        private readonly IUtilisateurService _utilisateurService;
        private readonly IPersonneService _personneService;


        public UtilisateurController(IEmployeService employeService, IUtilisateurService utilisateurService, IPersonneService personneService)
        {
            this._employeService = employeService;
            this._utilisateurService = utilisateurService;
            this._personneService = personneService;
        }



        // GET: RessourcesHumaines/Utilisateur
        /*public ActionResult Index()
        {
            return View();
        }*/

        /// <summary>
        /// Méthode utilisée depuis un appel ajax pour créer un compte utilisateur depuis la ficeh détaillée d'un employé
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /*public ActionResult CreateUtilisateurDetail(int id)
        {
            EmployeDTO employe = new EmployeDTO();
            CardEmployeUtilisateurViewModel modelOut = new CardEmployeUtilisateurViewModel();
            employe = Mapper.Map<Employe, EmployeDTO>(_employeService.Get(idEmploye));

            try
            {
                employe.utilisateur.login = employe.nom.ToUpperFirst() + '.' + employe.prenom.ToUpper().First();
                employe.utilisateur.password = Parametres.defaultPassword;

                _employeService.Update(Mapper.Map<EmployeDTO, Employe>(employe));
                _employeService.Save();
                FlashMessage.Confirmation("Actualisation du compte utilisateur avec succès");

                modelOut.utilisateur = employe.utilisateur;


            }catch(Exception e)
            {

                modelOut.utilisateur = employe.utilisateur;

                FlashMessage.Danger("Erreur lors de la création du compte utilisateur");

                return PartialView("~/Areas/RessourcesHumaines/Views/Employe/_CardUtilisateurPartial.cshtml", modelOut);
            }

            return PartialView("~/Areas/RessourcesHumaines/Views/Employe/_CardUtilisateurPartial.cshtml", modelOut);
        }*/


        /// <summary>
        /// Méthode utilisée depuis un appel ajax pour réinitialiser le mot de passe d'un employé depuis sa fiche détaillée
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ResetPwdEmployeDetail(int id)
        {
            UtilisateurDTO utilisateur = new UtilisateurDTO();
            CardEmployeUtilisateurViewModel modelOut = new CardEmployeUtilisateurViewModel();

            try
            {

                _utilisateurService.ResetPwd(id);
                _utilisateurService.Save();
                utilisateur = Mapper.Map<Utilisateur, UtilisateurDTO>(_utilisateurService.Get(id));

                modelOut.notifications.Add(new MaderaSoft.Models.Notification {
                    dureeNotification = Parametres.DureeNotification.Always,
                    message = "L'utilisateur doit changer le mot de passe par défaut",
                    typeNotification = Parametres.TypeNotification.Warning
                });

                FlashMessage.Confirmation("Mot de passe réinitialisé");

                modelOut.utilisateur = utilisateur;
            }
            catch(Exception e)
            {
                modelOut.utilisateur = utilisateur;

                FlashMessage.Danger("Erreur lors du changement de mot de passe");

                return PartialView("~/Areas/RessourcesHumaines/Views/Employe/_CardUtilisateurPartial.cshtml", modelOut);
            }
            return PartialView("~/Areas/RessourcesHumaines/Views/Employe/_CardUtilisateurPartial.cshtml", modelOut);
        }

        /// <summary>
        /// Méthode utilisée dépuis un appel ajax pour désactiver un compte utilisateur depuis la fiche détaillée d'un employé
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DesactiveEmployeDetail(int id)
        {
            UtilisateurDTO utilisateur = new UtilisateurDTO();
            CardEmployeUtilisateurViewModel modelOut = new CardEmployeUtilisateurViewModel();

            try
            {
                _utilisateurService.DesactiveUtilisateur(id);
                _utilisateurService.Save();
                utilisateur = Mapper.Map<Utilisateur, UtilisateurDTO>(_utilisateurService.Get(id));
                FlashMessage.Confirmation("Compte utilisateur désactivé");

                modelOut.notifications.Add(new MaderaSoft.Models.Notification
                {
                    dureeNotification = Parametres.DureeNotification.Always,
                    message = "Le compte utilisateur est désactivé",
                    typeNotification = Parametres.TypeNotification.Warning
                });

                modelOut.utilisateur = utilisateur;
            }
            catch(Exception e)
            {
                FlashMessage.Danger("Erreur lors de la désactivation");

                modelOut.utilisateur = utilisateur;
                return PartialView("~/Areas/RessourcesHumaines/Views/Employe/_CardUtilisateurPartial.cshtml", modelOut);
            }

            return PartialView("~/Areas/RessourcesHumaines/Views/Employe/_CardUtilisateurPartial.cshtml", modelOut);
        }

        /// <summary>
        /// Méthode utilisée dépuis un appel ajax pour désactiver un compte utilisateur depuis la fiche détaillée d'un employé
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ActiveEmployeDetail(int id)
        {
            UtilisateurDTO utilisateur = new UtilisateurDTO();
            CardEmployeUtilisateurViewModel modelOut = new CardEmployeUtilisateurViewModel();

            try
            {
                _utilisateurService.ActiveUtilisateur(id);
                _utilisateurService.Save();
                utilisateur = Mapper.Map<Utilisateur, UtilisateurDTO>(_utilisateurService.Get(id));
                FlashMessage.Confirmation("Compte utilisateur activé");

                modelOut.notifications.Add(new MaderaSoft.Models.Notification
                {
                    dureeNotification = Parametres.DureeNotification.Always,
                    message = "Le compte utilisateur est actif",
                    typeNotification = Parametres.TypeNotification.Information
                });

                modelOut.utilisateur = utilisateur;
            }
            catch (Exception e)
            {
                FlashMessage.Danger("Erreur lors de l'activation");

                modelOut.utilisateur = utilisateur;
                return PartialView("~/Areas/RessourcesHumaines/Views/Employe/_CardUtilisateurPartial.cshtml", modelOut);
            }

            return PartialView("~/Areas/RessourcesHumaines/Views/Employe/_CardUtilisateurPartial.cshtml", modelOut);
        }


    }
}