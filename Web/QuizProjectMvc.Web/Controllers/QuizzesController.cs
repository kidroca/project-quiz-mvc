namespace QuizProjectMvc.Web.Controllers
{
    using System.Web.Mvc;

    using QuizProjectMvc.Services.Data;
    using QuizProjectMvc.Web.ViewModels.Home;

    public class QuizzesController : BaseController
    {
        private readonly IQuizesService quizes;

        public QuizzesController(
            IQuizesService quizes)
        {
            this.quizes = quizes;
        }

        public ActionResult ById(string id)
        {
            var quiz = this.quizes.GetById(id);
            var viewModel = this.Mapper.Map<QuizBasicViewModel>(quiz);
            return this.View(viewModel);
        }
    }
}
