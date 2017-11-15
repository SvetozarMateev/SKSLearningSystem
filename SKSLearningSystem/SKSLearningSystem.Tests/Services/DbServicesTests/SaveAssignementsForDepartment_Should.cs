using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Models.ViewModels.AdminViewModels;
using SKSLearningSystem.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SKSLearningSystem.Tests.Services.DbServicesTests
{
    [TestClass]
    public class SaveAssignementsForDepartment_Should
    {
        [TestMethod]
        public void CallSaveChangesAsyncOnce()
        {
            // Arrange
            var contextMock = new Mock<LearningSystemDbContext>();
            var dbUsersSetMock = new Mock<DbSet<User>>();
            var dbCoursesSetMock = new Mock<DbSet<Course>>();
            var dbCourseStatesSetMock = new Mock<DbSet<CourseState>>();

            var courses = new List<Course>() { new Course() { Name = "C#1", Id = 1 } };
            var users = new List<User>() { new User() { UserName = "Pesho", Id = "1" } };
            var states = new List<CourseState>() { new CourseState() { Id = 2 } ,
                new CourseState() { Id = 1, UserId="1",CourseId=1} };

            dbCoursesSetMock.SetupData(courses);
            dbUsersSetMock.SetupData(users);
            dbCourseStatesSetMock.SetupData(states);

            contextMock.Setup(c => c.Courses).Returns(dbCoursesSetMock.Object);
            contextMock.Setup(u => u.Users).Returns(dbUsersSetMock.Object);
            contextMock.Setup(c => c.CourseStates).Returns(dbCourseStatesSetMock.Object);

            var DBServices = new DBServices(contextMock.Object);

            DepToCourseViewModel model = new DepToCourseViewModel() { CourseName = "C#1" };
            // Act
            var result = DBServices.SaveAssignementsForDepartment(model);

            // Assert
            contextMock.Verify(m => m.SaveChangesAsync(), Times.Once());

            
        }
    }
}
