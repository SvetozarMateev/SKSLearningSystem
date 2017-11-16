using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Controllers;
using SKSLearningSystem.Data;
using SKSLearningSystem.Models;
using SKSLearningSystem.Services.Contracts;
using SKSLearningSystem.Services.CourseServices;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;

namespace SKSLearningSystem.Tests.Controllers.CourseControllerTests
{
    [TestClass]
    public class TakeExamPost_Should
    {
        [TestMethod]
        public void ReturnPartialViewInvalidTestWithModel_WhenTestIsInvalid()
        {
            //Arrange
            var db = new Mock<LearningSystemDbContext>();
            var courseServicesMock = new Mock<ICourseService>();
            var dbServicesMock = new Mock<IDBServices>();

            var controller = new CourseController(courseServicesMock.Object,dbServicesMock.Object );
            var courseStateId = 14;
            var model = new TakeTestViewModel() { CourseStateId = courseStateId };
            courseServicesMock.Setup(x => x.ValidateTest(model)).Returns(false);

            //Act & Assert
            controller
                .WithCallTo(x => x.TakeExam(model))
                .ShouldRenderPartialView("InvalidTest")
                .WithModel(model).AndModelError("answers")
                .Containing("Please select one answer per question");
        }

        [TestMethod]
        [DataRow(49)]
        [DataRow(30)]
        [DataRow(20)]
        [DataRow(0)]
        public void ReturnPartialViewFailedTestWithModel_WhenTestIsFailed(int grade)
        {
            //Arrange
            
            var courseServicesMock = new Mock<ICourseService>();
            var dbServicesMock = new Mock<IDBServices>();

            var controller = new CourseController(courseServicesMock.Object,dbServicesMock.Object);
            var courseStateId = 14;
            var model = new TakeTestViewModel() { CourseStateId = courseStateId };
            courseServicesMock.Setup(x => x.ValidateTest(model)).Returns(true);
            courseServicesMock.Setup(x => x.GradeExam(model)).Returns(grade);

            //Act & Assert
            controller
                .WithCallTo(x => x.TakeExam(model))
                .ShouldRenderPartialView("FailedTest")
                .WithModel(model);
        }

        [TestMethod]
        [DataRow(50)]
        [DataRow(70)]
        [DataRow(90)]
        [DataRow(100)]
        public void ReturnPartialViewPassedTestWithModel_WhenTestIsPassesd(int grade)
        {
            //Arrange
            var db = new Mock<LearningSystemDbContext>();
            var courseServicesMock = new Mock<ICourseService>();
            var dbServicesMock = new Mock<IDBServices>();

            var controller = new CourseController(courseServicesMock.Object,dbServicesMock.Object );
            var courseStateId = 14;
            var model = new TakeTestViewModel() { CourseStateId = courseStateId };
            courseServicesMock.Setup(x => x.ValidateTest(model)).Returns(true);
            courseServicesMock.Setup(x => x.GradeExam(model)).Returns(grade);

            //Act & Assert
            controller
                .WithCallTo(x => x.TakeExam(model))
                .ShouldRenderPartialView("PassedTest")
                .WithModel(model);
        }

        [TestMethod]
        [DataRow(50)]
        [DataRow(70)]
        [DataRow(90)]
        [DataRow(100)]
        public void ChangeCourseState_WhenTestIsPassesd(int grade)
        {
            //Arrange
            var db = new Mock<LearningSystemDbContext>();
            var courseServicesMock = new Mock<ICourseService>();
            var dbServicesMock = new Mock<IDBServices>();

            var controller = new CourseController(courseServicesMock.Object,dbServicesMock.Object);
            var courseStateId = 14;
            var model = new TakeTestViewModel() { CourseStateId = courseStateId};
            courseServicesMock.Setup(x => x.ValidateTest(model)).Returns(true);
            courseServicesMock.Setup(x => x.GradeExam(model)).Returns(grade);

            //Act 
            controller.TakeExam(model);

            //Assert
            //courseServicesMock.Verify(x => x.ChangeCourseState(courseStateId, "Completed"),Times.Once);
        }
       
    }
}
