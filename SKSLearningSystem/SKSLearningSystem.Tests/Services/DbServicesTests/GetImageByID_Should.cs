using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace SKSLearningSystem.Tests.Services.DbServicesTests
{
    [TestClass]
   public class GetImageByID_Should
    {
        [TestMethod]
      public  void ReturnCorrectImageByGivenId()
        {
            // Arrange
            var contextMock = new Mock<LearningSystemDbContext>();
            var dbSetMock = new Mock<DbSet<Image>>();
            var image = new Image() { Id = 1, CourseId = 2 };
            var images = new Collection<Image>
            {
                image
            };

            dbSetMock.SetupData(images);
            contextMock.Setup(c => c.Images).Returns(dbSetMock.Object);

            var sut = new DBServices(contextMock.Object);

            // Act
            var result = sut.GetImageByID(1);

            // Assert
            Assert.AreSame(image, result.Result);
        }
    }
}
