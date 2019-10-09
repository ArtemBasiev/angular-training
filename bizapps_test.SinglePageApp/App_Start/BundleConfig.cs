using System.Web;
using System.Web.Optimization;

namespace bizapps_test.SinglePageApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery.unobtrusive-ajax").Include(
                "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockout-paging").Include(
                "~/Scripts/knockout-paging.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockout-mapping").Include(
                "~/Scripts/knockout.mapping-latest.js"));

            bundles.Add(new ScriptBundle("~/bundles/Authorization").Include(
                "~/Scripts/SpaLogic/Authorization.js"));

            bundles.Add(new ScriptBundle("~/bundles/CookieHandler").Include(
                "~/Scripts/SpaLogic/CookieHandler.js"));

            bundles.Add(new ScriptBundle("~/bundles/AjaxOperations").Include(
                "~/Scripts/SpaLogic/AjaxOperations.js"));

            bundles.Add(new ScriptBundle("~/bundles/WebApiConfig").Include(
                "~/Scripts/SpaLogic/WebApiConfig.js"));

            bundles.Add(new ScriptBundle("~/bundles/ViewModels").Include(
                "~/Scripts/SpaLogic/ViewModels.js"));

            bundles.Add(new ScriptBundle("~/bundles/ViewModelsSetting").Include(
                "~/Scripts/SpaLogic/ViewModelsSetting.js"));

            bundles.Add(new ScriptBundle("~/bundles/ViewsGetting").Include(
                "~/Scripts/SpaLogic/ViewsGetting.js"));

            bundles.Add(new ScriptBundle("~/bundles/PuttingViewToWrap").Include(
                "~/Scripts/SpaLogic/PuttingViewToWrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/CRUDBlog").Include(
                "~/Scripts/SpaLogic/CRUDBlog.js"));

            bundles.Add(new ScriptBundle("~/bundles/CRUDPost").Include(
                "~/Scripts/SpaLogic/CRUDPost.js"));

            bundles.Add(new ScriptBundle("~/bundles/CRUDCategory").Include(
                "~/Scripts/SpaLogic/CRUDCategory.js"));

            bundles.Add(new ScriptBundle("~/bundles/InterfaceLogic").Include(
                "~/Scripts/SpaLogic/InterfaceLogic.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css", 
                      "~/fonts/stylesheet.css"));
        }
    }
}
