using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Controllers;
using SKSLearningSystem.Data;
using SKSLearningSystem.Services.CourseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Tests.Services.CourseServices.CourseServiceTests
{
    [TestClass]
    public class ConstructorShould
    {
        [TestMethod]
        public void ThrowException_WhenDbContextIsNull()
        {
            // Arrange
            var contextMock = new Mock<LearningSystemDbContext>();
            var serviceMock = new Mock<ICourseService>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new CourseController(serviceMock.Object, null));
        }

        [TestMethod]
        public void ThrowException_WhenCourseServiceIsNull()
        {
            // Arrange
            var contextMock = new Mock<LearningSystemDbContext>();
            var serviceMock = new Mock<ICourseService>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new CourseController(null, contextMock.Object));
        }

        [TestMethod]
        public void ReturnInstance_WhenParametersAreCorrect()
        {
            // Arrange
            var contextMock = new Mock<LearningSystemDbContext>();
            var serviceMock = new Mock<ICourseService>();

            //Act
            var controller = new CourseController(serviceMock.Object, contextMock.Object);

            //Assert
            Assert.IsNotNull(controller);
        }
    }
}
