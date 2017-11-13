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
    public class GetTestViewModel_Should
    {
        [TestMethod]
        public void ReturnFullTestViewModel_WhenParametersAreCorrect()
        {
            //Arrange
            var dbMock = new Mock<LearningSystemDbContext>();
            var courseServices = new CourseService(dbMock.Object);
            var courseMock = new Course() { Id = 1,Name="some name" };
            var questionMock = new Question() { Id = 1,Statement="some statement" };
            var optionMock = new Option() { Id = 1 };         
            questionMock.CourseId = 1;        
            optionMock.QuestionId = 1;
            optionMock.Letter = "A";
            optionMock.Answer = "some answer";
            questionMock.Options = new List<Option>() { optionMock };
            courseMock.Questions = new List<Question>() { questionMock };
            var optionsAsViewModels = new List<OptionViewModel>() { new Models.OptionViewModel()
            {
                Answer=optionMock.Answer,
                Id=1,
                IsSelected=false,
                Letter=optionMock.Letter,
                QuestionId=questionMock.Id
            } };
            var questionsAsViewModels = new List<QuestionViewModel>()
            {
                new QuestionViewModel
                {
                    CourseId = questionMock.Id,
                    Id = 1,
                    Options = optionsAsViewModels,
                    Statement = questionMock.Statement
                }
            };
            var courseStatesMock = new Mock<DbSet<CourseState>>();
            var states = new List<CourseState>();
            var state = new CourseState() { Id = 1, State = "None" ,Course=courseMock,CourseId=1};
            var expected = new TakeTestViewModel()
            {
                CourseName = courseMock.Name,
                CourseStateId = state.Id,
                Grade = It.IsAny<Double>(),
                Questions = questionsAsViewModels
            };
            states.Add(state);
            courseStatesMock.SetupData(states);
            dbMock.Setup(x => x.CourseStates).Returns(courseStatesMock.Object);
            //Act
           var result= courseServices.GetTestViewModel(1);

            //Assert
            Assert.AreEqual(expected.CourseName, result.CourseName);
            Assert.AreEqual(expected.CourseStateId, result.CourseStateId);
            Assert.AreEqual(expected.Grade, result.Grade);
            Assert.AreEqual(expected.Questions.First().Statement, result.Questions.First().Statement);
            Assert.AreEqual(expected.Questions.First().Id, result.Questions.First().Id);
            Assert.AreEqual(expected.Questions.First().CourseId, result.Questions.First().CourseId);
            Assert.AreEqual(expected.Questions.First().Options.First().Id, result.Questions.First().Options.First().Id);
            Assert.AreEqual(expected.Questions.First().Options.First().IsSelected, result.Questions.First().Options.First().IsSelected);
            Assert.AreEqual(expected.Questions.First().Options.First().Letter, result.Questions.First().Options.First().Letter);
            Assert.AreEqual(expected.Questions.First().Options.First().QuestionId, result.Questions.First().Options.First().QuestionId);
            Assert.AreEqual(expected.Questions.First().Options.First().Answer, result.Questions.First().Options.First().Answer);

        }
    }
}
