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

namespace SKSLearningSystem.Tests.Areas.Admin.Services.AdminServicesTests
{
    [TestClass]
    public class SaveAssignedCoursesToDb_Should
    {
        [TestMethod]
        public void CallSaveChanges()
        {
            // Arrange
            var dbMock = new Mock<LearningSystemDbContext>();
            var dbSetUserMock = new Mock<DbSet<User>>();
            var dbSetCourseMock = new Mock<DbSet<Course>>();
            var users = new List<User>() {
                new User(){Id="a", UserName="a" },
                new User(){Id="b", UserName="b" },
                new User(){Id="c", UserName="c" }
            };

            var courses = new List<Course>() { new Course() };

            dbSetUserMock.SetupData(users);
            dbSetCourseMock.SetupData(courses);

            dbMock.Setup(x => x.Users).Returns(dbSetUserMock.Object);
            dbMock.Setup(x => x.Courses).Returns(dbSetCourseMock.Object);
            var adminServicesMock = new AdminServices(dbMock.Object);

            var assignCourseViewModel = new AssignCourseViewModel()
            {
                Users = new List<UserViewModel>()
            {
                new UserViewModel(){Id="a", UserName="a" },
                 new UserViewModel(){Id="b", UserName="b" },
                  new UserViewModel(){Id="c", UserName="c" }
            },
                Courses = new List<CourseViewModel>()
                {
                    new CourseViewModel(){Id=1,Name="d"},
                    new CourseViewModel(){Id=2,Name="e"},
                    new CourseViewModel(){Id=3,Name="f"},
                }
            };

            // Act 
            adminServicesMock.SaveAssignedCoursesToDb(assignCourseViewModel);
            // Assert
            dbMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void ShouldSaveDataToDb()
        {
            // Arrange
            var dbMock = new Mock<LearningSystemDbContext>();
            var dbSetUserMock = new Mock<DbSet<User>>();
            var dbSetCourseMock = new Mock<DbSet<Course>>();
            var dbSetCourseStateMock = new Mock<DbSet<CourseState>>();

            var users = new List<User>() {
                new User(){Id="a", UserName="a" },
                new User(){Id="b", UserName="b" },
                new User(){Id="c", UserName="c" }
            };

            var courses = new List<Course>() { new Course() };

            var courseStates = new List<CourseState>();

            dbSetUserMock.SetupData(users);
            dbSetCourseMock.SetupData(courses);
            dbSetCourseStateMock.SetupData(courseStates);

            dbMock.Setup(x => x.Users).Returns(dbSetUserMock.Object);
            dbMock.Setup(x => x.Courses).Returns(dbSetCourseMock.Object);
            dbMock.Setup(x => x.CourseStates).Returns(dbSetCourseStateMock.Object);

            var adminServicesMock = new AdminServices(dbMock.Object);

            var assignCourseViewModel = new AssignCourseViewModel()
            {
                Users = new List<UserViewModel>()
            {
                new UserViewModel(){Id="a", UserName="a" },
                 new UserViewModel(){Id="b", UserName="b" },
                  new UserViewModel(){Id="c", UserName="c" }
            },
                Courses = new List<CourseViewModel>()
                {
                    new CourseViewModel(){Id=1,Name="d"},
                    new CourseViewModel(){Id=2,Name="e"},
                    new CourseViewModel(){Id=3,Name="f"},
                }
            };

            // Act
            adminServicesMock.SaveAssignedCoursesToDb(assignCourseViewModel);

            // Assert
            //dbSetCourseStateMock.Verify(x => x.Add(It.IsAny<CourseState>()), Times.Once);
            Assert.AreEqual(1, courseStates.Count);
        }
    }
}
