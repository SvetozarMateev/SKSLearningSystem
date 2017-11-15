using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SKSLearningSystem.Services.CourseServices
{
    public interface ICourseService
    {   
        TakeTestViewModel GetTestViewModel(int courseId);

        bool ValidateTest(TakeTestViewModel questions);

        double GradeExam(TakeTestViewModel questions);

        Task ChangeCourseState(int courseId,string newState,double grade);              
    }
}
