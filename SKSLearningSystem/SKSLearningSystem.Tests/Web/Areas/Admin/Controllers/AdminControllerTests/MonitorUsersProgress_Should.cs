using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Areas.Admin.Controllers;
using SKSLearningSystem.Areas.Admin.Services;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Services.Contracts;
using TestStack.FluentMVCTesting;

namespace SKSLearningSystem.Tests.Areas.Admin.Controllers.AdminControllerTests
{
    [TestClass]
    public class MonitorUsersProgress_Should
    {
        [TestMethod]
        public void ReturnDefaultView_WhenCalled()
        {
            //Arrange
            var userStore = new Mock<IUserStore<User>>();
            var adminServicesMock = new Mock<IAdminServices>();
            var gridServicesMock = new Mock<IGridServices>();
            var dbServicesMock = new Mock<IDBServices>();

            var applicationUserManagerMock = new Mock<ApplicationUserManager>(userStore.Object);
            var dbMock = new Mock<LearningSystemDbContext>();
            var controller = new AdminController(adminServicesMock.Object,
                applicationUserManagerMock.Object, gridServicesMock.Object,dbServicesMock.Object);
            //Act & Assert
            controller
                .WithCallTo(c => c.MonitorUsersProgress())
                .ShouldRenderDefaultView();
        }
    }
}
 