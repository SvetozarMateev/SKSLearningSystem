using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Areas.Admin.Controllers;
using SKSLearningSystem.Areas.Admin.Services;
using SKSLearningSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var adminServicesMock = new Mock<IAdminServices>();
            var gridServicesMock = new Mock<IGridServices>();
            var dbMock = new Mock<LearningSystemDbContext>();
            var controller = new AdminController(adminServicesMock.Object, dbMock.Object, gridServicesMock.Object);
            //Act & Assert
            controller
                .WithCallTo(c => c.MonitorUsersProgress())
                .ShouldRenderDefaultView();
        }
    }
}
