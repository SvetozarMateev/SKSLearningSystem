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
            var id = "validId";
            var model = new MyProfileViewModel();
            var image = new Image();
            list.Add(image);
            var controller = new HomeController(homeServicesMock.Object, adminServicesMock.Object, dbServicesMock.Object)
            {
               // GetUserId = () => id
            };

            homeServicesMock.Setup(x => x.GetCourseStates(id)).Returns(model);
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
            var id = "validId";
            var model = new MyProfileViewModel();
            var image = new Image();
            list.Add(image);
            var controller = new HomeController(homeServicesMock.Object, adminServicesMock.Object, dbServicesMock.Object)
            {
                
            };


            homeServicesMock.Setup(x => x.GetCourseStates(id)).Returns(model);
            adminServicesMock.Setup(x => x.ReadImagesFromFiles(new List<HttpPostedFileBase>() { fileMock.Object })).Returns(list);
            //Act 
            controller.MyProfile(model, fileMock.Object);

            //Assert
            homeServicesMock.Verify(x => x.SaveImagesToUser(image, id), Times.Once);
        }
    }
}
