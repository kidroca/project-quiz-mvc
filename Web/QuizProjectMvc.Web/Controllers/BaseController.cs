namespace QuizProjectMvc.Web.Controllers
{
    using System.Web.Mvc;
    using AutoMapper;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using QuizProjectMvc.Services.Web;

    public abstract class BaseController : Controller
    {
        public ICacheService Cache { get; set; }

        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.Configuration.CreateMapper();
            }
        }

        protected string UserId
        {
            get { return this.User.Identity.GetUserId(); }
        }
    }
}
