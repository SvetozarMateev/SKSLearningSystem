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
    }
}