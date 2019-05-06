using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EventFinalProject.Startup))]
namespace EventFinalProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
