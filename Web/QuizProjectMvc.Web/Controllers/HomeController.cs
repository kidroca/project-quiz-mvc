namespace QuizProjectMvc.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure.Mapping;

    using Services.Data;
    using Services.Data.Models;
    using ViewModels.Home;
    using ViewModels.Quiz;

    public class HomeController : BaseController
    {
        private readonly IQuizzesService quizzes;
        private readonly ICategoriesService quizCategories;

        public HomeController(
            IQuizzesService quizzes,
            ICategoriesService quizCategories)
        {
            this.quizzes = quizzes;
            this.quizCategories = quizCategories;
        }

        public ActionResult Index(Pager pager)
        {
            if (pager == null || !this.ModelState.IsValid)
            {
                pager = new Pager();
            }

            var quizzes = this.quizzes.GetPage(pager)
                .To<QuizBasicViewModel>()
                .ToList();

            var categories =
                this.Cache.Get(
                    "categories",
                    () => this.quizCategories.GetTop(10)
                        .To<QuizCategoryViewModel>()
                        .ToList(),
                    durationInSeconds: 30 * 60);

            var viewModel = new IndexViewModel
            {
                Quizzes = quizzes,
                Categories = categories,
                Pager = pager
            };

            return this.View(viewModel);
        }
    }
}
