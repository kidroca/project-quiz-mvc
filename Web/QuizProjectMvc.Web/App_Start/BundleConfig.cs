namespace QuizProjectMvc.Web
{
    using System.Web.Optimization;

    public static class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScripts(bundles);
            RegisterStyles(bundles);

            // Enable for minification
            // BundleTable.EnableOptimizations = true;
        }

        private static void RegisterScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/jquery-ajax").Include("~/Scripts/jquery.unobtrusive-ajax.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                "~/Scripts/ngStorage.js",
                "~/Scripts/app/filters/paging-filter.js",
                "~/Scripts/app/services/error-handler.js"));

            bundles.Add(new ScriptBundle("~/bundles/solve-quiz").Include(
                "~/Scripts/app/controllers/solve-quiz.js"));

            bundles.Add(new ScriptBundle("~/bundles/manage-quiz").Include(
                "~/Scripts/angular-toggle-switch.js",
                "~/Scripts/app/controllers/manage-quiz/base-controller.js",
                "~/Scripts/app/controllers/manage-quiz/add-question.js"));

            bundles.Add(new ScriptBundle("~/bundles/create-quiz").Include(
                "~/Scripts/app/controllers/manage-quiz/create-quiz.js"));

            bundles.Add(new ScriptBundle("~/bundles/edit-quiz").Include(
                "~/Scripts/app/controllers/manage-quiz/edit-quiz.js"));

            bundles.Add(new ScriptBundle("~/bundles/select-avatar").Include(
               "~/Scripts/app/select-avatar.js"));

            bundles.Add(new ScriptBundle("~/bundles/toggle-profile-update").Include(
                "~/Scripts/app/toggle-profile-edit.js"));

            bundles.Add(new ScriptBundle("~/bundles/slick-carousel").Include(
                "~/Scripts/Slick/slick.js",
                "~/Scripts/Slick/angular-slick.js"));

            bundles.Add(new ScriptBundle("~/bundles/google").Include(
                "~/Scripts/google-analytics.js"));

            bundles.Add(new ScriptBundle("~/bundles/default").Include(
                "~/Scripts/app/confirm-submit.js",
                "~/Scripts/app/set-language.js"));

            bundles.Add(new ScriptBundle("~/bundles/about").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                "~/Scripts/Slick/slick.js",
                "~/Scripts/Slick/angular-slick.js",
                "~/Scripts/app/controllers/about.js"));

            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                "~/Scripts/ng-crop/ng-img-crop.js",
                "~/Scripts/ng-file-upload-all.js",
                "~/Scripts/app/admin/categoriesManager.js"));

            bundles.Add(new ScriptBundle("~/bundles/fb").Include(
                "~/Scripts/app/fb-integration.js"));
        }

        private static void RegisterStyles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/styles/bootstrap/bootstrap.css",
                "~/Content/styles/bootstrap/ui-bootstrap-csp.css",
                "~/Content/styles/font-awesome/font-awesome.css",
                "~/Content/styles/less/index.css"));

            bundles.Add(new StyleBundle("~/Content/toggle-switch").Include(
                "~/Content/styles/angular-toggle-switch/angular-toggle-switch-bootstrap.css",
                "~/Content/styles/angular-toggle-switch/angular-toggle-switch.css"));

            bundles.Add(new StyleBundle("~/Content/slick-carousel").Include(
                "~/Content/styles/slick-carousel/slick.css",
                "~/Content/styles/slick-carousel/slick-theme.css"));

            bundles.Add(new StyleBundle("~/Content/solve").Include(
               "~/Content/styles/less/pages/solve.css"));

            bundles.Add(new StyleBundle("~/Content/adminStyle").Include(
                "~/Content/admin/admin.css"));
        }
    }
}
