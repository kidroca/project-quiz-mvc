namespace QuizProjectMvc.Web.Areas.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Mvc;
    using Administration.ViewModels;
    using Common;
    using Data.Models;
    using Infrastructure.Mapping;
    using Services.Data.Exceptions;
    using Services.Data.Protocols;

    [System.Web.Mvc.Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categories;

        public CategoriesController(ICategoriesService categories)
        {
            this.categories = categories;
        }

        [OutputCache(Duration = 10 * 60)]
        [System.Web.Mvc.AllowAnonymous]
        public IHttpActionResult GetByPattern(string pattern, int take = 10)
        {
            var result = this.categories.FilterByPattern(pattern, take)
                .Select(c => c.Name)
                .ToList();

            return this.Ok(result);
        }

        public IHttpActionResult GetAll()
        {
            var result = this.categories.GetAll()
                .To<AdvancedCategoryModel>()
                .ToList();

            return this.Ok(result);
        }

        [System.Web.Mvc.HttpPost]
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

        [System.Web.Mvc.HttpPost]
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

        [System.Web.Mvc.HttpDelete]
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
    }
}
