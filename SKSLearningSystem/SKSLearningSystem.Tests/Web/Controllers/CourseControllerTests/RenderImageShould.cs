using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Controllers;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Services.CourseServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Testing;
using TestStack.FluentMVCTesting;
using System.IO;
using SKSLearningSystem.Services.Contracts;

namespace SKSLearningSystem.Tests.Controllers.CourseControllerTests
{
    [TestClass]
    public class RenderImageShould
    {
        [TestMethod]
        public void ReturnImageFile_WithCorrecParameters()
        {
            // Arrange
            var contextMock = new Mock<LearningSystemDbContext>();
            var courseServiceMock = new Mock<ICourseService>();

            byte[] imageAsArray = new byte[] { 1, 20, 10 };

            var image = new Image()
            {
                Id = 1,
                CurrentImage = imageAsArray
            };
            
            var dbServicesMock = new Mock<IDBServices>();

            dbServicesMock.Setup(c => c.GetImageByID(image.Id)).Returns(Task.FromResult(image));

            var controller = new CourseController(courseServiceMock.Object,dbServicesMock.Object);
            
            // Act & Assert
            controller.WithCallTo(x => x.RenderImage(image.Id)).ShouldRenderAnyFile();
        }
    }
}
