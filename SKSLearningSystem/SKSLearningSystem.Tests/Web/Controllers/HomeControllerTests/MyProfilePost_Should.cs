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
using SKSLearningSystem.Data.Models;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Routing;

namespace SKSLearningSystem.Tests.Web.Controllers.HomeControllerTests
{
    [TestClass]
    public class MyProfilePost_Should
    {
        [TestMethod]
        public void ReturnDefaultViewWithModel()
        {
            //Arrange
            var homeServicesMock = new Mock<IHomeServices>();
            var adminServicesMock = new Mock<IAdminServices>();
            var dbServicesMock = new Mock<IDBServices>();
            var fileMock = new Mock<HttpPostedFileBase>();
            var list = new List<Image>();
            var name = "validusername";
            var model = new MyProfileViewModel();
            var image = new Image();
            list.Add(image);
            var httpContext = new Mock<HttpContextBase>();
            var mockIdentity = new Mock<IIdentity>();
            httpContext.SetupGet(x => x.User.Identity).Returns(mockIdentity.Object);
            mockIdentity.Setup(x => x.Name).Returns(name);

            var controller = new HomeController(homeServicesMock.Object, adminServicesMock.Object, dbServicesMock.Object);
            controller.ControllerContext = new ControllerContext(httpContext.Object,
                                                                    new RouteData(), controller);

            homeServicesMock.Setup(x => x.GetCourseStates(name)).Returns(model);
            adminServicesMock.Setup(x => x.ReadImagesFromFiles(new List<HttpPostedFileBase>() { fileMock.Object })).Returns(list);
            //Act & Assert
            controller.WithCallTo(x => x.MyProfile(model,fileMock.Object)).ShouldRenderDefaultView().WithModel(model);
        }

        [TestMethod]
        public void InvokesMethodFOrSavingToDB()
        {
            //Arrange
            var homeServicesMock = new Mock<IHomeServices>();
            var adminServicesMock = new Mock<IAdminServices>();
            var dbServicesMock = new Mock<IDBServices>();
            var fileMock = new Mock<HttpPostedFileBase>();
            var list = new List<Image>();
            var username = "validName";
            var model = new MyProfileViewModel();
            var image = new Image();
            list.Add(image);
            var httpContext = new Mock<HttpContextBase>();
            var mockIdentity = new Mock<IIdentity>();
            httpContext.SetupGet(x => x.User.Identity).Returns(mockIdentity.Object);
            mockIdentity.Setup(x => x.Name).Returns(username);
           
            var controller = new HomeController(homeServicesMock.Object, adminServicesMock.Object, dbServicesMock.Object);
            controller.ControllerContext = new ControllerContext(httpContext.Object,
                                                                    new RouteData(), controller);

            homeServicesMock.Setup(x => x.GetCourseStates(username)).Returns(model);
            adminServicesMock.Setup(x => x.ReadImagesFromFiles(new List<HttpPostedFileBase>() { fileMock.Object })).Returns(list);
            //Act 
            controller.MyProfile(model, fileMock.Object);

            //Assert
            homeServicesMock.Verify(x => x.SaveImagesToUser(image, username), Times.Once);
        }
    }
}
