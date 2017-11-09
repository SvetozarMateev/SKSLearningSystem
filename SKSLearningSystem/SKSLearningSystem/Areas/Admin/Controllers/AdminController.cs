using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Areas.Admin.Services;
using SKSLearningSystem.Data;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

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

        [HttpGet]
        public ActionResult MonitorUsersProgress()
        {
            return this.View();
        }

        

        public ActionResult GetJSON(bool _search,int rows,int page,string filters)
        {

            if (_search == false)
            {
                var counter = 1;
                var users = db.Users.ToList();
                var result = new List<CourseStateRowViewModel>();
                foreach (var user in users)
                {
                    foreach (var courses in user.CourseStates)
                    {
                        if (courses.DueDate < DateTime.Now)
                        {
                            courses.State = "Overdue";
                        }
                        result.Add(new CourseStateRowViewModel()
                        {
                            Index = counter,
                            Username = user.UserName,
                            Coursename = courses.Course.Name,
                            AssignementDate = courses.AssignmentDate,
                            DueDate = courses.DueDate,
                            State = courses.State
                        });
                    }
                }
                var needed = new { total = 1, page = 1, records = result.Count, rows = result };
                return Json(needed, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var parsedFilters = JsonConvert.DeserializeObject<GridRequestViewModel>(filters);
                var counter = 1;
                var users = db.Users.ToList();
                var propertyName= parsedFilters.rules.First().field;
                var shortenedOperator = parsedFilters.rules.First().op;
                var inputedField = parsedFilters.rules.First().data;


                var result = new List<CourseStateRowViewModel>();
                foreach (var user in this.gridServices.Filtrator(propertyName, shortenedOperator, inputedField, users))
                {
                    foreach (var courses in user.CourseStates)
                    {
                        if (courses.DueDate < DateTime.Now)
                        {
                            courses.State = "Overdue";
                        }
                        result.Add(new CourseStateRowViewModel()
                        {
                            Index = counter,
                            Username = user.UserName,
                            Coursename = courses.Course.Name,
                            AssignementDate = courses.AssignmentDate,
                            DueDate = courses.DueDate,
                            State = courses.State
                        });
                    }
                }
                var needed = new { total = 1, page = 1, records = result.Count, rows = result };
                return Json(needed, JsonRequestBehavior.AllowGet);
            }         
        }
    }
}