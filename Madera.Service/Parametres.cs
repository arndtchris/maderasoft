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
            Connection,
            Deconnection,
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
    }
}