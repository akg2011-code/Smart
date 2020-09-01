using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SmartSite.Models;

[assembly: OwinStartupAttribute(typeof(SmartSite.Startup))]
namespace SmartSite
{
    public partial class Startup
    {
        ApplicationDbContext DB;
        public Startup()
        {
            DB = new ApplicationDbContext();
        }

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        public void CreateRole()
        {
            //RoleManager roleManager = new RoleManager<IdentityRole>() { };
        }

    }
}
