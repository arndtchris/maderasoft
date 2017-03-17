using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MaderaSoft.Models.DTO;

namespace MaderaSoft.Helpers
{
    public class DroitHelper
    {
        public static bool utilisateurPeutCreerSurCeService()
        {
            EmployeDTO user = (EmployeDTO)HttpContext.Current.Session["utilisateur"];
            string service = (string)HttpContext.Current.Session["service"];
            AffectationServiceDTO surCeService = user.affectationServices.FirstOrDefault(x => x.service.libe.ToUpper() == service.ToUpper());

            if (surCeService != null)
                return surCeService.groupe.create;
            else
                return false;

        }
    }
}