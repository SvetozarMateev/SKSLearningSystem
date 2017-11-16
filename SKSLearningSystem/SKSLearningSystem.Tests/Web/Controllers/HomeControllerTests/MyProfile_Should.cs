using Microsoft.AspNet.Identity;
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
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TestStack.FluentMVCTesting;

namespace SKSLearningSystem.Tests.Web.Controllers.HomeControllerTests
{
    [TestClass]
    public class MyProfile_Should
    {
        [TestMethod]
         public void ReturnDefaultViewWithModel()
        {
            //Arrange
            var homeServicesMock = new Mock<IHomeServices>();
            var adminServicesMock = new Mock<IAdminServices>();
            var dbServicesMock = new Mock<IDBServices>();
           
            var username = "valid name";
            var model = new MyProfileViewModel();
            var httpContext = new Mock<HttpContextBase>();
            var mockIdentity = new Mock<IIdentity>();
            httpContext.SetupGet(x => x.User.Identity).Returns(mockIdentity.Object);
            mockIdentity.Setup(x => x.Name).Returns(username);
           
            var controller = new HomeController(homeServicesMock.Object, adminServicesMock.Object, dbServicesMock.Object);
            controller.ControllerContext = new ControllerContext(httpContext.Object,
                                                                    new RouteData(), controller);

            homeServicesMock.Setup(x => x.GetCourseStates(username)).Returns(model);

            //Act & Assert
            controller.WithCallTo(x => x.MyProfile()).ShouldRenderDefaultView().WithModel(model);
        }
       
    }
}
