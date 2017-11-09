using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Data;
using System.IO;
using System.Drawing;
using SKSLearningSystem.Models;

namespace SKSLearningSystem.Services.CourseServices
{
    public class CourseService : ICourseService
    {
        private LearningSystemDbContext context;

        public string GetCourseName(int courseId)
        {
            // Get the assigned from admin course to user
            var assignedCourse = this.context.Courses.First(c => c.Id == courseId);
            var courseName = assignedCourse.Name;

            return courseName;
        }

        public ICollection<Bitmap> GetImages(TakeCourseModel model)
        {

            var courseName = model.CourseName;
            var images = this.context.Courses.First(c => c.Name == courseName).Images;

            var imagesAsBitmap = new List<Bitmap>();

            foreach (var image in images)
            {
                //using (var ms = new MemoryStream(image))
                //{
                //    return Image.FromStream(ms);
                //}

                Bitmap bitmapImage = (Bitmap)((new ImageConverter()).ConvertFrom(image.CurrentImage));
                imagesAsBitmap.Add(bitmapImage);
            }

            return imagesAsBitmap;
        }
    }
}
