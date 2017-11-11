using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Services.CourseServices
{
    public interface ICourseService
    {
        string GetCourseName(int courseId);

        ICollection<Bitmap> GetImages(TakeCourseModel model);

    }
}
