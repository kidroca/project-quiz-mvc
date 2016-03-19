namespace QuizProjectMvc.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Services.Data.Models.Search;
    using Services.Data.Protocols;
    using ViewModels.Quiz;

    public class QuizzesController : BaseController
    {
        private readonly IQuizzesService quizzes;
        private readonly ICategoriesService categories;

        public QuizzesController(IQuizzesService quizzes, ICategoriesService categories)
        {
            this.quizzes = quizzes;
            this.categories = categories;
        }

        [HttpGet]
        public ActionResult Search(QuizSearchModel query)
        {
            var results = new List<QuizBasicViewModel>();

            if (query != null && (query.Category != null || query.KeyPhrase != null))
            {
                results = this.quizzes.SearchQuizzes(query)
               .To<QuizBasicViewModel>()
               .ToList();
            }

            this.SetQuizMaxSolutions();

            var categoryItems = this.Cache.Get(
                "allCategories",
                () => this.categories.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Name
                }).ToList(),
                durationInSeconds: 20 * 60);

            var page = new SearchPageViewModel
            {
                QuizSearchModel = query,
                Quizzes = results,
                Categories = categoryItems
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

            this.SetQuizMaxSolutions();

            if (models.Count == 0)
            {
                return this.PartialView("_NoResultsPartial");
            }

            return this.PartialView("_QueryResultPartial", models);
        }

        private void SetQuizMaxSolutions()
        {
            var maxQuizzesSolved = this.quizzes.GetMaxSolutionsCount();
            QuizBasicViewModel.MaxTimesCompleted = maxQuizzesSolved;
        }
    }
}
