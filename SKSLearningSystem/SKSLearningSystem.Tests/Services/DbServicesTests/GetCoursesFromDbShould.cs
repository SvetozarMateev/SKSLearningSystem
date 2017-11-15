using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Models.ViewModels;
using SKSLearningSystem.Services;
using SKSLearningSystem.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Tests.Services.HomeServicesTests
{
    [TestClass]
    public class GetCoursesFRomDbShould
    {
        //[TestMethod]
        //public void ReturnCourses_WhenParametersAreCorrect()
        //{
        //    // Arrange
        //    var contextMock = new Mock<LearningSystemDbContext>();

        //    var courses = new List<Course>()
        //    {
        //        new Course(){ Name="C#1" },
        //        new Course() {Name = "C#2"},
        //        new Course(){ Name="OOP" },
        //        new Course() {Name = "DSA"},
        //        new Course(){ Name="HQC" },
        //        new Course() {Name = "FE"},
        //        new Course(){ Name="DB" },
        //        new Course() {Name = "DI"},
        //        new Course(){ Name="UT" },
        //        new Course() {Name = "MVC"},
        //    };

        //    var courseViewModels = new List<SingleCourseViewModel>()
        //    {
        //        new SingleCourseViewModel(){ CourseName="C#1" },
        //        new SingleCourseViewModel() {CourseName = "C#2"},
        //        new SingleCourseViewModel(){ CourseName="OOP" },
        //        new SingleCourseViewModel() {CourseName = "DSA"},
        //        new SingleCourseViewModel(){ CourseName="HQC" },
        //        new SingleCourseViewModel() {CourseName = "FE"},
        //        new SingleCourseViewModel(){ CourseName="DB" },
        //        new SingleCourseViewModel() {CourseName = "DI"},
        //        new SingleCourseViewModel(){ CourseName="UT" },
        //        new SingleCourseViewModel() {CourseName = "MVC"},
        //    };

        //    var courseSetMock = new Mock<DbSet<Course>>();

        //    courseSetMock.SetupData(courses);

        //    contextMock.Setup(c => c.Courses).Returns(courseSetMock.Object);
        //    var dbService = new DBServices(contextMock.Object);

        //    // Act
        //    var result = dbService.GetCoursesFromDb();

        //    //Assert

        //    CollectionAssert.AreEquivalent(courses, result);
        //}

        [TestMethod]
        public void ReturnCourseById()
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
            var result = sut.GetCoursesFromDB(1);

            // Assert
            Assert.AreSame(course, result);
        }
    }
}
