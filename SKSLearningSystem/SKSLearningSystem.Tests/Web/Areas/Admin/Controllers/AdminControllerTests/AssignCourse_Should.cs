using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Areas.Admin.Controllers;
using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Areas.Admin.Services;
using SKSLearningSystem.Data.Models;
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
    public class AssignCourse_Should
    {
        [TestMethod]
        public void ReturnDefaultView()
        {
            //Arrange
            var userStore = new Mock<IUserStore<User>>();
            var adminServicesMock = new Mock<IAdminServices>();
            var gridServicesMock = new Mock<IGridServices>();
            var dbServicesMock = new Mock<IDBServices>();
            var model = new AssignCourseViewModel();

            var applicationUserManagerMock = new Mock<ApplicationUserManager>(userStore.Object);

            var controller = new AdminController(adminServicesMock.Object, applicationUserManagerMock.Object, gridServicesMock.Object,
                dbServicesMock.Object);

            dbServicesMock.Setup(x => x.GetUsersAndCoursesFromDB()).Returns(model);

            //Act & Assert
            controller
                .WithCallTo(x => x.AssignCourse())
                .ShouldRenderDefaultView().WithModel(model);
        }
    }
}
