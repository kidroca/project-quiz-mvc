namespace QuizProjectMvc.Web.Helpers
{
    using System.Web.Mvc;

    public static class HtmlExtensions
    {
        public static string ActionName(this HtmlHelper html)
        {
            return html.ViewContext.RouteData.GetRequiredString("action");
        }

        public static string ControllerName(this HtmlHelper html)
        {
            return html.ViewContext.RouteData.GetRequiredString("controller")
                .Replace("Controller", string.Empty);
        }
    }
}
