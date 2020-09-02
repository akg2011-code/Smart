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
            CreateRole();
            CreateUser();
        }


        // ---------------- Function of creating roles --------------------
        public void CreateRole()
        {
            // role manager is resposible for dealing with roles :
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(DB));

            IdentityRole role;

            if (!roleManager.RoleExists("Admin")) // check if role "Admin" existed in DB
            {
                role = new IdentityRole(); // create new object if identity role
                role.Name = "Admin"; // assign "Admin" to role name
                roleManager.Create(role); // role manager creates role
            }

            if (!roleManager.RoleExists("User")) // check if role "User" existed in DB
            {
                role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }

        }

        // ---------------- Function of creating user admin --------------------
        public void CreateUser()
        {
            // user manager is responsiple for dealing with users .
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(DB));

            // applicationUser is a class inherit from IdentityUser and have all user info .
            var user = new ApplicationUser();

            // enter main user info .
            user.Email = "smartAdmin";
            user.UserName = "admin@smartsite.com";

            // creating admin with his password .
            var createUserAdmin = usermanager.Create(user, "smart@ADMINs");

            // check if userManager creates user successfully , assign role Admin to this user
            if (createUserAdmin.Succeeded)
            {
                usermanager.AddToRole(user.Id, "Admin");
            }
        }

    }
}
