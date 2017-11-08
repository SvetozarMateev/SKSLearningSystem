﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Areas.Admin.Services;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;

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
                    UserName = u.UserName,
                    Id = u.Id
                }).ToList();

            var courses = this.context
                .Courses
                .Select(c => new CourseViewModel()
                {
                    Name = c.Name,
                    Id = c.Id
                })
                .ToList();

            var assignCourseViewModel = new AssignCourseViewModel()
            {
                Courses = courses,
                Users = users
            };

            return this.View(assignCourseViewModel);
        }


        public ActionResult CompleteAssignment(AssignCourseViewModel assignCourseViewModel)
        {
            return View(assignCourseViewModel);
        }

        [HttpPost]
        public ActionResult SubmitAssignments(AssignCourseViewModel assignCourseViewModel)
        {
            var users = context.Users.Where(x => assignCourseViewModel.Users.Select(y => y.Id).ToList().Contains(x.Id)).ToList();
            var courses = context.Courses.Where(c => assignCourseViewModel.Courses.Select(cr => cr.Id).ToList().Contains(c.Id)).ToList();


            for (int i = 0; i < users.Count; i++)
            {
                for (int j = 0; j < courses.Count; j++)
                {
                    CourseState state = new CourseState()
                    {
                        User = users[i],
                        Course = courses[j]
                    };

                    users[i].CourseStates.Add(state);
                    courses[j].Registry.Add(state);
                }
            }

            context.SaveChanges();

            return RedirectToAction("AssignCourse");
        }

        [HttpPost]
        public ActionResult UploadCourse(UploadCourseViewModel model)
        {
            var IsValid = this.services.ValidateInputFiles(model);
            if (IsValid)
            {
                // var course = this.services.ReadCourseFromJSON(model);
                return PartialView("SuccessMessage", model);
            }
            else
            {
                this.ModelState.AddModelError("file", "You can upload only json, png or jpeg files.");
            }

            return this.View();
        }
    }
}