using Microsoft.Owin;

using Owin;

[assembly: OwinStartupAttribute(typeof(QuizProjectMvc.Web.Startup))]

namespace QuizProjectMvc.Web
{
    using System.Web.Http;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            JsonNetConfig.UseCamelCase(config);
            WebApiConfig.Register(config);

            this.ConfigureAuth(app);
        }
    }
}
