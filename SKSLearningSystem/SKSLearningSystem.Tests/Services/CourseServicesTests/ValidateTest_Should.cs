using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Data;
using SKSLearningSystem.Models;
using SKSLearningSystem.Services.CourseServices;
using System.Collections.Generic;

namespace SKSLearningSystem.Tests.Services.CourseServices.CourseServicesTests
{
    [TestClass]
    public class ValidateTest_Should
    {
        [TestMethod]
        public void ReturnFalse_WhenAQuestionHasMoreThanOneAnswer()
        {
            //Arrange
            var dbMock = new Mock<LearningSystemDbContext>();
            var courseServices = new CourseService(dbMock.Object);
            var option = new OptionViewModel() {QuestionId=1,IsSelected=true };
            var option2 = new OptionViewModel() { QuestionId = 1, IsSelected = true };
            var questions = new QuestionViewModel() { Options = new List<OptionViewModel>() { option, option2 } };
            var model = new TakeTestViewModel() { Questions=new List<QuestionViewModel>() { questions } };
            var expected = false;
            //Act
            var result = courseServices.ValidateTest(model);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ReturnTrue_WhenAQuestionHasOneAnswer()
        {
            //Arrange
            var dbMock = new Mock<LearningSystemDbContext>();
            var courseServices = new CourseService(dbMock.Object);
            var option = new OptionViewModel() { QuestionId = 1, IsSelected = true };
           
            var questions = new QuestionViewModel() { Options = new List<OptionViewModel>() { option } };
            var model = new TakeTestViewModel() { Questions = new List<QuestionViewModel>() { questions } };
            var expected = true;
            //Act
            var result = courseServices.ValidateTest(model);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
