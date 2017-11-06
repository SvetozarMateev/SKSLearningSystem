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
    }

    [TestMethod]
    public void ReturnImages_WhenParametersAreCorrect()
    {
        //Arrange
        var dbMock = new Mock<LearningSystemDbContext>();
        var services = new AdminServices(dbMock.Object);
        var model = new UploadCourseViewModel();
        var image = new Mock<HttpPostedFileBase>();

        ICollection<Image> expected = new List<Image>();
        foreach (var item in model.Photos)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                item.InputStream.CopyTo(ms);
                byte[] array = ms.GetBuffer();
                expected.Add(new Image() { CurrentImage = array });
            }
        }

        FileStream stream = new FileStream(@"..\..\MyTestPhoto.png", FileMode.Open);
        image.Setup(x => x.InputStream).Returns(stream);
    }
}
