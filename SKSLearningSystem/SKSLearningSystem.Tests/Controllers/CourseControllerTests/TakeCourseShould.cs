using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Controllers;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Models;
using SKSLearningSystem.Services.CourseServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;
using EntityFramework.Testing;



namespace SKSLearningSystem.Tests.Controllers.CourseControllerTests
{
    [TestClass]
    public class TakeCourseShould
    {
        [TestMethod]
        public void ReturnDefaultView_WithCorrectModel()
        {
            // Arrange
            var courseServiceMock = new Mock<ICourseService>();
            var contextMock = new Mock<LearningSystemDbContext>();
            var takeCourseModel = new TakeCourseModel();

            var courseMock = new Course() { Id=7};

            List<Course> courses = new List<Course>();
            courses.Add(courseMock);
            var courseSetMock = new Mock<DbSet<Course>>();
            courseSetMock.SetupData(courses);

            contextMock.Setup(c => c.Courses).Returns(courseSetMock.Object);
            
            var controller = new CourseController(courseServiceMock.Object, contextMock.Object);

            // Act & Assert
            controller
                .WithCallTo(c => c.TakeCourse(takeCourseModel, courseMock.Id))
                .ShouldRenderDefaultView()
                .WithModel(takeCourseModel);
        }
    }
}
