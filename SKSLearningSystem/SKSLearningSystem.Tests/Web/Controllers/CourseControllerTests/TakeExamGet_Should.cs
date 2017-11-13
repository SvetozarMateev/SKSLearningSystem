using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Controllers;
using SKSLearningSystem.Data;
using SKSLearningSystem.Services.CourseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace SKSLearningSystem.Tests.Controllers.CourseControllerTests
{
    [TestClass]
    public class TakeExamGet_Should
    {
        [TestMethod]
        public void ReturnDefaultViewWithModel()
        {
            //Arrange
            var db = new Mock<LearningSystemDbContext>();
            var courseServicesMock = new Mock<ICourseService>();
            var controller = new CourseController(courseServicesMock.Object, db.Object);
            var courseStateId = 14;
            var model = new TakeTestViewModel() { CourseStateId = courseStateId };

            courseServicesMock.Setup(x => x.GetTestViewModel(courseStateId)).Returns(model);

            //Act & Assert
            controller
                .WithCallTo(x => x.TakeExam(courseStateId))
                .ShouldRenderDefaultView()
                .WithModel(model);
        }
    }
}
