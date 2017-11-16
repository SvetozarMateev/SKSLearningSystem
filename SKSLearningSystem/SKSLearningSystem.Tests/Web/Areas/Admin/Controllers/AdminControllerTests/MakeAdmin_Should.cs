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
    public class MakeAdmin_Should
    {
        [TestMethod]
        public void MakeUserAdmin_WhenUserIsMarked()
        {
            //Arrange
            var userStore = new Mock<IUserStore<User>>();
            var adminServicesMock = new Mock<IAdminServices>();
            var gridServicesMock = new Mock<IGridServices>();
            var dbServicesMock = new Mock<IDBServices>();
            var ids = new string[] { "1", "2" };
            var model = new AssignCourseViewModel();
            model.Users = new List<UserViewModel>();
            model.Users.Add(new UserViewModel() { Checked = true, Id = "1" });
            model.Users.Add(new UserViewModel() { Checked = false, Id = "2" });
            var user = new User() { Id = "1" };
            var user2 = new User() { Id = "2" };

            var users = new List<User>() { user, user2 };
            var applicationUserManagerMock = new Mock<ApplicationUserManager>(userStore.Object);

            var controller = new AdminController(adminServicesMock.Object, applicationUserManagerMock.Object, gridServicesMock.Object,
                dbServicesMock.Object);
            dbServicesMock.Setup(x => x.GetUsersFromDB(ids)).Returns(users);
            //Act
            controller.MakeAdmin(model);

            //Assert
            applicationUserManagerMock.Verify(x => x.AddToRoleAsync("1", "Admin"));
        }

        [TestMethod]
        public void RedirectToAction()
        {
            //Arrange
            var userStore = new Mock<IUserStore<User>>();
            var adminServicesMock = new Mock<IAdminServices>();
            var gridServicesMock = new Mock<IGridServices>();
            var dbServicesMock = new Mock<IDBServices>();
            var applicationUserManagerMock = new Mock<ApplicationUserManager>(userStore.Object);

            var ids = new string[] { "1", "2" };
            var model = new AssignCourseViewModel();
            model.Users = new List<UserViewModel>();
            model.Users.Add(new UserViewModel() { Checked = true, Id = "1" });
            model.Users.Add(new UserViewModel() { Checked = false, Id = "2" });
            var user = new User() { Id = "1" };
            var user2 = new User() { Id = "2" };

            var users = new List<User>() { user, user2 };
            dbServicesMock.Setup(x => x.GetUsersFromDB(ids)).Returns(users);
            applicationUserManagerMock.Setup(x => x.GetRolesAsync("2")).ReturnsAsync(new List<string>() { "Admin" });
            var controller = new AdminController(adminServicesMock.Object, applicationUserManagerMock.Object, gridServicesMock.Object,
               dbServicesMock.Object);
            //Act & Assert
            controller.WithCallTo(x => x.MakeAdmin(model))
                .ShouldRedirectTo(x => x.AssignRoles());
        }
    }
}
