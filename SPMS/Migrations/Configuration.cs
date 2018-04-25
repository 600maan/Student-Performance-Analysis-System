namespace SPMS.Migrations
{
    using SPMS.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<SPMS.Models.UsersContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SPMS.Models.UsersContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            
            SeedMembership();

            context.schoolroles.AddOrUpdate(r => r.RolesName,
                new SchoolRoles { RolesName = "Admin" },
                new SchoolRoles { RolesName = "Teacher" },
                   new SchoolRoles { RolesName = "ClassTeacher" }
             
                );

        }

        private void SeedMembership()
        {

            WebSecurity.InitializeDatabaseConnection("DefaultConnection",
                "UserProfile", "UserID", "UserName", autoCreateTables: true);
            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            if (!roles.RoleExists("Admin"))
            {
                roles.CreateRole("Admin");
            }
            if (!roles.RoleExists("Teacher"))
            {
                roles.CreateRole("Teacher");
            }
            if (!roles.RoleExists("ClassTeacher"))
            {
                roles.CreateRole("ClassTeacher");
            }
            if (membership.GetUser("admin", false) == null)
            {
                membership.CreateUserAndAccount("admin", "admin");
            }

            if (!roles.GetRolesForUser("admin").Contains("Admin"))
            {
                roles.AddUsersToRoles(new[] { "admin" }, new[] { "Admin" });
            }

        }
    }
}
