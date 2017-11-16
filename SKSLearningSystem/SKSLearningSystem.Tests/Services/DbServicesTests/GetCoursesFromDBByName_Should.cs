using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
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
   public class GetCoursesFromDBByName_Should
    {
        [TestMethod]
        public void ReturnCourseByGivenName()
        {
            // Arrange
            var contextMock = new Mock<LearningSystemDbContext>();
            var dbSetMock = new Mock<DbSet<Course>>();
            var course = new Course() { Name = "C#1", Id = 1 };
            var courses = new List<Course>();
            courses.Add(course);

            dbSetMock.SetupData(courses);
            contextMock.Setup(c => c.Courses).Returns(dbSetMock.Object);

            var sut = new DBServices(contextMock.Object);

            // Act
            var result = sut.GetCoursesFromDBByName("C#1");

            // Assert
            Assert.AreSame(course, result);
        }
    }
}
