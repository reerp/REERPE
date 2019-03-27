using System.Web.Optimization;

namespace REERP
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/K-jquery").Include(
                "~/Content/katniss-template/js/jquery.js",
                "~/Content/katniss-template/js/popper.js",
                "~/Content/katniss-template/js/bootstrap.js",
                "~/Content/katniss-template/js/perfect-scrollbar.jquery.js",
                "~/Content/katniss-template/js/moment.js",
                "~/Content/katniss-template/js/katniss.js"));
            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                "~/Content/katniss-template/lib/highlightjs/highlight.pack.js",
                "~/Content/katniss-template/lib/datatables/jquery.dataTables.js",
                "~/Content/katniss-template/lib/select2/select2.min.js",
                "~/Content/katniss-template/lib/datatables-responsive/dataTables.responsive.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/K-css").Include(
                "~/Content/katniss-template/css/font-awesome.css",
                "~/Content/katniss-template/css/ionicons.css",
                "~/Content/katniss-template/css/perfect-scrollbar.css",
                "~/Content/katniss-template/css/spinkit.css",
                "~/Content/katniss-template/css/katniss.css"));
            bundles.Add(new StyleBundle("~/Content/datatable").Include(
                "~/Content/katniss-template/lib/highlightjs/github.css",
                "~/Content/katniss-template/lib/datatables/jquery.dataTables.css",
                "~/Content/katniss-template/lib/select2/select2.min.css"));
        }
    }
}
