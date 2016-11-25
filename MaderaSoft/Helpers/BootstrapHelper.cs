﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
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
        public static MvcHtmlString addButton(this HtmlHelper helper, string libe, string href, string cssClass)
        {
            StringBuilder html = new StringBuilder();
            html.Append("<a class=\"" + cssClass + " btn btn-success fa fa-plus\" href=\"" + href + "\" > " + libe + "</a>");

            return new MvcHtmlString(html.ToString());
        }
    }
}