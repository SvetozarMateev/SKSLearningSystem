using SKSLearningSystem.Data;
using SKSLearningSystem.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

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
                CourseName=x.Name,
                Descrtiption=x.Description,
                CourseImageId=x.Images.FirstOrDefault().Id
            }).Take(10).ToList();
            return courses;
        }
    }
}