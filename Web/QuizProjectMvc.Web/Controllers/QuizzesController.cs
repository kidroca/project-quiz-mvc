namespace QuizProjectMvc.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Services.Data.Models.DateRanges;
    using Services.Data.Models.Search;
    using Services.Data.Protocols;
    using ViewModels.Quiz;
    using ViewModels.Quiz.Ranking;

    public class QuizzesController : BaseQuizController
    {
        private readonly IQuizzesGeneralService quizzes;
        private readonly ICategoriesService categories;

        public QuizzesController(IQuizzesGeneralService quizzes, IQuizzesRankingService ranking, ICategoriesService categories)
            : base(ranking)
        {
            this.quizzes = quizzes;
            this.categories = categories;
        }

        [HttpGet]
        public ActionResult Search(QuizSearchModel query)
        {
            var results = new List<QuizRankedModel>();
            var range = new MonthlyRange();

            if (query != null && (query.Category != null || query.KeyPhrase != null))
            {
                results = this.Ranking
                   .OrderByRanking(this.quizzes.SearchQuizzes(query), range)
                   .To<QuizRankedModel>()
                   .ToList();
            }

            var maxSolutions = this.GetMaxSolutions(range);

            var page = new SearchPageViewModel(maxSolutions, range)
            {
                QuizSearchModel = query,
                Quizzes = results,
                Categories = this.categories.GetCategoryOptions()
            };

            return this.View(page);
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
