using System.Web;
using System.Web.Optimization;

namespace Achilles.Codex.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquer   yval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                      "~/Scripts/typeahead.js",
                      "~/Scripts/site.js",
                      "~/Scripts/handlebars-1.1.2.js"
                      ));


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));


            bundles.Add(new ScriptBundle("~/bundles/edit-codex-item-article").Include(
                "~/Scripts/codex-item/edit-codex-item-article.js" /*Make sure this item is alway last in the bundle*/
                     ));

            bundles.Add(new ScriptBundle("~/bundles/edit-codex-item-basic-info").Include(
                     "~/Scripts/bootstrap-tagsinput.js",
                     "~/Scripts/codex-item/edit-codex-item-basic-info.js"/*Make sure this item is alway last in the bundle*/
                     ));
                

            bundles.Add(new StyleBundle("~/Content/css").Include(
                    "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/typeahead.js-bootstrap.css"
                      ));
            
            bundles.Add(new StyleBundle("~/Content/edit-codex-item-basic-info").Include(
                      "~/Content/bootstrap-tagsinput.css"
                      ));
            
            bundles.Add(new StyleBundle("~/Content/edit-codex-item-article").Include(

                      ));
        }
    }
}
