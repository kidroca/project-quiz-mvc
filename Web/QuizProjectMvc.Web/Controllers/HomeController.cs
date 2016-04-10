namespace QuizProjectMvc.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Services.Data.Models;
    using Services.Data.Protocols;
    using ViewModels.Home;
    using ViewModels.Quiz;

    public class HomeController : BaseController
    {
        public const int QuizzesPerPage = 3;

        private readonly IQuizzesService quizzes;
        private readonly ICategoriesService quizCategories;

        public HomeController(
            IQuizzesService quizzes,
            ICategoriesService quizCategories)
        {
            this.quizzes = quizzes;
            this.quizCategories = quizCategories;
        }

        public ActionResult Index()
        {
            var categories =
                this.Cache.Get(
                    "allCategories",
                    () => this.quizCategories.GetAll()
                        .To<QuizCategoryViewModel>()
                        .ToList(),
                    durationInSeconds: 30 * 60);

            var viewModel = new IndexViewModel
            {
                Categories = categories
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

            var models = this.quizzes.GetPage(pager)
                .To<QuizBasicViewModel>()
                .ToList();

            var maxQuizzesSolved = this.quizzes.GetMaxSolutionsCount();
            QuizBasicViewModel.MaxTimesCompleted = maxQuizzesSolved;

            var categories =
                this.Cache.Get(
                    "topCategories",
                    () => this.quizCategories.GetTop(10)
                        .To<QuizCategoryViewModel>()
                        .ToList(),
                    durationInSeconds: 15 * 60);

            var viewModel = new DisplayPageViewModel
            {
                Quizzes = models,
                Categories = categories,
                Pager = pager
            };

            return this.View(viewModel);
        }
    }
}
