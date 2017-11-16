using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Areas.Admin.Controllers;
using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Areas.Admin.Services;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Services.Contracts;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;

namespace SKSLearningSystem.Tests.Areas.Admin.Controllers.AdminControllerTests
{
    [TestClass]
     public class UploadCoursePost_Should
     {
        [TestMethod]
        public void AddErrorToModelState_WhenFileNameIsInvalid()
        {
            //Arrange
            var userStore = new Mock<IUserStore<User>>();
            var adminServicesMock = new Mock<IAdminServices>();
            var gridServicesMock = new Mock<IGridServices>();
            var dbServicesMock = new Mock<IDBServices>();

          
            var viewModelMock = new UploadCourseViewModel();
            var applicationUserManagerMock = new Mock<ApplicationUserManager>(userStore.Object);
            var controller = new AdminController(adminServicesMock.Object,
                applicationUserManagerMock.Object,gridServicesMock.Object,dbServicesMock.Object);
            adminServicesMock.Setup(x => x.ValidateInputFiles(viewModelMock)).Returns(false);
            var expected = 1;

            //Act
            controller.UploadCourse(viewModelMock);

            //Assert
            Assert.AreEqual(expected, controller.ModelState.Count);
        }

        [TestMethod]
        public void ValidateNameExtensions()
        {
            //Arrange
            var userStore = new Mock<IUserStore<User>>();
            var adminServicesMock = new Mock<IAdminServices>();
            var gridServicesMock = new Mock<IGridServices>();
            var dbServicesMock = new Mock<IDBServices>();

           
            var viewModelMock = new UploadCourseViewModel();
            var applicationUserManagerMock = new Mock<ApplicationUserManager>(userStore.Object);
            var controller = new AdminController(adminServicesMock.Object,
                applicationUserManagerMock.Object,gridServicesMock.Object,dbServicesMock.Object);

            //Act 
            controller.UploadCourse(viewModelMock);

            //Assert
            adminServicesMock.Verify(x => x.ValidateInputFiles(viewModelMock), Times.Once);
        }

        [TestMethod]
        public void SaveCourseToDb_WhenFilesAreCorrect()
        {
            //Arrange
            var userStore = new Mock<IUserStore<User>>();
            var adminServicesMock = new Mock<IAdminServices>();
            var gridServicesMock = new Mock<IGridServices>();
            var dbMock = new Mock<LearningSystemDbContext>();
            var dbServicesMock = new Mock<IDBServices>();

            var images = new Image();
            var viewModelMock = new UploadCourseViewModel();
            var courseMock = new Course();
            var applicationUserManagerMock = new Mock<ApplicationUserManager>(userStore.Object);

            var controller = new AdminController(adminServicesMock.Object,
                applicationUserManagerMock.Object,gridServicesMock.Object,dbServicesMock.Object);
            adminServicesMock.Setup(x => x.ValidateInputFiles(viewModelMock)).Returns(true);
            adminServicesMock.Setup(x => x.ReadCourseFromJSON(viewModelMock.CourseFile)).Returns(courseMock);
            adminServicesMock.Setup(x => x.ReadImagesFromFiles(viewModelMock.Photos)).Returns(new List<Image>() { images });
            //Act 
            controller.UploadCourse(viewModelMock);

            //Assert
            adminServicesMock.Verify(x => x.SaveCourseToDB(courseMock), Times.Once);
        }

        [TestMethod]
        public void ReturnDefaultView()
        {
            //Arrange
            var userStore = new Mock<IUserStore<User>>();
            var adminServicesMock = new Mock<IAdminServices>();
            var gridServicesMock = new Mock<IGridServices>();
            var dbServicesMock = new Mock<IDBServices>();


            var viewModelMock = new UploadCourseViewModel();
            var applicationUserManagerMock = new Mock<ApplicationUserManager>(userStore.Object);
            var controller = new AdminController(adminServicesMock.Object,
                applicationUserManagerMock.Object,  gridServicesMock.Object,dbServicesMock.Object);

            adminServicesMock.Setup(x => x.ValidateInputFiles(viewModelMock)).Returns(true);

            //Act & Assert
            controller
                .WithCallTo(c => c.UploadCourse())
                .ShouldRenderDefaultView();
        }
    }
}
