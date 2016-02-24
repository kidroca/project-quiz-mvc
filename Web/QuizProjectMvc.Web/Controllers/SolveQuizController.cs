namespace QuizProjectMvc.Web.Controllers
{
    using System.Web.Mvc;
    using Services.Data;
    using Services.Data.Models.Evaluation;
    using ViewModels.Quiz;

    public class SolveQuizController : BaseController
    {
        private readonly IQuizzesService quizzes;

        public SolveQuizController(IQuizzesService quizzesService)
        {
            this.quizzes = quizzesService;
        }

        [HttpGet]
        public ActionResult Solve(int id, string title)
        {
            var model = this.Mapper.Map<QuizForSolvingModel>(this.quizzes.GetById(id));

            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Solve(SolutionForEvaluationModel solution)
        {
            return View();
        }
    }
}
