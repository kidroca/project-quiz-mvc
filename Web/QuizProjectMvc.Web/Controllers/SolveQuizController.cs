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
        public ActionResult Solve(int id)
        {
            var model = this.Mapper.Map<QuizForSolvingModel>(this.quizzes.GetById(id));

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Solve(SolutionForEvaluationModel solution)
        {
            return this.View();
        }


        // Todo: Solve Action HttpGet
        //[HttpPost]
        //public ActionResult Solve(SolutionForEvaluationModel quizSolution)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        // Todo Retry!
        //    }

        //    var quiz = this.quizzes.GetById(quizSolution.ForQuizId);
        //    if (quizSolution.Questions.Count != quiz.Questions.Count)
        //    {
        //        // return this.BadRequest("Invalid Solution: Questions count mismatch");
        //    }

        //    QuizSolution solution = this.quizzes.SaveSolution(quizSolution, quiz, this.UserId);
        //    QuizEvaluationResult result = this.quizzes.EvaluateSolution(solution);

        //    // Todo: Redirect with result
        //}
    }
}
