using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MaderaSoft.Models;
using System.Web.Mvc.Html;
using Madera.Service;

namespace System.Web.Mvc
{
/// <summary>
/// Helper permettant d'afficher les notifications utilisateur présentent dans un viewmodel
/// </summary>
    public static class NotificationHelper
    {
        /// <summary>
        /// Permet d'afficher une liste de notifications
        /// </summary>
        /// <param name="lesNotifications"></param>
        /// <returns></returns>
        public static MvcHtmlString AfficheNotificationUtilisateur(this HtmlHelper helper, List<Notification> lesNotifications)
        {
            string notifications = "";

            if(lesNotifications != null && lesNotifications.Count > 0)
                foreach (Notification notif in lesNotifications)
                {
                    notifications += _genereNotification(notif).ToString();
                }


            return new MvcHtmlString(notifications);
        }

        /// <summary>
        /// Permet d'encapsuler une notification au sein de code HTML
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        private static MvcHtmlString _genereNotification(Notification notification)
        {
            string classCss = "notification ";
            string icon = "";

            switch(notification.dureeNotification)
            {
                case Parametres.DureeNotification.FadeOut:
                    classCss += "alert ";
                    break;
                case Parametres.DureeNotification.Always:
                    classCss += "";
                    break;
            }

            switch (notification.typeNotification)
            {
                case Parametres.TypeNotification.Success:
                    classCss += "bg-green disabled color-palette ";
                    icon = "<i class=\"fa fa-check\" aria-hidden=\"true\"></i> ";
                    break;
                case Parametres.TypeNotification.Warning:
                    classCss += "bg-yellow disabled color-palette ";
                    icon = "<i class=\"fa fa-exclamation-triangle\" aria-hidden=\"true\"></i> ";
                    break;
                case Parametres.TypeNotification.Danger:
                    classCss += "bg-red disabled color-palette ";
                    icon = "<i class=\"fa fa-stop-circle\" aria-hidden=\"true\"></i> ";
                    break;
                case Parametres.TypeNotification.Information:
                    classCss += "bg-aqua disabled color-palette";
                    icon = "<i class=\"fa fa-info-circle\" aria-hidden=\"true\"></i> ";
                    break;
            }

            return new MvcHtmlString(
                "<div class=\"" + classCss +"\"><p class=\"p-notification\">"+ icon + notification.message + "</p></div>"
                );

        }
    }
}