using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Areas.Admin.Services;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace SKSLearningSystem.Tests.Areas.Admin.Services.AdminServicesTests
{
    [TestClass]
    public class ReadCourseFromJSON_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenModelIsNull()
        {
            //Arrange
            var dbMock = new Mock<LearningSystemDbContext>();
            var services = new AdminServices(dbMock.Object);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => services.ReadCourseFromJSON(null));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenModelCourseFileIsNull()
        {
            //Arrange
            var dbMock = new Mock<LearningSystemDbContext>();
            var services = new AdminServices(dbMock.Object);
            var model = new UploadCourseViewModel();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => services.ReadCourseFromJSON(model.CourseFile));
        }

        [TestMethod]
        public void ThrowException_WhenJsonIsEmpty()
        {
            //Arrange
            var dbMock = new Mock<LearningSystemDbContext>();
            var services = new AdminServices(dbMock.Object);
            var jsonFileMock = new Mock<HttpPostedFileBase>();
            var model = new UploadCourseViewModel();

            FileStream stream = new FileStream(@"..\..\Empty.json", FileMode.Open);
            jsonFileMock.Setup(x => x.InputStream).Returns(stream);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => services.ReadCourseFromJSON(model.CourseFile));

            stream.Dispose();
        }

        [TestMethod]
        public void ReturnCourse_WhenParametersAreCorrect()
        {
            //Arrange
            var dbMock = new Mock<LearningSystemDbContext>();
            var services = new AdminServices(dbMock.Object);
            var jsonFileMock = new Mock<HttpPostedFileBase>();

            Course expected = new Course() { Id = 1, Description = "desc", Name = "name" };
            var courseJson = JsonConvert.SerializeObject(expected);

            MemoryStream memoryStream = new MemoryStream();
            StreamWriter writer = new StreamWriter(memoryStream);
            writer.Write(courseJson);
            writer.Flush();
            
            memoryStream.Position = 0;

            jsonFileMock.SetupGet(m => m.InputStream).Returns(memoryStream);

            //Act 
            Course actual = services.ReadCourseFromJSON(jsonFileMock.Object);

            //Assert
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);

            writer.Dispose();
        }
    }
}
