using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartSite.Startup))]
namespace SmartSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
