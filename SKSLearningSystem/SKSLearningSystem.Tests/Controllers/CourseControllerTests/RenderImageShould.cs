using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Controllers;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Services.CourseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Tests.Controllers.CourseControllerTests
{
    [TestClass]
    public class RenderImageShould
    {
        [TestMethod]
        public async Task ReturnImageFile_WithCorrecParameters()
        {
            // Arrange
            var contextMock = new Mock<LearningSystemDbContext>();
            var courseServiceMock = new Mock<ICourseService>();

            var image = new Image();
            var currentImage = image.CurrentImage;
            var imageId = image.Id;

            var controller = new CourseController(courseServiceMock.Object, contextMock.Object);

            // Act
            await controller.RenderImage(imageId);

            // Assert


        }
    }
}
