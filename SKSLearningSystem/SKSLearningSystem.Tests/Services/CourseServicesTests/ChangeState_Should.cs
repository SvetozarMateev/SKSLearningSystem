using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Services.CourseServices;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace SKSLearningSystem.Tests.Services.CourseServices.CourseServicesTests
{
    [TestClass]
    public class ChangeState_Should
    {
        [TestMethod]
        public async Task ChangeCourseState_WhenParametersAreCorrect()
        {
            //Arrange
            var dbMock = new Mock<LearningSystemDbContext>();
            var courseServices = new CourseService(dbMock.Object);
            var courseStatesMock = new Mock<DbSet<CourseState>>();
            var states = new List<CourseState>();
            var state = new CourseState() { Id = 1, State = "None" };
            var expected = "New State";
            states.Add(state);
            courseStatesMock.SetupData(states);
            dbMock.Setup(x => x.CourseStates).Returns(courseStatesMock.Object);

            //Act
            await courseServices.ChangeCourseState(state.Id, expected, 6);

            //Assert
            Assert.AreEqual(expected, state.State);
        }

        [TestMethod]
        public async Task SaveChanges_WhenParametersAreCorrect()
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
            await courseServices.ChangeCourseState(state.Id, It.IsAny<string>(), 6);
            //courseServices.ChangeCourseState(1, It.IsAny<string>());

            //Assert
            dbMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
