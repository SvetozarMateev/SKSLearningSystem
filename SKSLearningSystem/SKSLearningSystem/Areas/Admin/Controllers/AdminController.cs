using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Areas.Admin.Services;
using SKSLearningSystem.Data;
using System.Web.Mvc;

namespace SKSLearningSystem.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminServices services;
        private readonly LearningSystemDbContext db;

        public AdminController(IAdminServices services, LearningSystemDbContext db)
        {
            this.services = services;
            this.db = db;
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
                var course = this.services.ReadCourseFromJSON(model);
                var images = this.services.ReadImagesFromFiles(model);
                course.Images = images;
                this.services.SaveCourseToDB(course);
            }
            else
            {
                this.ModelState.AddModelError("file", "You can upload only json, png or jpg files.");
            }

            return this.View();
        }
    }
}