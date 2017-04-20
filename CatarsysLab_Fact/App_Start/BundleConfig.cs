using System.Web;
using System.Web.Optimization;

namespace CatarsysLab_Fact
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/detect.js",
                        "~/Scripts/fastclick.js",
                        "~/Scripts/jquery.slimscroll.js",
                        "~/Scripts/jquery.blockUI.js",
                        "~/Scripts/waves.js",
                        "~/Scripts/wow.min.js",
                        "~/Scripts/jquery.nicescroll.js",
                        "~/Scripts/jquery.scrollTo.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/morris").Include(
                       "~/Plugins/morris/morris.min.js",
                       "~/Plugins/raphael/raphael-min.js"));

            bundles.Add(new ScriptBundle("~/bundles/counterup").Include(
                        "~/Plugins/waypoints/lib/jquery.waypoints.js",
                        "~/Plugins/counterup/jquery.counterup.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/jquerydash").Include(
                        "~/Scripts/jquery.dashboard.js",
                         "~/Scripts/jquery.core.js",
                          "~/Scripts/jquery.app.js"));


            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/plugins/morris/morris.css",
                         "~/Content/bootstrap.css",
                        "~/Content/menu.css",
                        "~/Content/Site.css",
                        "~/Content/core.css",
                        "~/Content/components.css",
                        "~/Content/icons.css",
                        "~/Content/pages.css",
                        "~/Content/responsive.css"));



        }
    }
}
