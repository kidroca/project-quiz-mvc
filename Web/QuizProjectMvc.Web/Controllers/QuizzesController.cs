namespace QuizProjectMvc.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Services.Data.Models.Search;
    using Services.Data.Protocols;
    using ViewModels.Quiz;

    public class QuizzesController : BaseController
    {
        private readonly IQuizzesService quizzes;

        public QuizzesController(IQuizzesService quizzes)
        {
            this.quizzes = quizzes;
        }

        [HttpGet]
        public ActionResult Search(QuizSearchModel query)
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Query(QuizSearchModel query)
        {
            if (!this.ModelState.IsValid)
            {
                // Todo: Something more appropriate
                return this.PartialView("_NoResultsPartial");
            }

            var models = this.quizzes.SearchQuizzes(query)
                .To<QuizBasicViewModel>()
                .ToList();

            if (models.Count == 0)
            {
                return this.PartialView("_NoResultsPartial");
            }

            return this.PartialView("_QueryResultPartial", models);
        }
    }
}
