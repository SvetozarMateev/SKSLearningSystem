using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Models.ViewModels.AdminViewModels;
using System.Collections.Generic;
using System.Web;

namespace SKSLearningSystem.Areas.Admin.Services
{
    public interface IAdminServices
    {
        ICollection<Image> ReadImagesFromFiles(IEnumerable<HttpPostedFileBase> model);

        Course ReadCourseFromJSON(HttpPostedFileBase model);

        bool ValidateInputFiles(UploadCourseViewModel model);

        void SaveCourseToDB(Course course);

        // Assign Course Methods Start

        void DeleteCourseStates(List<DeassignViewModel> model);

        void SaveAssignedCoursesToDb(AssignCourseViewModel assignCourseViewModel);
        // end

      
    }
}
