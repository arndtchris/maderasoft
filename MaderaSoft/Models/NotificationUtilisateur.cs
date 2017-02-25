using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Madera.Service;

namespace MaderaSoft.Models
{
    /// <summary>
    /// Cet objet permet d'afficher des messages
    /// </summary>
    public abstract class NotificationUtilisateur
    {
        public List<Notification> notifications { get; set; }

        public NotificationUtilisateur()
        {
            notifications = new List<Notification>();
        }
    }

    /// <summary>
    /// Défini les attributs nécessaire à la génération d'une notification destinée aux utilisateurs
    /// Une notification dont la durée est FadeOut disparait au bout de 6000ms
    /// </summary>
    public class Notification
    {
        public string message { get; set; }

        public Parametres.TypeNotification typeNotification { get; set; }

        public Parametres.DureeNotification dureeNotification { get; set; }

        public Notification()
        {

        }
    }
}