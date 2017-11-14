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

        public MyProfileViewModel GetCourseStates(MyProfileViewModel myProfileViewModel)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();

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