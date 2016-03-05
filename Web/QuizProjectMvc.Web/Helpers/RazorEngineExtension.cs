namespace QuizProjectMvc.Web.Helpers
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    public class RazorEngineExtension : RazorViewEngine
    {
        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            partialPath = GlobalizeViewPath(controllerContext, partialPath);
            return base.CreatePartialView(controllerContext, partialPath);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            viewPath = GlobalizeViewPath(controllerContext, viewPath);
            return base.CreateView(controllerContext, viewPath, masterPath);
        }

        private static string GlobalizeViewPath(ControllerContext ctrlContext, string viewPath)
        {
            var request = ctrlContext.HttpContext.Request;
            var cookie = request.Cookies["language"];
            string language = cookie?.Value;

            if (string.IsNullOrEmpty(language))
            {
                language = request.Headers["accept-language"]?.Substring(0, 2);
            }

            if (
                !string.IsNullOrEmpty(language) &&
                !string.Equals(language, "en", StringComparison.InvariantCultureIgnoreCase))
            {
                string localizedViewPath = Regex.Replace(
                    viewPath,
                    "^~/Views/",
                    $"~/Views/Globalization/{language}/");

                if (File.Exists(request.MapPath(localizedViewPath)))
                {
                    viewPath = localizedViewPath;
                }
            }

            return viewPath;
        }
    }
}
