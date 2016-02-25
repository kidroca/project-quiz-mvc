namespace QuizProjectMvc.Web.Controllers
{
    using System.Web.Mvc;

    public class CreateQuizController : BaseController
    {
        // The Post Method is handled in the api/Quizzes/Create
        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        public ActionResult AddQuestionTemplate()
        {
            return this.PartialView();
        }
    }
}
