using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace SKSLearningSystem.Services
{
    public interface IHomeServices
    {


        MyProfileViewModel GetCourseStates(string userId);

        Task SaveImagesToUser(Image file, string userId);
    }
}
