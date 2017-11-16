using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Areas.Admin.Controllers;
using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Areas.Admin.Services;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Models.ViewModels.AdminViewModels;
using SKSLearningSystem.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace SKSLearningSystem.Tests.Web.Areas.Admin.Controllers.AdminControllerTests
{
    [TestClass]
    public class AssignCourseToUsersPost_Should
    {
        [TestMethod]
        public void ReturnPartialViewWithModel()
        {
            //Arrange
            var userStore = new Mock<IUserStore<User>>();
            var adminServicesMock = new Mock<IAdminServices>();
            var gridServicesMock = new Mock<IGridServices>();
            var dbServicesMock = new Mock<IDBServices>();
            var courseName = "name";
            var course = new Course() { Name = courseName, Id=1 };
            var model = new AssignCourseToUsersViewModel();
            model.CourseId = course.Id;
            var applicationUserManagerMock = new Mock<ApplicationUserManager>(userStore.Object);
            var usersViewModel = new List<UserViewModel>();
            model.Users = usersViewModel;

            var controller = new AdminController(adminServicesMock.Object, applicationUserManagerMock.Object, gridServicesMock.Object,
                dbServicesMock.Object);
            dbServicesMock.Setup(x => x.GetCoursesFromDBByName(courseName)).Returns(course);
            dbServicesMock.Setup(x => x.GetUserViewModels()).Returns(usersViewModel);

            //Act & Assert
            controller
                .WithCallTo(x => x.AssignCourseToUsers(courseName))
                .ShouldRenderPartialView("Assigning")
                .WithModel<AssignCourseToUsersViewModel>(x=>x.Users==usersViewModel
                &&x.CourseId==course.Id);
        }
    }
}
