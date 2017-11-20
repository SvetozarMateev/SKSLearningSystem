using Bytes2you.Validation;
using SKSLearningSystem.Areas.Admin.Services;
using SKSLearningSystem.Models.ViewModels;
using SKSLearningSystem.Services;
using SKSLearningSystem.Services.Contracts;
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
      

        public HomeController(IHomeServices homeServices, IAdminServices adminServices,
            IDBServices dBServices)
        {

            Guard.WhenArgument(homeServices, "homeServices").IsNull().Throw();
            Guard.WhenArgument(adminServices, "adminServices").IsNull().Throw();
            Guard.WhenArgument(dBServices, "dBServices").IsNull().Throw();

            this.homeServices = homeServices;
            this.adminServices = adminServices;
            this.dBServices = dBServices;
            
        }

        [ChildActionOnly]
        [OutputCache(Duration = 180)]
        public ActionResult GetCoursesPartial()
        {
            var courses = this.dBServices.GetCoursesFromDb();

            return this.PartialView("_CoursesPartial", courses);
        }

        public ActionResult Index()
        {           
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