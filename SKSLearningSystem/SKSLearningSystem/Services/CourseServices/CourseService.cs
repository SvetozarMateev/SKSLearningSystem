using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Data;
using System.IO;
using SKSLearningSystem.Models;
using Bytes2you.Validation;

namespace SKSLearningSystem.Services.CourseServices
{
    public class CourseService : ICourseService
    {
        private LearningSystemDbContext context;

        public CourseService(LearningSystemDbContext context)
        {
            Guard.WhenArgument(context, "context").IsNull().Throw();

            this.context = context;
        }

        public string GetCourseName(int courseId)
        {
            // Get the assigned from admin course to user
            var assignedCourse = this.context.Courses.First(c => c.Id == courseId);
            var courseName = assignedCourse.Name;

            return courseName;
        }

        public ICollection<Image> GetImages(int courseId)
        {
            var images = this.context.Courses.First(c => c.Id == courseId).Images;

            return images.ToList();
        }
    }
}
