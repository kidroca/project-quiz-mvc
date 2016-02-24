namespace QuizProjectMvc.Web.Controllers
{
    using System.Web.Mvc;
    using Data.Models;
    using Services.Data;
    using ViewModels.Quiz.Create;

    public class CreateQuizController : BaseController
    {
        private IQuizzesService quizzes;

        public CreateQuizController(IQuizzesService quizzes)
        {
            this.quizzes = quizzes;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Create(CreateQuizModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["error"] = "Invalid Quiz Data";
                return this.View();
            }

            var quiz = this.Mapper.Map<Quiz>(model);
            quiz.CreatedById = this.UserId;

            this.quizzes.Add(quiz);

            return this.RedirectToRoute("Default");
        }

        public ActionResult AddQuestionTemplate()
        {
            return this.PartialView();
        }
    }
}
