namespace PortFolio_A1_Version2._0.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using PortFolio_A1_Version2._0.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<PortFolio_A1_Version2._0.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PortFolio_A1_Version2._0.Models.ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // If the admin role doesn't exist, create it
            if (!roleManager.RoleExists("Doctor"))
            {
                roleManager.Create(new IdentityRole("Doctor"));
            }

            // If the user role doesn't exist, create it
            if (!roleManager.RoleExists("Patient"))
            {
                roleManager.Create(new IdentityRole("Patient"));
            }

            // Check if the admin user exists
            var adminUser = userManager.FindByEmail("admin@example.com");
            if (adminUser == null)
            {
                // Create the admin user
                adminUser = new ApplicationUser { UserName = "admin@example.com", Email = "admin@example.com" };
                var adminResult = userManager.Create(adminUser, "Admin@12345");

                // Assign the admin user to the Admin role
                if (adminResult.Succeeded)
                {
                    userManager.AddToRole(adminUser.Id, "Doctor");
                }
            }
        }

    }
}
