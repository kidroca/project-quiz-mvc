namespace QuizProjectMvc.Web.Areas.Api.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using Administration.ViewModels;
    using Common;
    using Data.Models;
    using Infrastructure.Mapping;
    using Services.Data.Exceptions;
    using Services.Data.Protocols;
    using ViewModels.Quiz;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categories;

        public CategoriesController(ICategoriesService categories)
        {
            this.categories = categories;
        }

        [System.Web.Mvc.OutputCache(Duration = 10 * 60)]
        [AllowAnonymous]
        public IHttpActionResult GetByPattern(string pattern)
        {
            var result = this.categories.FilterByPattern(pattern)
                .To<QuizCategoryViewModel>()
                .ToList();

            return this.Ok(result);
        }

        [AllowAnonymous]
        public IHttpActionResult GetCategories()
        {
            return this.GetByPattern(null);
        }

        public IHttpActionResult GetAll()
        {
            var result = this.categories.GetAll()
                .To<AdvancedCategoryModel>()
                .ToList();

            return this.Ok(result);
        }

        [HttpPost]
        public IHttpActionResult Edit(AdvancedCategoryModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var category = this.categories.GetById(model.Id);
            if (category == null)
            {
                return this.NotFound();
            }

            this.Mapper.Map(model, category);
            this.categories.Save();

            return this.Ok();
        }

        [HttpPost]
        public IHttpActionResult AddNew(AdvancedCategoryModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            try
            {
                var category = this.Mapper.Map<QuizCategory>(model);
                this.categories.Create(category);

                return this.Created($"/api/GetByPattern?pattern={category.Name}", new { id = category.Id });
            }
            catch (CategoryManagementException ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                bool deleted = this.categories.Delete(id);
                return deleted
                    ? (IHttpActionResult)this.Ok()
                    : this.NotFound();
            }
            catch (CategoryManagementException ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult UploadImage()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server
                        .MapPath("~/Content/images/categories/" + postedFile.FileName);

                    // NOTE: To store in memory use postedFile.InputStream
                    postedFile.SaveAs(filePath);
                }

                return this.Created("/Content/images/categories/", string.Empty);
            }

            return this.BadRequest("Could not upload image");
        }

        [HttpGet]
        public IHttpActionResult GetAvailableImages()
        {
            var directory = new DirectoryInfo(HttpContext.Current.Server
                .MapPath("~/Content/images/categories/"));

            var files = directory.EnumerateFiles()
                .Select(f => f.Name);

            return this.Ok(files);
        }

        [HttpDelete]
        public IHttpActionResult DeleteImage(string name)
        {
            if (!name.StartsWith("/Content/images/categories/"))
            {
                return this.BadRequest();
            }

            var file = new FileInfo(HttpContext.Current.Server
                .MapPath($"~{name}"));

            file.Delete();

            return this.Ok();
        }
    }
}
