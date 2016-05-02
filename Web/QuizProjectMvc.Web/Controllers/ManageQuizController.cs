namespace QuizProjectMvc.Web.Controllers
{
    using System.Web.Mvc;
    using Common;
    using Data.Models;
    using Services.Data.Protocols;
    using ViewModels.Quiz.Edit;

    [Authorize]
    public class ManageQuizController : BaseController
    {
        private readonly IQuizzesGeneralService quizzes;

        public ManageQuizController(IQuizzesGeneralService quizzes)
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
            var result = this.EnsureQuiz(quiz);

            if (result == null)
            {
                var model = this.Mapper.Map<EditQuizModel>(quiz);

                result = this.View(model);
            }

            return result;
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var quiz = this.quizzes.GetById(id);
            var result = this.EnsureQuiz(quiz);

            if (result == null)
            {
                this.quizzes.Delete(quiz);
                this.TempData["notification"] = "Quiz successfully deleted";
                result = this.RedirectToAction("Index", "Home");
            }

            return result;
        }

        [HttpGet]
        public ActionResult GetCategoryTemplate()
        {
            return this.PartialView("_CategoryTemplatePartial");
        }

        private ActionResult EnsureQuiz(Quiz quiz)
        {
            if (quiz == null)
            {
                return this.HttpNotFound("The specified quiz has disappeared without a trace");
            }

            bool isAdmin = this.User.IsInRole(GlobalConstants.AdministratorRoleName);

            if (!isAdmin && this.UserId != quiz.CreatedById)
            {
                this.TempData["error"] = "You are not allowed to modify this quiz as you are not it's creator";
                return this.RedirectToAction("Index", "Home");
            }

            return null;
        }
    }
}
