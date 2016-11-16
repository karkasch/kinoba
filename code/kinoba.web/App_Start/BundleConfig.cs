using System.Web;
using System.Web.Optimization;

namespace kinoba.web
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

            //////////////

            bundles.Add(new StyleBundle("~/knd/styles/css").Include(
                      "~/knd/styles/kendo.common.min.css",
                      "~/knd/styles/kendo.default.min.css",
                      "~/knd/styles/kendo.metro.min.css"));

            bundles.Add(new StyleBundle("~/css/kinoba/search").Include(
                      "~/content/specialist.search.css"));

            bundles.Add(new StyleBundle("~/css/casting").Include(
                      "~/content/specialist.casting.css"));

            /////

            bundles.Add(new ScriptBundle("~/scripts/uploaderplugin").Include(
                "~/Scripts/uploader/vendor/jquery.ui.widget.js",
                "~/Scripts/uploader/jquery.iframe-transport.js",
                "~/Scripts/uploader/jquery.fileupload.js"));
            
            bundles.Add(new ScriptBundle("~/kendo/scripts").Include(
                "~/knd/js/kendo.all.min.js"));

            bundles.Add(new ScriptBundle("~/apps/search").Include(
                "~/scripts/jquery.scrollTo.min.js",
                "~/scripts/knb/app/specialist.search.app.js",
                "~/scripts/knb/models/specialist.search.params.model.js",
                "~/scripts/knb/models/specialist.search.results.model.js",
                "~/scripts/knb/models/specialist.details.model.js"
                ));

            bundles.Add(new ScriptBundle("~/apps/casting").Include(
                "~/Scripts/knb/data/list.data.item.js",
                "~/Scripts/knb/data/professions.datasource.js",
                "~/Scripts/knb/data/schools.datasource.js",
                "~/Scripts/knb/models/specialist.model.js",
                "~/Scripts/knb/app/specialist.edit.app.js"    
                ));

        }
    }
}
