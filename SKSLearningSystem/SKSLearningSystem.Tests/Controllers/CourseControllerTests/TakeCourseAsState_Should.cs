using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Controllers;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Services.Contracts;
using SKSLearningSystem.Services.CourseServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;
using SKSLearningSystem.Data;

namespace SKSLearningSystem.Tests.Controllers.CourseControllerTests
{
    [TestClass]
    public class TakeCourseAsState_Should
    {
        [TestMethod]
        public void RedirectToTakeCourseAction_WhenParametersAreCorrect()
        {
            // Arrange
            var courseServiceMock = new Mock<ICourseService>();
            var dbSevrviceMock = new Mock<IDBServices>();
            var contextMock = new Mock<LearningSystemDbContext>();

            var courseStateId = 1;
            var courseId = 1;

            var courseState = new CourseState() { Id = courseStateId };

            var courseStates = new List<CourseState>() { courseState };

            var courseStateSetMock = new Mock<DbSet<CourseState>>();
            courseStateSetMock.SetupData(courseStates);

            contextMock.Setup(c => c.CourseStates).Returns(courseStateSetMock.Object);

            var controller = new CourseController(courseServiceMock.Object, dbSevrviceMock.Object);

            // Act & Assert
            controller
                .WithCallTo(c => c.TakeCourseAsState(courseStateId))
                .ShouldRedirectTo(c => c.TakeCourse(courseStateId, courseId));
        }
    }
}
