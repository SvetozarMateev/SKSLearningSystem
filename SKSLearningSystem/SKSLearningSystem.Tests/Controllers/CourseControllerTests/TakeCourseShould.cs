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
using SKSLearningSystem.Services.Contracts;

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
            var dbSevrviceMock = new Mock<IDBServices>();

            var courseId = 0;
            var courseStateId = 1;
            var courseName = "DSA";
            var images = new List<Image>();

            var course = new Course()
            {
                Id = courseId,
                Name = courseName,
                Images = images
            };

            dbSevrviceMock.Setup(d => d.GetCoursesFromDB(courseId)).Returns(course);
            dbSevrviceMock.Setup(d => d.GetImages(courseId)).Returns(images);

            var model = new TakeCourseModel();
            model.CourseStateId = courseStateId;
            model.CourseName = courseName;
            model.Images = images;

            var controller = new CourseController(courseServiceMock.Object, dbSevrviceMock.Object);

            // Act & Assert
            controller
                .WithCallTo(c => c.TakeCourse(courseStateId, courseId))
                .ShouldRenderDefaultView()
                .WithModel<TakeCourseModel>();

        }
    }
}
