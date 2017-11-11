using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Models;
using System.Collections.Generic;

namespace SKSLearningSystem.Services.CourseServices
{
    public interface ICourseService
    {
        string GetCourseName(int courseId);

        ICollection<Image> GetImages(int courseId);

        IList<QuestionViewModel> GetQuestionsForCourse(int courseId);

        bool ValidateTest(IList<QuestionViewModel> questions);

        double GradeExam(IList<QuestionViewModel> questions);
    }
}
