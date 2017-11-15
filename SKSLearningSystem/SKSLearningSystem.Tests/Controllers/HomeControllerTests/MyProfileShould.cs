using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Controllers;
using SKSLearningSystem.Models.ViewModels;
using SKSLearningSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace SKSLearningSystem.Tests.Controllers.HomeControllerTests
{
    [TestClass]
    public class MyProfileShould
    {
        [TestMethod]
        public void ReturnDefaultView_WithCorrectModel()
        {
            // Arrange
            var homeServiceMock = new Mock<IHomeServices>();
            var myProfileViewModel = new MyProfileViewModel();

           //homeServiceMock.Setup(c => c.GetCourseStates()).Returns(myProfileViewModel);

            //var controller = new HomeController(homeServiceMock.Object);

            
            // Act & Assert
            //controller.WithCallTo(c => c.MyProfile()).ShouldRenderDefaultView();
        }
    }
}
