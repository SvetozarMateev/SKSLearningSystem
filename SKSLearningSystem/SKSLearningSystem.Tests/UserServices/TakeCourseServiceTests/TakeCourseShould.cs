using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EntityFramework.Testing;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SKSLearningSystem.Tests.UserServices.TakeCourseServiceTests
{
    [TestClass]
    public class TakeCourseShould
    {
        [TestMethod]
        public void AddCourseStateToUser_WhenParametersAreCorrect()
        {
            //// Arrange
            //var contextMock = new Mock<LearningSystemDbContext>();
            //var factoryMock = new Mock<ICourseStateFactory>();
            //var courseStateMock = new Mock<CourseState>();

            //string username = "Username";
            //string courseName = "CourseName";
            //int courseId = 1;

            //List<User> users = new List<User>()
            //{
            //    new User{UserName = username}
            //};

            //List<Course> courses = new List<Course>()
            //{
            //    new Course{Name = courseName}
            //};

            //List<CourseState> courseStates = new List<CourseState>()
            //{
            //    factoryMock.Object.CreateCourseState(courseId)
            //};

            //var usersSetMock = new Mock<DbSet<User>>().SetupData(users);
            //var courseSetMock = new Mock<DbSet<Course>>().SetupData(courses);
            //var courseStateSetMock = new Mock<DbSet<CourseState>>().SetupData(courseStates);

            //contextMock.Setup(c => c.Users).Returns(usersSetMock.Object);
            //contextMock.Setup(c => c.Courses).Returns(courseSetMock.Object);
            //contextMock.Setup(c => c.CourseStates).Returns(courseStateSetMock.Object);

            ////factoryMock.Setup(f => f.CreateCourseState(courseId)).Returns(courseStateMock.Object);

            //TakeCourseService sevice = new TakeCourseService(contextMock.Object);

            //// Act
            //sevice.TakeCourse(username, courseName);

            //// Assert
            //var user = contextMock.Object.Users.Single();
            //var courseState = factoryMock.Object.CreateCourseState(courseId);
            //Assert.AreEqual(courseId, courseState.CourseId);
            //contextMock.Verify(m => m.SaveChanges(), Times.Once());


        }
    }
}
