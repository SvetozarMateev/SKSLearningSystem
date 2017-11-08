using Bytes2you.Validation;
using Newtonsoft.Json;
using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SKSLearningSystem.Areas.Admin.Services
{
    public class AdminServices:IAdminServices
    {
        private readonly LearningSystemDbContext db;

        public AdminServices(LearningSystemDbContext db)
        {
            Guard.WhenArgument(db, "db").IsNull().Throw();

            this.db = db;
        }
        
        public bool ValidateInputFiles(UploadCourseViewModel model)
        {
            if (model == null)
            {
                return false;
            }
            var supportedTypesForFile = new[] { "json" };
            var supportedTypesForPics = new[] { "png", "jpg", "jpeg" };

            var fileExt = Path.GetExtension(model.CourseFile.FileName).Substring(1);

            if (!supportedTypesForFile.Contains(fileExt.ToLower()))
            {
                return false;
            }

            foreach (var item in model.Photos)
            {
                var picExt = Path.GetExtension(item.FileName).Substring(1);

                if (!supportedTypesForPics.Contains(picExt))
                {
                    return false;
                }
            }
            return true;
        }

        public Course ReadCourseFromJSON(UploadCourseViewModel model)
        {
            Guard.WhenArgument(model, "model").IsNull().Throw();

            Guard.WhenArgument(model.CourseFile, "model").IsNull().Throw();


            var stream = model.CourseFile.InputStream;
            Course course;
            using (StreamReader reader = new StreamReader(stream))
            {
                var allContent = reader.ReadToEnd();
                Guard.WhenArgument(allContent, "allContent").IsNullOrWhiteSpace().IsEmpty().Throw();
                course = JsonConvert.DeserializeObject<Course>(allContent);
            }

            Guard.WhenArgument(course.Name, "course name").IsNullOrWhiteSpace().IsEmpty().Throw();
            Guard.WhenArgument(course.Description, "course description").IsNullOrWhiteSpace().IsEmpty().Throw();            

            return course;
        }

        public ICollection<Image> ReadImagesFromFiles(UploadCourseViewModel model)
        {
            Guard.WhenArgument(model, "model").IsNull().Throw();

            Guard.WhenArgument(model.Photos, "model").IsNull().Throw();

            ICollection<Image> images = new List<Image>();
            foreach (var item in model.Photos)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    item.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                    images.Add(new Image() { CurrentImage = array });
                }
            }
            return images;
        }

        public void SaveCourseToDB(Course course)
        {
            Guard.WhenArgument(course, "course").IsNull().Throw();
            this.db.Courses.Add(course);
            this.db.SaveChanges();
        }
    }
}