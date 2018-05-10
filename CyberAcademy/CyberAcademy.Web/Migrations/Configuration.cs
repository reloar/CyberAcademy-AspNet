namespace CyberAcademy.Web.Migrations
{
    using CyberAcademy.Web.DataAccess;
    using CyberAcademy.Web.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CyberAcademy.Web.DataAccess.AcademyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CyberAcademy.Web.DataAccess.AcademyDbContext context)
        {
            string username = "admin@cyberspace.com";
            string password = "admin";
            string role = "ADMIN";

            //IUserStore<Contact> contactStore = new UserStore<Contact>(new AcademyDbContext());
            //UserManager<Contact, string> userMgr = new UserManager<Contact, string>(contactStore);
            var userMgr = Startup.UserManagerFactory.Invoke();

            var userRole = Startup.RoleManagerFactory.Invoke();

            if (userMgr.FindByName(username) != null)
                return;

            var appUser = new AppUser()
            {
                UserName = username,
                CreatedOn = DateTime.Now

            };


         //  var contact = Contact.Create(username, DateTime.Now);
            var result = userMgr.Create(appUser, password);

            //var roleStore = new RoleStore<IdentityRole>(new AcademyDbContext());
            //var roleManager = new RoleManager<IdentityRole>(roleStore);


            if (!userRole.RoleExists(role))
            {
                var irole = new AppRole() { Name = role };
                userRole.Create<AppRole, Guid>(irole);
            }

            if (!userMgr.IsInRole<AppUser, Guid>(appUser.Id, role))
            {
                userMgr.AddToRole<AppUser, Guid>(appUser.Id, role);
            }








            //var roleMgr = Startup.RoleManagerFactory.Invoke();

            //if (!roleMgr.RoleExists(role))
            //{
            //    var irole = new IdentityRole() { Name = role };
            //    roleManager.Create(irole);
            //}

            //if (!userMgr.IsInRole(contact.Id, role))
            //{
            //    userMgr.AddToRole(contact.Id, role);
            //}

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
