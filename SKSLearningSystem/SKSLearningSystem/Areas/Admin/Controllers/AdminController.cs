using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Areas.Admin.Services;

namespace SKSLearningSystem.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminServices services;
        public AdminController(IAdminServices services)
        {
            this.services = services;
        }

        [HttpGet]
        public ActionResult UploadCourse()
        {
            return this.View();
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