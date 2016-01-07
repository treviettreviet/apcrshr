using System.Web;
using System.Web.Optimization;

namespace apcrshr_site
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Home page Stylesheet
            bundles.Add(new StyleBundle("~/bundles/css/colors").Include(
                        "~/Content/css/colors/blue.css",
                        "~/Content/css/table.css",
                        "~/Content/realsite.css"));

            bundles.Add(new ScriptBundle("~/bundles/javascript").Include(
                        "~/Scripts/jquery.min.js",
                        "~/Scripts/jquery.migrate.js",
                        "~/Scripts/modernizrr.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/jquery.fitvids.js",
                        "~/Scripts/owl.carousel.min.js",
                        "~/Scripts/nivo-lightbox.min.js",
                        "~/Scripts/jquery.isotope.min.js",
                        "~/Scripts/jquery.appear.js",
                        "~/Scripts/count-to.js",
                        "~/Scripts/jquery.textillate.js",
                        "~/Scripts/jquery.lettering.js",
                        "~/Scripts/jquery.easypiechart.min.js",
                        "~/Scripts/jquery.nicescroll.min.js",
                        "~/Scripts/jquery.parallax.js",
                        "~/Scripts/jquery.themepunch.plugins.min.js",
                        "~/Scripts/jquery.themepunch.revolution.min.js",
                        "~/Scripts/script.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}