using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Models;
using System.Collections.Generic;

namespace SKSLearningSystem.Services.CourseServices
{
    public interface ICourseService
    {
        string GetCourseName(int courseId);

        ICollection<Image> GetImages(int? courseId);

        TakeTestViewModel GetTestViewModel(int courseId);

        bool ValidateTest(TakeTestViewModel questions);

        double GradeExam(TakeTestViewModel questions);

        void ChangeCourseState(int courseId,string newState);
    }
}
