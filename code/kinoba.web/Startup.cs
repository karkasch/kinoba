using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(kinoba.web.Startup))]
namespace kinoba.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
