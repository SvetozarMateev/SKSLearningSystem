using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Areas.Admin.Controllers;
using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Areas.Admin.Services;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Models.ViewModels.AdminViewModels;
using SKSLearningSystem.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using TestStack.FluentMVCTesting;

namespace SKSLearningSystem.Tests.Web.Areas.Admin.Controllers.AdminControllerTests
{
    [TestClass]
   public class FinalAssign_Should
    {
        [TestMethod]
        public void RedirectToAction()
        {
            //Arrange
            var userStore = new Mock<IUserStore<User>>();
            var adminServicesMock = new Mock<IAdminServices>();
            var gridServicesMock = new Mock<IGridServices>();
            var dbServicesMock = new Mock<IDBServices>();
          
            var applicationUserManagerMock = new Mock<ApplicationUserManager>(userStore.Object);
            var usersViewModel = new List<UserViewModel>();
            var list = new List<UserViewModel>() { new UserViewModel() { Checked = true } };
            var model = new AssignCourseToUsersViewModel() { CourseId = 1,Users=list };
            
            var controller = new AdminController(adminServicesMock.Object, applicationUserManagerMock.Object, gridServicesMock.Object,
                dbServicesMock.Object);

            dbServicesMock.Setup(x => x.SaveAssignementsToDb(model.CourseId, model.Users.Where(y => y.Checked).ToList()));

            //Act & Assert
            controller
                .WithCallTo(x => x.FinalAssign(model))
                .ShouldRedirectTo(x => x.AssignChoose());
                
        }
    }
}
