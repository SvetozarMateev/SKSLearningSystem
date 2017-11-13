using System.Web;
using System.Web.Optimization;

namespace SKSLearningSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/slick.min.js",
                      "~/Scripts/slider.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/uploadcourse.css",

                      "~/Content/home-page.css",
                    "~/Content/hover.css",
                    "~/Content/slick-theme.css",
                    "~/Content/slick.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/jqGridcss").Include(
                    "~/Content/themes/base/jquery-ui.css",                     
                     "~/Content/jquery.jqGrid/ui.jqgrid.css",             
                       "~/Content/AssignCourse.css",
                        "~/Content/CompleteAssignment.css",
                      "~/Content/uploadcourse.css",
                      "~/Content/TakeCourse.css"));


            bundles.Add(new ScriptBundle("~/bundles/jqGrid").Include(
                    "~/Scripts/i18n/grid.locale-en.js",
                    "~/Scripts/jquery.jqGrid.js",
                    "~/Scripts/jquery-ui-{version}.js",
                    "~/Scripts/monitoringAdminSide.js"));


            bundles.Add(new ScriptBundle("~/bundles/uploadCourse").Include(
                      "~/Scripts/uploadCourse.js"));
            bundles.Add(new ScriptBundle("~/bundles/takeExam").Include(
                     "~/Scripts/takeExamValidator.js"));

            bundles.Add(new StyleBundle("~/Content/takeExam").Include(
                   "~/Content/takeExam.css"));

            bundles.Add(new StyleBundle("~/Content/TakeCourse").Include(
                   "~/Content/TakeCourse.css"));
        }
    }
}
