using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Areas.Admin.Services;
using SKSLearningSystem.Controllers;
using SKSLearningSystem.Models.ViewModels;
using SKSLearningSystem.Services;
using SKSLearningSystem.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TestStack.FluentMVCTesting;

namespace SKSLearningSystem.Tests.Controllers.HomeControllerTests
{
    [TestClass]
    public class MyProfilePost_Should
    {
        [TestMethod]
        public void ReturnDefaultView_WithCorrectModel()
        {
            // Arrange
            var homeServiceMock = new Mock<IHomeServices>();
            var adminServiceMock = new Mock<IAdminServices>();
            var dbServiceMock = new Mock<IDBServices>();

            var imageMock = new Mock<HttpPostedFileBase>();
            var myProfileViewModel = new MyProfileViewModel();


            var controller = new HomeController(homeServiceMock.Object, adminServiceMock.Object, dbServiceMock.Object);
           // homeServiceMock.Setup(h => h.ReadImagesFromFiles(imageMock.Object));

            //var controller = new HomeController(homeServiceMock.Object);

            // Act & Assert
        //    controller.WithCallTo(c => c.MyProfile(myProfileViewModel, imageMock.Object)).ShouldRenderDefaultView();
        }
    }
}
