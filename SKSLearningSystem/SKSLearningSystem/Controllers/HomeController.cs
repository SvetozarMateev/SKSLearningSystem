using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Areas.Admin.Services;
using SKSLearningSystem.Data;
using SKSLearningSystem.Models;
using System;
using SKSLearningSystem.Data.Models;

using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SKSLearningSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdminServices services;
        private readonly ApplicationUserManager applicationUserManager;
        private readonly LearningSystemDbContext context;

        public HomeController(ApplicationUserManager applicationUserManager, LearningSystemDbContext context,
            IAdminServices services)
        {
            this.applicationUserManager = applicationUserManager;
            this.context = context;
            this.services = services;
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
                ProfilePictureArr = user.ProfilePicture.CurrentImage,
                UserName = user.UserName,
                CourseStates = user.CourseStates.Select(cs => new CourseStateViewModel()
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

        [HttpPost]
        public async Task<ActionResult> ChangeProfilePicture(UserViewModel model)
        {
            var user = await this.applicationUserManager.FindByNameAsync(this.User.Identity.Name);


            using (MemoryStream ms = new MemoryStream())
            {
                model.ProfilePictureFile.InputStream.CopyTo(ms);
                byte[] array = ms.GetBuffer();
                user.ProfilePicture = new Image() { CurrentImage = array };
            }

            context.SaveChanges();

            return RedirectToAction("MyProfile");
        }

        public async Task<ActionResult> RenderImage(UserViewModel userViewModel)
        {
            Image image = new Image();
            image.CurrentImage = userViewModel.ProfilePictureArr;
            byte[] currentImage = image.CurrentImage;
            return File(currentImage, "image/png");
        }





    }
}