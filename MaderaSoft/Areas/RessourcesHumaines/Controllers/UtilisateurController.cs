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

                _utilisateurService.ResetPwd(id, _donneNomPrenomUtilisateur());
                _utilisateurService.Save();
                utilisateur = Mapper.Map<Utilisateur, UtilisateurDTO>(_utilisateurService.Get(id));

                if(utilisateur.isActive)
                {
                    modelOut.notifications.Add(new MaderaSoft.Models.Notification
                    {
                        dureeNotification = Parametres.DureeNotification.Always,
                        message = "Le compte utilisateur est actif",
                        typeNotification = Parametres.TypeNotification.Information
                    });
                }else
                {
                    modelOut.notifications.Add(new MaderaSoft.Models.Notification
                    {
                        dureeNotification = Parametres.DureeNotification.Always,
                        message = "Le compte utilisateur est désactivé",
                        typeNotification = Parametres.TypeNotification.Warning
                    });
                }

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
                _utilisateurService.DesactiveUtilisateur(id, _donneNomPrenomUtilisateur());
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
                _utilisateurService.ActiveUtilisateur(id, _donneNomPrenomUtilisateur());
                _utilisateurService.Save();
                utilisateur = Mapper.Map<Utilisateur, UtilisateurDTO>(_utilisateurService.Get(id));
                FlashMessage.Confirmation("Compte utilisateur activé");

                modelOut.notifications.Add(new MaderaSoft.Models.Notification
                {
                    dureeNotification = Parametres.DureeNotification.Always,
                    message = "Le compte utilisateur est actif",
                    typeNotification = Parametres.TypeNotification.Information
                });

                if(utilisateur.isFirstConnexion)
                {
                    modelOut.notifications.Add(new MaderaSoft.Models.Notification
                    {
                        dureeNotification = Parametres.DureeNotification.Always,
                        message = "L'utilisateur doit changer le mot de passe par défaut",
                        typeNotification = Parametres.TypeNotification.Warning
                    });
                }

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

        public HtmlString PersoPwd(PersoPwdDTO newpwd)
        {
            try
            {
                _utilisateurService.ChangePwd(newpwd.id, newpwd.pwd1, _donneNomPrenomUtilisateur());
                _utilisateurService.Save();
                _updateSession();
            }
            catch(Exception e)
            {
                return (new HtmlString("<p>Erreur lors de l'actualisation du mot de passe</p>"));
            }

            return (new HtmlString("<p>Actualisation du mot de passe avec succès</p>"));
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