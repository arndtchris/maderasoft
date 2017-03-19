using System.Web.Mvc;

namespace MaderaSoft.Areas.ServiceCommercial
{
    public class ServiceCommercialAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ServiceCommercial";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ServiceCommercial_default",
                "ServiceCommercial/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}