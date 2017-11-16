using Microsoft.AspNet.Identity;
using SKSLearningSystem.Areas.Admin.Services;
using SKSLearningSystem.Models.ViewModels;
using SKSLearningSystem.Services;
using SKSLearningSystem.Services.Contracts;
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
        private readonly IAdminServices adminServices;
        private readonly IDBServices dBServices;
       // public Func<string> GetUserId; //For testing

        public HomeController(IHomeServices homeServices, IAdminServices adminServices,
            IDBServices dBServices)
        {
            this.homeServices = homeServices;
            this.adminServices = adminServices;
            this.dBServices = dBServices;
            //GetUserId = () => HttpContext.User.Identity.GetUserId();
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

        public ActionResult AllCourses()
        {
            var courses = this.dBServices.GetCoursesFromDb();
            return View(courses);
        }

        [HttpGet]
        public ActionResult MyProfile()
        {
            var userId = HttpContext.User.Identity.Name;
            var myProfileViewModel =  this.homeServices.GetCourseStates(userId);
            return View(myProfileViewModel);
        }

        [HttpPost]
        public ActionResult MyProfile(MyProfileViewModel myProfileViewModel,HttpPostedFileBase image)
        {
            var userId = HttpContext.User.Identity.Name;
            var singleImage = this.adminServices.ReadImagesFromFiles(new List<HttpPostedFileBase>() { image }).Single();
            this.homeServices.SaveImagesToUser(singleImage, userId);
            return View(myProfileViewModel);
        }
    }
}