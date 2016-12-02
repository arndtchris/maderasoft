using System.Web.Mvc;

namespace MaderaSoft.Areas.RessourcesHumaines
{
    public class RessourcesHumainesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RessourcesHumaines";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "RessourcesHumaines_default",
                "RessourcesHumaines/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}