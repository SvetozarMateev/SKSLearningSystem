using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKSLearningSystem.Data.Models;

namespace SKSLearningSystem.Services.Factories
{
    public class CourseStateFactory : ICourseStateFactory
    {
        public CourseState CreateCourseState(int courseId)
        {
            return new CourseState();
        }
    }
}
