namespace QuizProjectMvc.Web.Controllers
{
    using System.Web.Mvc;
    using Data.Models;
    using Infrastructure.SharedModels;
    using Infrastructure.SharedModels.Evaluation;
    using QuizProjectMvc.Services.Data;
    using ViewModels.Quiz;

    public class QuizzesController : BaseController
    {
        private readonly IQuizesService quizzes;

        public QuizzesController(
            IQuizesService quizzes)
        {
            this.quizzes = quizzes;
        }

        public ActionResult ById(string id)
        {
            var quiz = this.quizzes.GetById(id);
            var viewModel = this.Mapper.Map<QuizBasicViewModel>(quiz);
            return this.View(viewModel);
        }

        // Todo: Solve Action HttpGet
        [HttpPost]
        public ActionResult Solve(SolutionForEvaluationModel quizSolution)
        {
            if (!this.ModelState.IsValid)
            {
                // Todo Retry!
            }

            var quiz = this.quizzes.GetById(quizSolution.ForQuizId);
            if (quizSolution.Questions.Count != quiz.Questions.Count)
            {
                // return this.BadRequest("Invalid Solution: Questions count mismatch");
            }

            QuizSolution solution = this.quizzes.SaveSolution(quizSolution, quiz, this.UserId);
            QuizEvaluationResult result = this.quizzes.EvaluateSolution(solution);

            // Todo: Redirect with result
        }
    }
}
