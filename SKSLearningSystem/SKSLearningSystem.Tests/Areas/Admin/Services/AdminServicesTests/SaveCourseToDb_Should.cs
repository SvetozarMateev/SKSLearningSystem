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

namespace SKSLearningSystem.Tests.Areas.Admin.Services.AdminServicesTests
{
    [TestClass]
   public class SaveCourseToDb_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenParameterIsNull()
        {
            //Arrange
            var dbMock = new Mock<LearningSystemDbContext>();
            var services = new AdminServices(dbMock.Object);
            var coursesMock = new Mock<DbSet<Course>>();
            
            var model = new UploadCourseViewModel();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(()=> services.SaveCourseToDB(null));
        }

        [TestMethod]
        public void SaveCourse_WhenParameterIsCorrect()
        {
            //Arrange
            var dbMock = new Mock<LearningSystemDbContext>();
            var services = new AdminServices(dbMock.Object);
            var coursesMock = new Mock<DbSet<Course>>();
            var listCourses = new List<Course>();
            var courseMock = new Course();
            var model = new UploadCourseViewModel();
           
            coursesMock.SetupData(listCourses);
            dbMock.Setup(x => x.Courses).Returns(coursesMock.Object);

            //Act 
            services.SaveCourseToDB(courseMock);

            //Assert
            Assert.AreSame(courseMock, dbMock.Object.Courses.Single());
        }
    }
}
