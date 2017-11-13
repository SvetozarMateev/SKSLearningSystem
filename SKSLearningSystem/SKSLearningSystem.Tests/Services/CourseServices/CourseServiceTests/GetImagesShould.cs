using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Services.CourseServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Tests.Services.CourseServices.CourseServiceTests
{
    [TestClass]
    public class GetImagesShould
    {
        [TestMethod]
        public void ReturnCollectionOfImages_WithCorrectParameters()
        {
            // Arrange
            var contextMock = new Mock<LearningSystemDbContext>();
            var courseServiceMock = new Mock<ICourseService>();
            var courseMock = new Course();

            List<Course> courses = new List<Course>();
            courses.Add(courseMock);
            var courseSetMock = new Mock<DbSet<Course>>();
            courseSetMock.SetupData(courses);

            contextMock.Setup(c => c.Courses).Returns(courseSetMock.Object);
            var dbImages = contextMock.Object.Courses.First(c => c.Id == courseMock.Id).Images;
            // Act
            var images = courseServiceMock.Object.GetImages(courseMock.Id);

            // Assert
            Assert.AreSame(dbImages, images);
        }
    }
}
