using System.Web.Mvc;

namespace MaderaSoft.Areas.GestionModule
{
    public class GestionModuleAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "GestionModule";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "GestionModule_default",
                "GestionModule/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}