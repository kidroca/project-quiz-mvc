namespace QuizProjectMvc.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using QuizProjectMvc.Common;
    using QuizProjectMvc.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AdministrationController : BaseController
    {
    }
}
