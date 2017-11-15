using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Models.ViewModels;
using System.Collections.Generic;
using System.Web;

namespace SKSLearningSystem.Services
{
    public interface IHomeServices
    {


        MyProfileViewModel GetCourseStates(string userId);

        void SaveImagesToUser(Image file, string userId);
    }
}
