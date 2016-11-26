using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Madera.Service;
using MaderaSoft.Models.Bootstrap;

namespace System.Web.Mvc
{
    public static class BootstrapHelper
    {
        public static void bootstrapTable(this HtmlHelper helper, BootstrapTableModel modelIn)
        {
            helper.RenderPartial("~/Views/Shared/_BootstrapTable.cshtml", modelIn);
        }


        //ToDo : voir pourquoi le helper pour la génération de bouton ne fonctionne pas...
        public static MvcHtmlString addButton(this HtmlHelper helper, string libe, string href, string cssClass, Parametres.TypeBouton type)
        {
            string css = "";
            switch(type)
            {
                case Parametres.TypeBouton.Creation:
                    css = cssClass + " btn btn-success fa fa-plus\"";
                    break;
                case Parametres.TypeBouton.Modification:
                    css = cssClass + " btn btn-warning fa fa-pencil\"";
                    break;
                case Parametres.TypeBouton.Suppression:
                    css = cssClass + " btn btn-danger fa fa-trash\"";
                    break;
                default:
                    break;
            }
            return new MvcHtmlString("<a class=\"" + css + " href=\"" + href + "\" > " + libe + "</a>");
        }
    }
}