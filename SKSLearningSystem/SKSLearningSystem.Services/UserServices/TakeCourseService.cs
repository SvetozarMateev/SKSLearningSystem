using Bytes2you.Validation;
using System.Linq;
using SKSLearningSystem.Services.Factories;

namespace SKSLearningSystem.Services.UserServices
{
    public class TakeCourseService
    {
        //private readonly LearningSystemDbContext context;
        //private readonly ICourseStateFactory factory;

        //public TakeCourseService (LearningSystemDbContext context)
        //{
        //    Guard.WhenArgument(context, "context").IsNull().Throw();

        //    this.context = context;
        //}
        
        //public void TakeCourse(string username, string courseName)
        //{
        //    var user = context.Users.FirstOrDefault(u => u.UserName == username);

        //    var course = context.Courses.FirstOrDefault(c => c.Name == courseName);

        //    var courseState = this.factory.CreateCourseState(course.Id);

        //    //var courseState = context.CourseStates.FirstOrDefault(c => c.CourseId == course.Id);

        //    user.CourseStates.Add(courseState);
        //    this.context.SaveChanges();
        //}
    }
}
