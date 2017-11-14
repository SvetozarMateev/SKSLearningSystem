using Bytes2you.Validation;
using Microsoft.AspNet.Identity;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Models.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SKSLearningSystem.Services
{
    public class HomeServices :IHomeServices
    {
        private LearningSystemDbContext context;

        public HomeServices(LearningSystemDbContext context)
        {
            this.context = context;   
        }
        public List<SingleCourseViewModel> GetCoursesFromDb()
        {
            var courses = context.Courses.Select(x=> new SingleCourseViewModel() {
               CourseStateId=x.Id,
                CourseName=x.Name,
                Descrtiption=x.Description,
                CourseImageId=x.Images.FirstOrDefault().Id
            }).Take(10).ToList();
            return courses;
        }

        public MyProfileViewModel GetCourseStates()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var myProfileViewModel = new MyProfileViewModel();
            myProfileViewModel.CourseStates =  context.CourseStates
                .Where(x => x.UserId == userId)
                .Select(x=>new CourseSateViewModel()
                {
                    Grade=x.Grade,
                    Passed=x.Passed,
                    DueDate=x.DueDate,
                    AssignmentDate=x.AssignmentDate,
                    CompletionDate=x.CompletionDate,
                    CourseName=x.Course.Name,
                    Mandatory=x.Mandatory,
                    State=x.State
                }).ToList();

            return myProfileViewModel;
        }

        public void ReadImagesFromFiles(HttpPostedFileBase file)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            Guard.WhenArgument(file, "file").IsNull().Throw();

            Image image = new Image();

            using (MemoryStream ms = new MemoryStream())
            {
                file.InputStream.CopyTo(ms);
                byte[] array = ms.GetBuffer();
                image = new Image() { CurrentImage = array };
            }

            image.UserId = userId;

            context.Images.Add(image);
            context.SaveChanges();
        }
    }
}