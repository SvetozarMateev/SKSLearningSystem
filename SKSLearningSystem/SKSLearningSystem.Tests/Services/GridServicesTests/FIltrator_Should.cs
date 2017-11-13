using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
     public class FIltrator_Should
    {
        [TestMethod]
        [DataRow("Test")]
        [DataRow("Test2")]
        public void FilterUsersByInputField_WhenPropertyNameIsUsersAndOpIsEq( string inputField)
        {
            //Arrange
            var dbMock = new Mock<LearningSystemDbContext>();
            var gridService = new GridServices(dbMock.Object);
           
            var user = new User() { UserName = "Test" };
            var user2 = new User() { UserName = "Test2" };
            var users = new List<User>() { user,user2};
            var op = "eq";
            var propertyName = "Username";


            //Act
            var result = gridService.Filtrator(propertyName, op, inputField, users);

            //Assert
            Assert.AreEqual(result.First().UserName, inputField);
        }

        [TestMethod]
        [DataRow("Test")]
        [DataRow("Test2")]
        public void FilterCourseStatesbyInputField_WhenPropertyNameIsCoursenameAndOpIsEq(string inputField)
        {
            //Arrange
            var dbMock = new Mock<LearningSystemDbContext>();
            var gridService = new GridServices(dbMock.Object);
            var state = new CourseState() { Course = new Course() { Name = "Test" } };
            var state2 = new CourseState() { Course = new Course() { Name = "Test2" } };
            var states = new List<CourseState>() { state, state2 };
            var user = new User() { UserName = "TestName",CourseStates=states };
            var user2 = new User() { UserName = "Test2Name" };
            var users = new List<User>() { user, user2 };
            var op = "eq";
            var propertyName = "Coursename";


            //Act
            var result = gridService.Filtrator(propertyName, op, inputField, users);

            //Assert
            Assert.AreEqual(result.First().UserName, user.UserName);
        }
    }
}
