using Microsoft.Owin;

using Owin;

[assembly: OwinStartupAttribute(typeof(QuizProjectMvc.Web.Startup))]

namespace QuizProjectMvc.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
