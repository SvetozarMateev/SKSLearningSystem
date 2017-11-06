using SKSLearningSystem.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Areas.Admin.Services
{
   public interface IAdminServices
    {
        bool ValidateInputFiles(UploadCourseViewModel model);

       // Course ReadCourseFromJSON(UploadCourseViewModel model);
    }
}
