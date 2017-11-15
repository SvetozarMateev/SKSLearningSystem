using Bytes2you.Validation;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Models;
using SKSLearningSystem.Services.CourseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Data.Entity;
using SKSLearningSystem.Areas.Admin.Models;
using Microsoft.AspNet.Identity;
using SKSLearningSystem.Services.Contracts;

namespace SKSLearningSystem.Controllers
{
    public class CourseController : Controller
    { 
        private readonly ICourseService services;
        private readonly IDBServices dBServices;

        public CourseController(ICourseService services,IDBServices dBServices)
        {
            Guard.WhenArgument(services, "services").IsNull().Throw();                   
            this.services = services;
            this.dBServices = dBServices;
        }

        // GET: Course
        [Authorize]
        public ActionResult TakeCourse(int courseStateId, int courseId)
        {
            TakeCourseModel model = new TakeCourseModel() { CourseStateId=courseStateId};
            var course = this.dBServices.GetCoursesFromDB(courseId);
            var images = this.dBServices.GetImages(courseId);
            //var user=
            model.CourseName = course.Name;

            model.Images = images;

            return this.View(model);
        }

        [Authorize]
        public ActionResult TakeCourseAsState( int courseStateId)
        {

            var state = this.dBServices.GetStateFromDB(courseStateId);
            state.State = "Started";
            this.dBServices.SaveAssignementsToDb(state);
            return RedirectToAction("TakeCourse",new { courseStateId =state.Id, courseId=state.CourseId });
        }

        public async Task<ActionResult> RenderImage(int id)
        {
            Image image = await this.dBServices.GetImageByID(id);

            byte[] currentImage = image.CurrentImage;

            return this.File(currentImage, "image/png");
        }

        [HttpGet]
        [Authorize]
        public ActionResult TakeExam(int courseStateId)
        {
            
            var TakeTestViewModel =  this.services.GetTestViewModel(courseStateId);
            return this.View(TakeTestViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult TakeExam(TakeTestViewModel TakeTestViewModel)
        {
            var IsTestStateValid = this.services.ValidateTest(TakeTestViewModel);

            if (IsTestStateValid == false)
            {
                this.ModelState.AddModelError("answers", "Please select one answer per question");
                return this.PartialView("InvalidTest",TakeTestViewModel);
            }
            var grade = this.services.GradeExam(TakeTestViewModel);
            TakeTestViewModel.Grade = grade;
            if (grade >= 50)
            {
                
                this.services.ChangeCourseState(TakeTestViewModel.CourseStateId,"Completed",grade);
                return this.PartialView("PassedTest",TakeTestViewModel);
            }
            else
            {
                return this.PartialView("FailedTest",TakeTestViewModel);
            }
        }
    }
}