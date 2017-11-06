using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Areas.Admin.Services;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Assert.ThrowsException<ArgumentNullException>(() => services.ReadCourseFromJSON(model));
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
            Assert.ThrowsException<ArgumentNullException>(() => services.ReadCourseFromJSON(model));

            stream.Dispose();
        }

        [TestMethod]
        public void ReturnCourse_WhenParametersAreCorrect()
        {
            //Arrange
            var dbMock = new Mock<LearningSystemDbContext>();
            var services = new AdminServices(dbMock.Object);
            var jsonFileMock = new Mock<HttpPostedFileBase>();
            var model = new UploadCourseViewModel();

            Course expected;
            using (StreamReader reader = new StreamReader(@"..\..\Full.json"))
            {
                var allContent = reader.ReadToEnd();
                expected = JsonConvert.DeserializeObject<Course>(allContent);
            }
            FileStream stream = new FileStream(@"..\..\Full.json", FileMode.Open);

            model.CourseFile = jsonFileMock.Object;

            jsonFileMock.Setup(x => x.InputStream).Returns(stream);

            //Act 
            var actual = services.ReadCourseFromJSON(model);

            //Assert
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.Questions.Count, actual.Questions.Count);

            stream.Dispose();
        }
    }
}
