namespace QuizProjectMvc.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using Common;
    using Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AdministrationController : BaseController
    {
        public ActionResult AdminMenu()
        {
            return this.RedirectToAction("Edit", "Categories");
        }
    }
}
