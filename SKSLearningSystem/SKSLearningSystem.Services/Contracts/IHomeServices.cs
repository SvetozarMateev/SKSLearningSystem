using SKSLearningSystem.Models.ViewModels;
using System.Collections.Generic;
using System.Web;

namespace SKSLearningSystem.Services
{
    public interface IHomeServices
    {
        List<SingleCourseViewModel> GetCoursesFromDb();

        MyProfileViewModel GetCourseStates();

        void ReadImagesFromFiles(HttpPostedFileBase file);
    }
}
