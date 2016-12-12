using System.Web.Mvc;

namespace MaderaSoft.Areas.Simulateur
{
    public class SimulateurAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Simulateur";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Simulateur_default",
                "Simulateur/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}