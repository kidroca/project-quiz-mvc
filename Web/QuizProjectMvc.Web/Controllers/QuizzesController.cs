namespace QuizProjectMvc.Web.Controllers
{
    using System.Web.Mvc;
    using QuizProjectMvc.Services.Data;
    using ViewModels.Quiz;

    public class QuizzesController : BaseController
    {
        private readonly IQuizzesService quizzes;

        public QuizzesController(
            IQuizzesService quizzes)
        {
            this.quizzes = quizzes;
        }

        public ActionResult ById(int id)
        {
            var quiz = this.quizzes.GetById(id);
            var viewModel = this.Mapper.Map<QuizBasicViewModel>(quiz);
            return this.View(viewModel);
        }
    }
}
