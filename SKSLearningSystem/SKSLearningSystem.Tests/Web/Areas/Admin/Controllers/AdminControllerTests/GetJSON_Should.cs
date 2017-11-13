using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SKSLearningSystem.Areas.Admin.Controllers;
using SKSLearningSystem.Areas.Admin.Services;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;

namespace SKSLearningSystem.Tests.Areas.Admin.Controllers.AdminControllerTests
{
    [TestClass]
    public class GetJSON_Should
    {
        [TestMethod]
        public void ReturnJSONFromGridServicesWhenFalseMethod_WhenSearchIsFalse()
        {
            //Arrange
            var userStore = new Mock<IUserStore<User>>();
            var adminServicesMock = new Mock<IAdminServices>();
            var gridServicesMock = new Mock<IGridServices>();
            var dbMock = new Mock<LearningSystemDbContext>();
            var applicationUserManagerMock = new Mock<ApplicationUserManager>(userStore.Object);
            var controller = new AdminController(adminServicesMock.Object,
                applicationUserManagerMock.Object, dbMock.Object, gridServicesMock.Object);
            var _search = true;
            var rows = It.IsAny<int>();
            var pages = It.IsAny<int>();
            var filters = It.IsAny<string>();
            //Act & Assert
            controller
                .WithCallTo(c => c.GetJSON(_search, rows, pages, filters))
                .ShouldReturnJson(x => gridServicesMock.Object.SearchFalseResult())
                .JsonRequestBehavior.HasFlag(JsonRequestBehavior.AllowGet);
                
        }

        [TestMethod]
        public void ReturnJSONFromGridServicesWhenTrueMethod_WhenSearchIsTrue()
        {
            //Arrange
            var userStore = new Mock<IUserStore<User>>();
            var adminServicesMock = new Mock<IAdminServices>();
            var gridServicesMock = new Mock<IGridServices>();
            var dbMock = new Mock<LearningSystemDbContext>();
            var applicationUserManagerMock = new Mock<ApplicationUserManager>(userStore.Object);
            var controller = new AdminController(adminServicesMock.Object, applicationUserManagerMock.Object, dbMock.Object, gridServicesMock.Object);
            var _search = true;
            var rows = It.IsAny<int>();
            var pages = It.IsAny<int>();
            var filters = It.IsAny<string>();
            //Act & Assert
            controller
                .WithCallTo(c => c.GetJSON(_search, rows,pages, filters))
                .ShouldReturnJson(x => gridServicesMock.Object.SearchResultTrue(filters))
                .JsonRequestBehavior.HasFlag(JsonRequestBehavior.AllowGet);
        }
    }
}
