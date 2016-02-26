namespace QuizProjectMvc.Web.Areas.Api.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Data.Models;
    using Services.Data.Exceptions;
    using Services.Data.Models.Evaluation;
    using Services.Data.Protocols;
    using ViewModels.Quiz.Create;

    public class QuizzesController : BaseController
    {
        private readonly IQuizzesService quizzes;
        private readonly ICategoriesService categories;

        public QuizzesController(IQuizzesService quizzes, ICategoriesService categories)
        {
            this.quizzes = quizzes;
            this.categories = categories;
        }

        [HttpPost]
        public IHttpActionResult Create(CreateQuizModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ResponseMessage(new HttpResponseMessage(HttpStatusCode.BadRequest));
                return this.BadRequest(this.ModelState);
            }

            var quiz = this.Mapper.Map<Quiz>(model);
            quiz.CreatedById = this.UserId;
            quiz.Category = this.categories.EnsureCategory(model.Category);

            try
            {
                this.quizzes.Add(quiz);
                return this.CreatedAtRoute(
                "QuizApi", new { action = "Solve", id = quiz.Id }, new { id = quiz.Id });
            }
            catch (QuizCreationException ex)
            {
                this.ResponseMessage(new HttpResponseMessage(HttpStatusCode.BadRequest));
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Solve(SolutionForEvaluationModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ResponseMessage(new HttpResponseMessage(HttpStatusCode.BadRequest));
                return this.BadRequest(this.ModelState);
            }

            try
            {
                var result = this.quizzes.SaveSolution(model, this.UserId);
                return this.Ok(this.quizzes.EvaluateSolution(result));
            }
            catch (QuizEvaluationException ex)
            {
                this.ResponseMessage(new HttpResponseMessage(HttpStatusCode.BadRequest));
                return this.BadRequest(ex.Message);
            }
        }
    }
}
