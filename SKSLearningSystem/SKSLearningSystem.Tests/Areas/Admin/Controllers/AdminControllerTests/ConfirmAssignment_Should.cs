using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Areas.Admin.Controllers;
using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Areas.Admin.Services;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace SKSLearningSystem.Tests.Areas.Admin.Controllers.AdminControllerTests
{
    [TestClass]
   public class ConfirmAssignment_Should
    {
        [TestMethod]
        public void ReturnDefaultViewWithModel()
        {
            // Arrange
            var userStoreMock = new Mock<IUserStore<User>>();
            var adminServiceMock = new Mock<IAdminServices>();
            var userManagerMock = new Mock<ApplicationUserManager>(userStoreMock.Object);
            var contextMock = new Mock<LearningSystemDbContext>();
            var gridServicesMock = new Mock<IGridServices>();

            var assignCourseViewModel = new AssignCourseViewModel() { };

            var controller = new AdminController(adminServiceMock.Object, userManagerMock.Object,
                contextMock.Object, gridServicesMock.Object);

            adminServiceMock.Setup(x => x.GetUsersAndCoursesFromDB()).Returns(assignCourseViewModel);

            // Act & Assert
            controller.WithCallTo(x => x.ConfirmAssignment(assignCourseViewModel))
                .ShouldRenderDefaultView().WithModel(assignCourseViewModel);
        }
    }
}
