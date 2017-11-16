using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Services.CourseServices;
using System.Collections.Generic;
using System.Data.Entity;

namespace SKSLearningSystem.Tests.Services.CourseServices.CourseServicesTests
{
    [TestClass]
    public class ChangeState_Should
    {
        //[TestMethod]
        //public async void ChangeCourseState_WhenParametersAreCorrect()
        //{
        //    //Arrange
        //    var dbMock = new Mock<LearningSystemDbContext>();
        //    var courseServices = new CourseService(dbMock.Object);
        //    var courseStatesMock = new Mock<DbSet<CourseState>>();
        //    var states = new List<CourseState>();
        //    var state = new CourseState() { Id = 1, State = "None" };
        //    var expected = "New State";
            
        //    states.Add(state);
        //    courseStatesMock.SetupData(states);
        //    dbMock.Setup(x => x.CourseStates).Returns(courseStatesMock.Object);

        //    //Act
        //    await courseServices.ChangeCourseState(1, expected,It.IsAny<double>());

        //    //Assert
        //    Assert.AreEqual(expected, state.State);
        //}

        [TestMethod]
        public  void ChangeGrade_WhenParametersAreCorrect()
        {
            //Arrange
            var dbMock = new Mock<LearningSystemDbContext>();
            var courseServices = new CourseService(dbMock.Object);
            var courseStatesMock = new Mock<DbSet<CourseState>>();
            var states = new List<CourseState>();
            var state = new CourseState() { Id = 1, State = "None" };
            var expected = 70;

            states.Add(state);
            courseStatesMock.SetupData(states);
            dbMock.Setup(x => x.CourseStates).Returns(courseStatesMock.Object);

            //Act
             courseServices.ChangeCourseState(1, It.IsNotNull<string>(), expected);

            //Assert
            Assert.AreEqual(expected, state.Grade);
        }

        [TestMethod]
        public  void SaveChanges_WhenParametersAreCorrect()
        {
            //Arrange
            var dbMock = new Mock<LearningSystemDbContext>();
            var courseServices = new CourseService(dbMock.Object);
            var courseStatesMock = new Mock<DbSet<CourseState>>();
            var states = new List<CourseState>();
            var state = new CourseState() { Id = 1, State = "None" };           
            states.Add(state);
            courseStatesMock.SetupData(states);
            dbMock.Setup(x => x.CourseStates).Returns(courseStatesMock.Object);

            //Act
            courseServices.ChangeCourseState(1, It.IsNotNull<string>(),It.IsAny<double>());

            //Assert
            dbMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
