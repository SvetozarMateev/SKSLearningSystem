using SKSLearningSystem.Models.ViewModels;
using System.Collections.Generic;

namespace SKSLearningSystem.Services
{
    public interface IHomeServices
    {
        List<SingleCourseViewModel> GetCoursesFromDb();
    }
}
