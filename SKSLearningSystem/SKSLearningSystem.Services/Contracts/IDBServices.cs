using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Models.ViewModels;
using SKSLearningSystem.Models.ViewModels.AdminViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SKSLearningSystem.Services.Contracts
{
    public interface IDBServices
    {
        Course GetCoursesFromDB(int? courseId);

        ICollection<Image> GetImages(int? courseId);

        string GetCourseName(int courseId);

        Task<Image> GetImageByID(int id);

        IEnumerable<User> GetUsersFromDB(string[] userId);

        AssignCourseViewModel GetUsersAndCoursesFromDB();

        List<SingleCourseViewModel> GetCoursesFromDb();

        Course GetCoursesFromDBByName(string courseName);

        IList<UserViewModel> GetUserViewModels();
        void SaveAssignementsToDb(int courseId, IList<UserViewModel> users);
        void SaveAssignementsForDepartment(DepToCourseViewModel model);
        CourseState GetStateFromDB(int courStateId);
        void SaveAssignementsToDb(CourseState state);
    }
}
