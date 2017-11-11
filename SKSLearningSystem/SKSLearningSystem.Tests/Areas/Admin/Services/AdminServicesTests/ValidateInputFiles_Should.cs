using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Areas.Admin.Services;
using SKSLearningSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SKSLearningSystem.Tests.Areas.Admin.Services.AdminServicesTests
{
    [TestClass]
   public class ValidateInputFiles_Should
    {
        [TestMethod]
        public void ReturnFalse_WhenModelIsNull()
        {
            //Arrange
            var dbMock = new Mock<LearningSystemDbContext>();

            var services = new AdminServices(dbMock.Object);

            var expected = false;
            //Act
            var actual=services.ValidateInputFiles(null);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReturnFalse_WhenFileExtensionIsNotValid()
        {
            //Arrange
            var dbMock = new Mock<LearningSystemDbContext>();
            var model = new UploadCourseViewModel();
            var services = new AdminServices(dbMock.Object);

            var fileMock = new Mock<HttpPostedFileBase>();

            fileMock.SetupGet(x => x.FileName).Returns("somename.invalid");
            model.CourseFile = fileMock.Object;

            var expected = false;
            //Act
            var actual = services.ValidateInputFiles(model);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReturnFalse_WhenImageFileExtensionIsNotValid()
        {
            //Arrange
            var dbMock = new Mock<LearningSystemDbContext>();
            var model = new UploadCourseViewModel();
            var services = new AdminServices(dbMock.Object);
            var fileMockNotLegit = new Mock<HttpPostedFileBase>();
            var fileMockLegit = new Mock<HttpPostedFileBase>();
            var filesMock = new List<HttpPostedFileBase>();
            var fileMockJSON = new Mock<HttpPostedFileBase>();

            fileMockJSON.SetupGet(x => x.FileName).Returns("somename.json");
            model.CourseFile = fileMockJSON.Object;
            fileMockNotLegit.SetupGet(x => x.FileName).Returns("somename.invalid");
            fileMockLegit.SetupGet(x => x.FileName).Returns("somename.png");
            filesMock.Add(fileMockNotLegit.Object);
            filesMock.Add(fileMockLegit.Object);
            model.Photos = filesMock;

            var expected = false;
            //Act
            var actual = services.ValidateInputFiles(model);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReturnTrue_WhenFileExtensionsAreCorrect()
        {
            //Arrange
            var dbMock = new Mock<LearningSystemDbContext>();
            var model = new UploadCourseViewModel();
            var services = new AdminServices(dbMock.Object);
            var fileMockNotLegit = new Mock<HttpPostedFileBase>();
            var fileMockLegit = new Mock<HttpPostedFileBase>();
            var filesMock = new List<HttpPostedFileBase>();
            var fileMockJSON = new Mock<HttpPostedFileBase>();

            fileMockJSON.SetupGet(x => x.FileName).Returns("somename.json");
            model.CourseFile = fileMockJSON.Object;
            fileMockNotLegit.SetupGet(x => x.FileName).Returns("somename.jpg");
            fileMockLegit.SetupGet(x => x.FileName).Returns("somename.png");
            filesMock.Add(fileMockNotLegit.Object);
            filesMock.Add(fileMockLegit.Object);
            model.Photos = filesMock;

            var expected = true;
            //Act
            var actual = services.ValidateInputFiles(model);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
