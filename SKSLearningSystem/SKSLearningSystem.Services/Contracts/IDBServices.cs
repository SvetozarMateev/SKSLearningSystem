using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Models.ViewModels;
using SKSLearningSystem.Models.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SKSLearningSystem.Services.Contracts
{
    public interface IDBServices
    {
        Course GetCoursesFromDB(int courseId);

        List<Image> GetImages(int courseId);

        string GetCourseName(int courseId);

        Task<Image> GetImageByID(int id);

        IEnumerable<User> GetUsersFromDB(string[] userId);

        AssignCourseViewModel GetUsersAndCoursesFromDB();

        List<SingleCourseViewModel> GetCoursesFromDb();

        Course GetCoursesFromDBByName(string courseName);

        IList<UserViewModel> GetUserViewModels();
        Task SaveAssignementsToDb(int courseId, IList<UserViewModel> users);
        Task SaveAssignementsForDepartment(DepToCourseViewModel model);
        CourseState GetStateFromDB(int courStateId);
        Task SaveAssignementsToDb(CourseState state);

        List<CourseState> GetAllStates();

         void SaveToFile(Object obj);
    }
}
