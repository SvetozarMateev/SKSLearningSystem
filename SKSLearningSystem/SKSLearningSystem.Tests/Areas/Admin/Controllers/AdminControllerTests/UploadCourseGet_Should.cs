using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Areas.Admin.Controllers;
using SKSLearningSystem.Areas.Admin.Services;
using SKSLearningSystem.Data;
using TestStack.FluentMVCTesting;

namespace SKSLearningSystem.Tests.Areas.Admin.Controllers.AdminControllerTests
{
    [TestClass]
    public class UploadCourseGet_Should
    {
        [TestMethod]
        public void ReturnDefaultView_WhenCalled()
        {
            //Arrange
            var adminServicesMock = new Mock<IAdminServices>();
            var gridServicesMock = new Mock<IGridServices>();
            var dbMock = new Mock<LearningSystemDbContext>();
            var controller = new AdminController(adminServicesMock.Object,dbMock.Object,gridServicesMock.Object);
            //Act & Assert
            controller
                .WithCallTo(c=>c.UploadCourse())
                .ShouldRenderDefaultView();
        }
    }
}
