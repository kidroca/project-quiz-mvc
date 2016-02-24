namespace QuizProjectMvc.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "QuizPage",
            //    url: "Quiz/{id}",
            //    defaults: new { controller = "Quizzes", action = "ById" });

            routes.MapRoute(
               name: "SubmitSolution",
               url: "Quizzes/Solve",
               defaults: new { controller = "SolveQuiz", action = "Solve" },
               namespaces: new[] { "QuizProjectMvc.Web.Controllers" });

            routes.MapRoute(
                name: "SolveQuiz",
                url: "Quizzes/Solve/{id}/{title}",
                defaults: new { controller = "SolveQuiz", action = "Solve" },
                namespaces: new[] { "QuizProjectMvc.Web.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "QuizProjectMvc.Web.Controllers" });
        }
    }
}
