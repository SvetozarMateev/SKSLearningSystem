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

namespace SKSLearningSystem.Controllers
{
    public class CourseController : Controller
    { 
        private readonly ICourseService services;
        private readonly LearningSystemDbContext context;

        public CourseController(ICourseService services, LearningSystemDbContext context)
        {
            Guard.WhenArgument(services, "services").IsNull().Throw();
            Guard.WhenArgument(context, "context").IsNull().Throw();
            
            this.services = services;
            this.context = context;
        }

        // GET: Course
        public ActionResult TakeCourse(TakeCourseModel model)
        {
            int courseId = 7;
            var course = this.context.Courses.First(c => c.Id == courseId);
            var images = this.services.GetImages(courseId);

            model.CourseName = course.Name;

            model.Images = images;

            return View(model);
        }

        public async Task<ActionResult> RenderImage(int id)
        {
            Image image = await context.Images.FirstAsync(x => x.Id == id);

            byte[] currentImage = image.CurrentImage;

            return File(currentImage, "image/png");
        }

        [HttpGet]
        public ActionResult TakeExam(int? courseStateId)
        {
            courseStateId = 14;
            var TakeTestViewModel =  this.services.GetTestViewModel(14);
            return this.View(TakeTestViewModel);
        }

        [HttpPost]
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
                this.services.ChangeCourseState(TakeTestViewModel.CourseStateId,"Completed");
                return this.PartialView("PassedTest",TakeTestViewModel);
            }
            else
            {
                return this.PartialView("FailedTest",TakeTestViewModel);
            }
        }
    }
}