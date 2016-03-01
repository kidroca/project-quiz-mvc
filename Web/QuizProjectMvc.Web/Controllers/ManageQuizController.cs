namespace QuizProjectMvc.Web.Controllers
{
    using System.Web.Mvc;
    using Services.Data.Protocols;
    using ViewModels.Quiz.Manage;

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

            var model = this.Mapper.Map<ManageQuizModel>(quiz);

            return this.View(model);
        }
    }
}
