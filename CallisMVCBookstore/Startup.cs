using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CallisMVCBookstore.Startup))]
namespace CallisMVCBookstore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
