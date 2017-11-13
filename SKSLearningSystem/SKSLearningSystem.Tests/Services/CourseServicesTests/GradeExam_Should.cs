using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Areas.Admin.Models;
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

namespace SKSLearningSystem.Tests.Services.CourseServices.CourseServicesTests
{
    [TestClass]
   public class GradeExam_Should
    {
        [TestMethod]
        public void ReturnGradeAsPercentage_WhenParameterIsCorrect()
        {
            //Arrange
            var dbMock = new Mock<LearningSystemDbContext>();
            var courseServices = new CourseService(dbMock.Object);
            var takeTestModel = new TakeTestViewModel();
            var question = new Question() { Answer = "A" ,Id=1};
            var question2 = new Question() { Answer = "A" ,Id=2};
            var questions = new List<Question>() { question, question2 };
            var questionsDbSetMock = new Mock<DbSet<Question>>();
            var expected = 50;
            var questionViewModel = new QuestionViewModel()
            {
                Id=1,
                Options = new List<OptionViewModel>(){ new OptionViewModel()
                {
                    IsSelected = true,
                    Letter = "A",
                    QuestionId=1
                } },
                
            };
            var questionViewModel2 = new QuestionViewModel()
            {
                Id=2,
                Options = new List<OptionViewModel>(){ new OptionViewModel()
                {
                    IsSelected = true,
                    Letter = "B",
                    QuestionId=2
                } },
            };
            var questionViewModels = new List<QuestionViewModel>() { questionViewModel, questionViewModel2 };
            takeTestModel.Questions = questionViewModels;

            questionsDbSetMock.SetupData(questions);
            dbMock.Setup(x => x.Questions).Returns(questionsDbSetMock.Object);

            //Act
            var result =courseServices.GradeExam(takeTestModel);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
