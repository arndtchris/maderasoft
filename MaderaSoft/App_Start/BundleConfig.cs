using System.Web;
using System.Web.Optimization;

namespace MaderaSoft
{
    public class BundleConfig
    {
        // Pour plus d'informations sur le regroupement, visitez http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.2.3.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilisez la version de développement de Modernizr pour le développement et l'apprentissage. Puis, une fois
            // prêt pour la production, utilisez l'outil de génération (bluid) sur http://modernizr.com pour choisir uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/layoutcss").Include(
                      "~/Content/grayscale.min.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/layoutjs").Include(
                      "~/Scripts/grayscale.min.js"));

            bundles.Add(new StyleBundle("~/Content/admincss").Include(
                      "~/Content/Admin/AdminLTE.css",
                      "~/Content/Admin/skin-blue.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/adminlayoutjs").Include(
                      "~/Scripts/Admin/appScript.js",
                      "~/Scripts/Admin/adminLte.min.js",
                      "~/Scripts/Admin/SimulateurMaison.js"));
        }
    }
}
