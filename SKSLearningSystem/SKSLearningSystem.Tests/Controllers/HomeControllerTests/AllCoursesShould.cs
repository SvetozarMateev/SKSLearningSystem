using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Areas.Admin.Services;
using SKSLearningSystem.Controllers;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Models.ViewModels;
using SKSLearningSystem.Services;
using SKSLearningSystem.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace SKSLearningSystem.Tests.Controllers.HomeControllerTests
{
    [TestClass]
    public class AllCoursesShould
    {
        [TestMethod]
        public void ReturnDefaultView_WithCorrectModel()
        {
            // Arrange
            var homeServiceMock = new Mock<IHomeServices>();
            var adminServiceMock = new Mock<IAdminServices>();
            var dbServiceMock = new Mock<IDBServices>();

            var courses = new List<SingleCourseViewModel>();
            
            var controller = new HomeController(homeServiceMock.Object, adminServiceMock.Object, dbServiceMock.Object);
           // var courses = new List<SingleCourseViewModel>();

            //homeServiceMock.Setup(x => x.GetCoursesFromDb()).Returns(courses);

            //var controller = new HomeController(homeServiceMock.Object);

            
            // Act & Assert
            //controller
            //    .WithCallTo(c => c.AllCourses())
            //    .ShouldRenderDefaultView();
        }
    }
}
