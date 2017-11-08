using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Data.Models;
using System.Collections.Generic;

namespace SKSLearningSystem.Areas.Admin.Services
{
    public interface IAdminServices
    {
        ICollection<Image> ReadImagesFromFiles(UploadCourseViewModel model);

        Course ReadCourseFromJSON(UploadCourseViewModel model);

        bool ValidateInputFiles(UploadCourseViewModel model);

        void SaveCourseToDB(Course course);        
    }
}
