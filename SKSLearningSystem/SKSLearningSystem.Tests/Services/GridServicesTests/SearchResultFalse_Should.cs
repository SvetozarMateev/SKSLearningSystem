using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Areas.Admin.Services;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Tests.Services.GridServicesTests
{
    [TestClass]
    public class SearchResultFalse_Should
    {
        [TestMethod]
        public void ReturnObjectWithFilledProperties()
        {
            //Arrange
            var dbMock = new Mock<LearningSystemDbContext>();
            var gridServices = new GridServices(dbMock.Object);
            var user = new User() { UserName = "das", Id = "1" };
            var page = 1;
            var rows = 30;
            var userDbSetMock = new Mock<DbSet<User>>();
            var course = new Course() { Id = 1, Name = "das" };
            var courseState = new CourseState() { UserId = "1", User = user, Grade = 0, State = "some", Course = course, CourseId = 1 };
            user.CourseStates = new List<CourseState>() { courseState };
            var modelExpected = new CourseStateRowViewModel() { AssignementDate = courseState.AssignmentDate, State = courseState.State, Coursename = course.Name, DueDate = courseState.DueDate, Username = user.UserName, Index = 1 };
            var userList = new List<User>() { user };
            userDbSetMock.SetupData(userList);
            dbMock.Setup(x => x.Users).Returns(userDbSetMock.Object);
            var expected = new { total = 1, page = 1, records = userList.Count, rows = new List<CourseStateRowViewModel>() { modelExpected } };
            //Act
            var result = gridServices.SearchFalseResult(page,rows);

            //Assert
            Assert.AreEqual(expected.ToString(), result.ToString());
        }
    }
}
