using System.Web.Mvc;

namespace MaderaSoft.Areas.RechercheDeveloppement
{
    public class RechercheDeveloppementAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RechercheDeveloppement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "RechercheDeveloppement_default",
                "RechercheDeveloppement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}