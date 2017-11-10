using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Areas.Admin.Services;
using SKSLearningSystem.Data;
using System.Web.Mvc;

namespace SKSLearningSystem.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminServices services;
        private readonly LearningSystemDbContext db;
        private readonly IGridServices gridServices;

        public AdminController(IAdminServices services, LearningSystemDbContext db,IGridServices gridServices)
        {
            this.services = services;
            this.db = db;
            this.gridServices = gridServices;
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
                var course = this.services.ReadCourseFromJSON(model.CourseFile);
                var images = this.services.ReadImagesFromFiles(model.Photos);
                course.Images = images;
                this.services.SaveCourseToDB(course);
            }
            else
            {
                this.ModelState.AddModelError("file", "You can upload only json, png or jpg files.");
            }

            return this.View();
        }

       
        public ActionResult MonitorUsersProgress()
        {
            return this.View();
        }
     
        public ActionResult GetJSON(bool _search,int rows,int page,string filters)
        {
            if (_search == false)
            {               
                return Json(this.gridServices.SearchFalseResult(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(this.gridServices.SearchResultTrue(filters), JsonRequestBehavior.AllowGet);
            }         
        }
    }
}