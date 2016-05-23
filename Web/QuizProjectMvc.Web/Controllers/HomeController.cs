namespace QuizProjectMvc.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Helpers;
    using Infrastructure.Mapping;
    using Services.Data.Models;
    using Services.Data.Models.DateRanges;
    using Services.Data.Protocols;
    using ViewModels.Home;
    using ViewModels.Quiz;
    using ViewModels.Quiz.Ranking;

    public class HomeController : BaseQuizController
    {
        public const int QuizzesPerPage = 3;

        private readonly IQuizzesGeneralService quizzes;
        private readonly ICategoriesService quizCategories;

        public HomeController(
            IQuizzesGeneralService quizzes,
            ICategoriesService quizCategories,
            IQuizzesRankingService ranking)
            : base(ranking)
        {
            this.quizzes = quizzes;
            this.quizCategories = quizCategories;
        }

        public ActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Categories = this.GetAllCategories()
            };

            return this.View(viewModel);
        }

        public ActionResult DisplayPage(Pager pager)
        {
            if (pager == null || !this.ModelState.IsValid)
            {
                return this.HttpNotFound("The specified page has disappeared without a trace");
            }

            if (pager.TotalPages == 0)
            {
                pager.TotalPages = this.quizzes.GetTotalPages(pager.CategoryName, QuizzesPerPage);
            }

            var rankingPeriod = new WeeklyRange();
            var maxSolutions = this.GetMaxSolutions(rankingPeriod);

            var models = this.Ranking.GetQuizzesOrderedBySolutions(rankingPeriod)
                .ApplyPaging(pager)
                .To<QuizRankedModel>()
                .ToArray();

            var viewModel = new HomePageViewModel(maxSolutions, rankingPeriod, pager)
            {
                Quizzes = models,
                Categories = this.GetTopCategories(),
            };

            return this.View(viewModel);
        }

        private IList<QuizCategoryViewModel> GetAllCategories()
        {
            var categories =
                this.Cache.Get(
                    "allCategories",
                    () => this.quizCategories.GetAll()
                        .To<QuizCategoryViewModel>()
                        .ToList(),
                    durationInSeconds: 30 * 60);

            return categories;
        }

        private IList<QuizCategoryViewModel> GetTopCategories()
        {
            var categories =
                this.Cache.Get(
                    "topCategories",
                    () => this.quizCategories.GetTop(10)
                        .To<QuizCategoryViewModel>()
                        .ToList(),
                    durationInSeconds: 15 * 60);

            return categories;
        }
    }
}
