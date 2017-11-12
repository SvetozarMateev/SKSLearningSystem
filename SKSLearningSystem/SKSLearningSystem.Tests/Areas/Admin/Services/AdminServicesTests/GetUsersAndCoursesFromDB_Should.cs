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
    public class GetUsersAndCoursesFromDB_Should
    {
        [TestMethod]
        public void ReturnViewModelWithUserFromDb()
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
            List<UserViewModel> usersList = new List<UserViewModel>()
            {
                new UserViewModel(){Id="a", UserName="a" },
                 new UserViewModel(){Id="b", UserName="b" },
                  new UserViewModel(){Id="c", UserName="c" }
            };
            var courses = new List<Course>() { new Course() };

            dbSetUserMock.SetupData(users);
            dbSetCourseMock.SetupData(courses);

            dbMock.Setup(x => x.Users).Returns(dbSetUserMock.Object);
            dbMock.Setup(x => x.Courses).Returns(dbSetCourseMock.Object);
            var adminServicesMock = new AdminServices(dbMock.Object);
            // Act
            var result = adminServicesMock.GetUsersAndCoursesFromDB();

            // Assert
            Assert.AreEqual(usersList[0].Id, result.Users[0].Id);
            Assert.AreEqual(usersList[1].Id, result.Users[1].Id);
            Assert.AreEqual(usersList[2].Id, result.Users[2].Id);
            Assert.AreEqual(usersList[0].UserName, result.Users[0].UserName);
            Assert.AreEqual(usersList[2].UserName, result.Users[2].UserName);
            Assert.AreEqual(usersList[1].UserName, result.Users[1].UserName);
        }
    }
}
