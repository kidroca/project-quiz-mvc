namespace QuizProjectMvc.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Infrastructure.Mapping;

    using Services.Data;

    using ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IQuizesService quizes;
        private readonly ICategoriesService jokeCategories;

        public HomeController(
            IQuizesService quizes,
            ICategoriesService jokeCategories)
        {
            this.quizes = quizes;
            this.jokeCategories = jokeCategories;
        }

        public ActionResult Index()
        {
            // Todo Map to correct view model
            var jokes = this.quizes.GetRandomQuizzes(3).To<QuizBasicViewModel>().ToList();
            var categories =
                this.Cache.Get(
                    "categories",
                    () => this.jokeCategories.GetAll().To<QuizCategoryViewModel>().ToList(),
                    30 * 60);
            var viewModel = new IndexViewModel
            {
                Quizzes = jokes,
                Categories = categories
            };

            return this.View(viewModel);
        }
    }
}
