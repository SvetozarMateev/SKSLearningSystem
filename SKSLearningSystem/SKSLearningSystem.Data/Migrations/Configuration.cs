using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace SKSLearningSystem.Migrations
{


    public sealed class Configuration : DbMigrationsConfiguration<LearningSystemDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LearningSystemDbContext context)
        {
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
            if (context.Roles.Count() == 0)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var UserManager = new UserManager<User>(new UserStore<User>(context));
                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var course = new Course();
                var courseState = new CourseState();
                course.Name = "Seed";
                course.Description = "Seeded";
                courseState.Course = course;
                courseState.AssignmentDate = DateTime.Now;
                courseState.CompletionDate = DateTime.Now;
                courseState.DueDate = DateTime.Now;


                var course2 = new Course();
                var courseState2 = new CourseState();
                course2.Name = "Seed2";
                course2.Description = "Seeded2";
                courseState2.Course = course2;
                courseState2.AssignmentDate = DateTime.Now;
                courseState2.CompletionDate = DateTime.Now;
                courseState2.DueDate = DateTime.Now;

                context.Courses.Add(course);
                context.Courses.Add(course2);


                var user = new User();
                user.CourseStates = new List<CourseState>() {  };
                user.UserName = "adming@admin.com";
                user.Email = "adming@admin.com";

                string userPWD = "Admin123$";
                
                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }

                var user2 = new User();
                user2.CourseStates = new List<CourseState>() { };
                user2.UserName = "sir@sir.com";
                user2.Email = "sir@sir.com";

                string userPWD2 = "Sir123$";

                var chkUser2 = UserManager.Create(user2, userPWD2);
            }

        }
    }
}
