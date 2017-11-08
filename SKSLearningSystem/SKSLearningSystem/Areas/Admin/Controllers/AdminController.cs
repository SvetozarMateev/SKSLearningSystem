using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Areas.Admin.Services;
using SKSLearningSystem.Data;

namespace SKSLearningSystem.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminServices services;
        private readonly ApplicationUserManager userManager;
        private readonly LearningSystemDbContext context;

        public AdminController(IAdminServices services, ApplicationUserManager userManager, LearningSystemDbContext context)
        {
            this.services = services;
            this.userManager = userManager;
            this.context = context;
        }

        [HttpGet]
        public ActionResult UploadCourse()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult AssignCourse()
        {
            
            var users = this.userManager
                .Users
                .Select(u => new UserViewModel()
                {
                    UserName = u.UserName
                }).ToList();

            var courses = this.context
                .Courses
                .Select(c => new CourseViewModel() { Name = c.Name })
                .ToList();

            var assignCourseViewModel = new AssignCourseViewModel() {
                Courses = courses,
            Users = users
            };

            return this.View(assignCourseViewModel);
        }

        [HttpPost]
        public ActionResult CompleteAssignment(AssignCourseViewModel assignCourseViewModel)
        {
            return View(assignCourseViewModel);
        }

        [HttpPost]
        public ActionResult UploadCourse(UploadCourseViewModel model)
        {
            var IsValid = this.services.ValidateInputFiles(model);
            if (IsValid)
            {
                // var course = this.services.ReadCourseFromJSON(model);
                return PartialView("SuccessMessage",model);
            }
            else
            {
                this.ModelState.AddModelError("file", "You can upload only json, png or jpeg files.");
            }
          
                return this.View();
        }
    }
}