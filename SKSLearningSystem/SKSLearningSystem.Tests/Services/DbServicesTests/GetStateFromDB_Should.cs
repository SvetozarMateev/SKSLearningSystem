using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Services;
using System.Collections.Generic;
using System.Data.Entity;

namespace SKSLearningSystem.Tests.Services.DbServicesTests
{
    [TestClass]
    public class GetStateFromDB_Should
    {
        [TestMethod]
        public void ReturnCorrectStateByGivenId()
        {
            // Arrange
            var contextMock = new Mock<LearningSystemDbContext>();
            var dbSetMock = new Mock<DbSet<CourseState>>();
            var courseState = new CourseState() { Id = 1 };
            var states = new List<CourseState>();
            states.Add(courseState);

            dbSetMock.SetupData(states);
            contextMock.Setup(c => c.CourseStates).Returns(dbSetMock.Object);

            var sut = new DBServices(contextMock.Object);

            // Act
            var result = sut.GetStateFromDB(1);

            // Assert
            Assert.AreSame(courseState, result);
        }
    }
}
