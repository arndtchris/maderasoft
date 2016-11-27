using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Madera.Service;
using MaderaSoft.Models.ViewModel;

namespace System.Web.Mvc
{
    public static class BootstrapHelper
    {
        public static void bootstrapTable(this HtmlHelper helper, BootstrapTableViewModel modelIn)
        {
            helper.RenderPartial("~/Views/Shared/_BootstrapTablePartial.cshtml", modelIn);
        }

        public static MvcHtmlString actionButton(this HtmlHelper helper, string libe, Parametres.TypeBouton type, string href = "", string cssClass = "")
        {
            string css = "";
            switch(type)
            {
                case Parametres.TypeBouton.Creation:
                    css = cssClass + " btn btn-success fa fa-plus\" type=\"button\"";
                    break;
                case Parametres.TypeBouton.Submit:
                    css = cssClass + " btn btn-success fa fa-check\" type=\"submit\"";
                    break;
                case Parametres.TypeBouton.Modification:
                    css = cssClass + " btn btn-warning fa fa-pencil\" type=\"button\"";
                    break;
                case Parametres.TypeBouton.Suppression:
                    css = cssClass + " btn btn-danger fa fa-trash\" type=\"button\"";
                    break;
                case Parametres.TypeBouton.DissmissModal:
                    css = cssClass + " btn btn-default\" data-dismiss=\"modal\" type=\"button\"";
                    break;
                default:
                    break;
            }
            return new MvcHtmlString("<button class=\"" + css + " href=\"" + href + "\" > " + libe + "</button>");
        }
    }
}