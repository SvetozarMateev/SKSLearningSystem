using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Areas.Admin.Controllers;
using SKSLearningSystem.Areas.Admin.Services;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Models.ViewModels.AdminViewModels;
using SKSLearningSystem.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace SKSLearningSystem.Tests.Web.Areas.Admin.Controllers.AdminControllerTests
{
    [TestClass]
    public class ConfirmDepToCourse_Should
    {
        [TestMethod]
        public void RedirectsToAction()
        {
            //Arrange
            var userStore = new Mock<IUserStore<User>>();
            var adminServicesMock = new Mock<IAdminServices>();
            var gridServicesMock = new Mock<IGridServices>();
            var dbServicesMock = new Mock<IDBServices>();
            var model = new DepToCourseViewModel();

            var applicationUserManagerMock = new Mock<ApplicationUserManager>(userStore.Object);

            var controller = new AdminController(adminServicesMock.Object, applicationUserManagerMock.Object, gridServicesMock.Object,
                dbServicesMock.Object);

            //Act & Assert
            controller
                .WithCallTo(x => x.ConfirmDepToCourse(model))
                .ShouldRedirectTo(x=>x.AssignChoose());
                
        }

        [TestMethod]
        public void InvokesDbServiceMethod_WhenModelIsNotNull()
        {
            //Arrange
            var userStore = new Mock<IUserStore<User>>();
            var adminServicesMock = new Mock<IAdminServices>();
            var gridServicesMock = new Mock<IGridServices>();
            var dbServicesMock = new Mock<IDBServices>();
            var model = new DepToCourseViewModel();

            var applicationUserManagerMock = new Mock<ApplicationUserManager>(userStore.Object);

            var controller = new AdminController(adminServicesMock.Object, applicationUserManagerMock.Object, gridServicesMock.Object,
                dbServicesMock.Object);

            //Act 
            controller.ConfirmDepToCourse(model);

            //Assert
            dbServicesMock.Verify(x => x.SaveAssignementsForDepartment(model), Times.Once);
        }
    }
}
