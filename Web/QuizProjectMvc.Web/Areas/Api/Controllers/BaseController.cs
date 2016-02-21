namespace QuizProjectMvc.Web.Areas.Api.Controllers
{
    using System.Web.Http;
    using AutoMapper;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;

    public abstract class BaseController : ApiController
    {
        protected string UserId
        {
            get { return this.User.Identity.GetUserId(); }
        }

        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.Configuration.CreateMapper();
            }
        }
    }
}
