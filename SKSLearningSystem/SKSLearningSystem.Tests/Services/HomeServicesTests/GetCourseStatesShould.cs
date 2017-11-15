using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Models.ViewModels;
using SKSLearningSystem.Services;
using System.Collections.Generic;
using System.Data.Entity;

namespace SKSLearningSystem.Tests.Services.HomeServicesTests
{
    [TestClass]
    public class GetCourseStatesShould
    {
        [TestMethod]
        public void ReturnCorrectModel()
        {
            //Arrange
            var dbMock = new Mock<LearningSystemDbContext>();
            var homeServices = new HomeServices(dbMock.Object);
            var user = new User() { UserName = "1" ,Id="1"};
            var courseStateOverdue = new CourseState()
            { UserId = user.Id, State = "Overdue",Passed=true,Grade=20,Mandatory=false,Course=new Course() { Images = new List<Image> { new Image() } } };
            var courseStateCompleted = new CourseState()
            { UserId = user.Id, State = "Completed", Passed = true, Grade = 25, Mandatory = true ,Course = new Course() { Images = new List<Image> { new Image() } } };
            var courseStatePending = new CourseState()
            { UserId = user.Id, State = "Pending", Passed = false, Grade = 26, Mandatory = false ,Course = new Course() { Images = new List<Image> { new Image() } } };
            var courseStateStarted = new CourseState()
            { UserId = user.Id, State = "Started", Passed = false, Grade = 27, Mandatory = true ,Course = new Course() { Images = new List<Image> { new Image() } } };
            var listStates = new List<CourseState>()
            { courseStateCompleted, courseStateOverdue, courseStatePending, courseStateStarted };
            var dbCourseStatesMock = new Mock<DbSet<CourseState>>();
            var dbUsersMock = new Mock<DbSet<User>>();
            var listUsers = new List<User>() { user };
            dbUsersMock.SetupData(listUsers);
            dbCourseStatesMock.SetupData(listStates);
            dbMock.Setup(x => x.CourseStates).Returns(dbCourseStatesMock.Object);
            dbMock.Setup(x => x.Users).Returns(dbUsersMock.Object);

            var expected = new MyProfileViewModel();
            expected.Overdues = new List<CourseSateViewModel>() {
                new CourseSateViewModel()
                {
                    UserId = courseStateOverdue.UserId,
                    Grade = courseStateOverdue.Grade,
                    Mandatory = courseStateOverdue.Mandatory,
                    State= courseStateOverdue.State,
                    Passed= courseStateOverdue.Passed
                }
            };
            expected.Pendings = new List<CourseSateViewModel>() {
                new CourseSateViewModel()
                {
                    UserId = courseStatePending.UserId,
                    Grade = courseStatePending.Grade,
                    Mandatory = courseStatePending.Mandatory,
                    State= courseStatePending.State,
                    Passed= courseStatePending.Passed
                }
            };
            expected.Completed = new List<CourseSateViewModel>() {
                new CourseSateViewModel()
                {
                    UserId = courseStateCompleted.UserId,
                    Grade = courseStateCompleted.Grade,
                    Mandatory = courseStateCompleted.Mandatory,
                    State= courseStateCompleted.State,
                    Passed= courseStateCompleted.Passed
                }
            };
            expected.Started = new List<CourseSateViewModel>() {
                new CourseSateViewModel()
                {
                    UserId = courseStateStarted.UserId,
                    Grade = courseStateStarted.Grade,
                    Mandatory = courseStateStarted.Mandatory,
                    State= courseStateStarted.State,
                    Passed= courseStateStarted.Passed
                }
            };

            //Act
            var result = homeServices.GetCourseStates(user.UserName);

            //Assert
            Assert.AreEqual(expected.Overdues.Count, result.Overdues.Count);
            Assert.AreEqual(expected.Completed.Count, result.Completed.Count);
            Assert.AreEqual(expected.Started.Count, result.Started.Count);
            Assert.AreEqual(expected.Pendings.Count, result.Pendings.Count);

        }
    }
}
