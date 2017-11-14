using SKSLearningSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SKSLearningSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeServices homeServices;

        public HomeController(IHomeServices homeServices)
        {
            this.homeServices = homeServices;
        }
        public ActionResult Index()
        {
            var courses = this.homeServices.GetCoursesFromDb();
            return View(courses);
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

        public ActionResult AllCourses()
        {
            var courses = this.homeServices.GetCoursesFromDb();
            return View(courses);
        }
    }
}