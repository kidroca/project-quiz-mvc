namespace QuizProjectMvc.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;
    using Common;
    using Services.Data.Protocols;
    using Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categories;

        public CategoriesController(ICategoriesService categories)
        {
            this.categories = categories;
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult GetEditorTemplate()
        {
            return this.PartialView("_CategoryEditPartial");
        }

        [HttpGet]
        public ActionResult GetDisplayTemplate()
        {
            return this.PartialView("_CategoryDisplayPartial");
        }

        [HttpGet]
        public ActionResult GetImagesModalTemplate()
        {
            return this.PartialView("_SelectImageModalPartial");
        }
    }
}
