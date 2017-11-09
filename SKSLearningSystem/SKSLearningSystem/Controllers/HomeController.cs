using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Data;
using SKSLearningSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SKSLearningSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationUserManager applicationUserManager;
        private readonly LearningSystemDbContext context;

        public HomeController(ApplicationUserManager applicationUserManager, LearningSystemDbContext context)
        {
            this.applicationUserManager = applicationUserManager;
            this.context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public async Task<ActionResult> MyProfile()
        {
            var user = await this.applicationUserManager.FindByNameAsync(this.User.Identity.Name);

            var userModel = new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                CourseStates = user.CourseStates.Select(cs=>new CourseStateViewModel()
                {
                    CourseName = cs.Course.Name,
                    Mandatory = cs.Mandatory,
                    Grade = cs.Grade,
                    AssignmentDate = cs.AssignmentDate,
                    DueDate = cs.DueDate,
                    CompletionDate = cs.DueDate
                }).ToList()
        };

            return View(userModel);
        }


    }
}