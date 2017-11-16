using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
    public class ReadImagesFromFile_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenModelIsNull()
        {
            //Arrange
            var dbMock = new Mock<LearningSystemDbContext>();
            var services = new AdminServices(dbMock.Object);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => services.ReadImagesFromFiles(null));
        }

        [TestMethod]
        public void ReturnImages_WhenParametersAreCorrect()
        {
            //Arrange
            var dbMock = new Mock<LearningSystemDbContext>();
            var services = new AdminServices(dbMock.Object);
            var model = new UploadCourseViewModel();
            var imageMock = new Mock<HttpPostedFileBase>();

            MemoryStream fileStream = new MemoryStream();
            byte[] imageArray = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                imageArray[i] = (byte)i;
            }
            fileStream.Write(imageArray, 0, 256);
            fileStream.Position = 0;

            imageMock.SetupGet(i => i.InputStream).Returns(fileStream);

            List<HttpPostedFileBase> files = new List<HttpPostedFileBase>() { imageMock.Object };

            AdminServices service = new AdminServices(dbMock.Object);

            // Act
            var result = service.ReadImagesFromFiles(files);

            // Assert
            CollectionAssert.AreEquivalent(imageArray, result.Single().CurrentImage);
        }
    }

    
}
