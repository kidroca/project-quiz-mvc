namespace QuizProjectMvc.Web.Controllers
{
    using System.Web.Mvc;
    using ViewModels.ProfileInformation;

    public class ProfileController : Controller
    {
        public ActionResult My()
        {
            // Todo Get public profile information, last 10 quizzes created, last 10 quizzes solution
            // and add the to the page view model then display, confirm the 'edit profile' button is showing

            var pageModel = new ProfilePageViewModel();

            return View(pageModel);
        }
    }
}
