namespace QuizProjectMvc.Web.Areas.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Mvc;
    using Services.Data;

    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categories;

        public CategoriesController(ICategoriesService categories)
        {
            this.categories = categories;
        }

        [OutputCache(Duration = 10 * 60)]
        public IHttpActionResult GetByPattern(string pattern, int take = 10)
        {
            var result = this.categories.FilterByPattern(pattern, take)
                .Select(c => c.Name)
                .ToList();

            return this.Ok(result);
        }
    }
}
