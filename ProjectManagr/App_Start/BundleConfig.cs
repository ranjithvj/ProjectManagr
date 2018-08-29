using System.Web;
using System.Web.Optimization;

namespace ProjectManagr
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/dashboardJs").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/Notify/bootstrap-notify.min.js",
                        "~/Scripts/Application/Common.js",
                        "~/Scripts/Application/Dashboard.js",
                        "~/Scripts/New Datepicker/bootstrap-datepicker.js"));

            bundles.Add(new StyleBundle("~/bundles/dashboardCss").Include(
                      "~/Content/Common.css",
                      "~/Content/Dashboard.css",
                      "~/Content/Datepicker/datepicker.css"));

            bundles.Add(new StyleBundle("~/bundles/mydpCss").Include(
                     "~/Content/Common.css",
                     "~/Content/ProjectSite.css"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/mydpJs").Include(
                   "~/Scripts/jquery.unobtrusive-ajax.js",
                   "~/Scripts/Notify/bootstrap-notify.min.js",
                   "~/Scripts/Application/Common.js",
                   "~/Scripts/Application/ProjectSite.js"));

            bundles.Add(new StyleBundle("~/bundles/LayoutCssBundle").Include(
                       "~/Content/ProjectSite.css" ));
        }
    }
}
