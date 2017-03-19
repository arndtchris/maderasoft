using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Madera.Service
{
    public static class Parametres
    {
        public enum Action
        {
            Connexion,
            Deconnexion,
            Creation,
            Modification,
            SuppressionLogique,
            Suppression
        }

        public enum TypeBouton
        {
            Creation,
            Modification,
            Suppression,
            DissmissModal,
            Submit,
            Detail
        }

        public enum TypeNotification
        {
            Success,
            Warning,
            Danger,
            Information
        }

        public enum DureeNotification
        {
            FadeOut,
            Always
        }

        public const string defaultPassword = "123456789*";
    }
}