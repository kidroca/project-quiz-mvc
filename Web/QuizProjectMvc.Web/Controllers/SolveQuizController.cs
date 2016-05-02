namespace QuizProjectMvc.Web.Controllers
{
    using System.Web.Mvc;
    using Services.Data.Exceptions;
    using Services.Data.Models.Evaluation;
    using Services.Data.Protocols;
    using ViewModels.Quiz.Solve;

    public class SolveQuizController : BaseController
    {
        private readonly IQuizzesGeneralService quizzes;
        private readonly IQuizzesEvalService quizzesEvalService;

        public SolveQuizController(IQuizzesGeneralService quizzesService, IQuizzesEvalService quizzesEvalService)
        {
            this.quizzes = quizzesService;
            this.quizzesEvalService = quizzesEvalService;
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
            if (!this.ModelState.IsValid)
            {
                this.TempData["error"] = "Invalid Solution Data";
                this.RedirectToAction("Solve");
            }

            try
            {
                var result = this.quizzesEvalService.SaveSolution(solution, this.UserId);
                return this.RedirectToAction("Result", new { solutionId = result.Id });
            }
            catch (QuizEvaluationException ex)
            {
                this.TempData["error"] = ex.Message;
                return this.RedirectToAction("Solve");
            }
        }

        public ActionResult Result(int solutionId)
        {
            var solution = this.quizzesEvalService.EvaluateSolution(solutionId);
            if (solution == null)
            {
                // Todo: Redirect To Not Found
                return this.RedirectToRoute("Default");
            }

            return this.View(solution);
        }
    }
}
