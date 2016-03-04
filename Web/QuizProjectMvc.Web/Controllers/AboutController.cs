namespace QuizProjectMvc.Web.Controllers
{
    using System.Web.Mvc;

    public class AboutController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}