//using System.Web;
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
                        "~/Scripts/jquery.scrollTo.min.js",
                        "~/Scripts/main.js",
                        "~/Scripts/jquery.mask.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            //bundles.Add(new ScriptBundle("~/Plugins/morris").Include(
            //           "~/Content/Plugins/morris/morris.min.js",
            //           "~/Content/Plugins/raphael/raphael-min.js"));

            bundles.Add(new ScriptBundle("~/bundles/counterup").Include(
                        "~/Content/Plugins/waypoints/lib/jquery.waypoints.js",
                        "~/Content/Plugins/counterup/jquery.counterup.min.js",
                         "~/Content/Plugins/bootstrap-maxlength/bootstrap-maxlength.min.js"));


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



            //Datatable
            //bundles.Add(new StyleBundle("~/Plugins/datatTables").Include(
            ////"~/Content/Plugins/datatables/jquery.dataTables.min.css",
            ////"~/Content/Plugins/datatables/buttons.bootstrap.min.css",
            ////"~/Content/Plugins/datatables/fixedHeader.bootstrap.min.css",
            ////"~/Content/Plugins/datatables/responsive.bootstrap.min.css",
            //"~/Content/Plugins/datatables/scroller.bootstrap.min.css"));

            bundles.Add(new ScriptBundle("~/Plugins/datatablesJS").Include(
            "~/Content/Plugins/datatables/jquery.dataTables.min.js",
            "~/Content/Plugins/datatables/dataTables.bootstrap.js",
            "~/Content/Plugins/datatables/dataTables.buttons.min.js",
            "~/Content/Plugins/datatables/buttons.bootstrap.min.js",
            "~/Content/Plugins/datatables/jszip.min.js",
            "~/Content/Plugins/datatables/pdfmake.min.js",
            "~/Content/Plugins/datatables/vfs_fonts.js",
            "~/Content/Plugins/datatables/buttons.html5.min.js",
            "~/Content/Plugins/datatables/buttons.print.min.js",
            "~/Content/Plugins/datatables/dataTables.fixedHeader.min.js",
            "~/Content/Plugins/datatables/dataTables.keyTable.min.js",
            "~/Content/Plugins/datatables/dataTables.responsive.min.js",
            "~/Content/Plugins/datatables/responsive.bootstrap.min.js",
            "~/Content/Plugins/datatables/dataTables.scroller.min.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/Plugins/morris/morris.css",
                        "~/Content/bootstrap.css",
                        "~/Content/menu.css",
                        "~/Content/Site.css",
                        "~/Content/core.css",
                        "~/Content/components.css",
                        "~/Content/icons.css",
                        "~/Content/pages.css",
                        "~/Content/responsive.css"));

            bundles.Add(new StyleBundle("~/Content/Facturas").Include(                      
                        "~/Content/Facturas.css"));

            //Datepicker
            bundles.Add(new StyleBundle("~/Plugins/datepicker").Include(
            "~/Content/Plugins/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css"));

            bundles.Add(new ScriptBundle("~/Plugins/datepickerJS").Include(
            "~/Content/Plugins/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"));

            //switchery
            bundles.Add(new StyleBundle("~/Plugins/switchery").Include(
            "~/Content/Plugins/switchery/switchery.min.css"));

            bundles.Add(new ScriptBundle("~/Plugins/switcheryJS").Include(
            "~/Content/Plugins/switchery/switchery.min.js"));



            //@Scripts.Render("~/Plugins/switchery")

            //@Styles.Render("~/Plugins/switcheryJS")





        }
    }
}
