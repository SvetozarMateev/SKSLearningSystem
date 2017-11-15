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
            var courses = new List<Course>();

            byte[] imageAsArray = File.ReadAllBytes("../../solid-OOP_wall-skills.jpg");

            var image = new Image()
            {
                Id = 1,
                CurrentImage = imageAsArray
            };

            var images = new List<Image>() { image };

            var course = new Course()
            {
                Id = 1,
                Images = images
            };

            courses.Add(course);

            var courseSetMock = new Mock<DbSet<Course>>().SetupData(courses);
            var dbServicesMock = new Mock<IDBServices>();

            var imageSetMock = new Mock<DbSet<Image>>().SetupData(images);
            contextMock.Setup(c => c.Courses).Returns(courseSetMock.Object);
            contextMock.Setup(c => c.Images).Returns(imageSetMock.Object);

            var controller = new CourseController(courseServiceMock.Object,dbServicesMock.Object);
            
            // Act & Assert
            controller.WithCallTo(x => x.RenderImage(image.Id)).ShouldRenderAnyFile();
        }
    }
}
