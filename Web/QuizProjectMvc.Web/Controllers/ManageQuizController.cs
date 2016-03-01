namespace QuizProjectMvc.Web.Controllers
{
    using System.Web.Mvc;
    using Services.Data.Protocols;
    using ViewModels.Quiz.Manage;

    [Authorize]
    public class ManageQuizController : BaseController
    {
        private readonly IQuizzesService quizzes;

        public ManageQuizController(IQuizzesService quizzes)
        {
            this.quizzes = quizzes;
        }

        // The Post Method is handled in the api/Quizzes/Create
        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        // The Post Method is handled in the api/Quizzes/Update/{id}
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var quiz = this.quizzes.GetById(id);
            if (quiz == null)
            {
                return this.HttpNotFound("The specified quiz has disappeared without a trace");
            }

            if (this.UserId != quiz.CreatedById)
            {
                this.TempData["error"] = "You are not allowed to modify this quiz as you are not it's creator";
                return this.RedirectToAction("Index", "Home");
            }

            var model = this.Mapper.Map<ManageQuizModel>(quiz);

            return this.View(model);
        }
    }
}
