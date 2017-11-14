using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Areas.Admin.Services;
using SKSLearningSystem.Data;
using System.Linq;
using SKSLearningSystem;
using System.Web.Mvc;
using System.Threading.Tasks;
using SKSLearningSystem.Data.Models;
using Bytes2you.Validation;
using SKSLearningSystem.Models.ViewModels.AdminViewModels;

namespace SKSLearningSystem.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminServices services;
        private readonly IGridServices gridServices;
        private readonly ApplicationUserManager userManager;
        private readonly LearningSystemDbContext context;

        public AdminController(IAdminServices services, ApplicationUserManager userManager, LearningSystemDbContext context
            , IGridServices gridServices)

        {          
            this.services = services;
            this.userManager = userManager;
            this.context = context;
            this.gridServices = gridServices;
            this.userManager = userManager;
        }     

        [HttpGet]
        public ActionResult UploadCourse()
        {
            return this.View();
        }

        // Assign Course Methods Start
        [HttpGet]
        public ActionResult AssignCourse()
        {
            var assignCourseViewModel = this.services.GetUsersAndCoursesFromDB();

            return this.View(assignCourseViewModel);
        }

        public ActionResult ConfirmAssignment(AssignCourseViewModel assignCourseViewModel)
        {
            return View(assignCourseViewModel);
        }

        [HttpPost]
        public ActionResult SubmitAssignments(AssignCourseViewModel assignCourseViewModel)
        {
            this.services.SaveAssignedCoursesToDb(assignCourseViewModel);

            return RedirectToAction("AlertUser");
        }

        public ActionResult AlertUser()
        {
            return View();
        }
        // end

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadCourse(UploadCourseViewModel model)
        {
            var IsValid = this.services.ValidateInputFiles(model);
            var infoModel = new UploadedCourseInfoViewModel();
            if (IsValid)
            {
                var course = this.services.ReadCourseFromJSON(model.CourseFile);
                var images = this.services.ReadImagesFromFiles(model.Photos);
                infoModel.CourseName = course.Name;
                infoModel.PhotosCount = images.Count;
                course.Images = images;
                this.services.SaveCourseToDB(course);
            }
            else
            {
                this.ModelState.AddModelError("file", "You can upload only json, png or jpg files.");
            }
            return RedirectToAction("AlertUploadCourses");
        }

        public ActionResult AlertUploadCourses()
        {
            return this.View();
        }


        public ActionResult MonitorUsersProgress()
        {
            return this.View();
        }

        public async Task<ActionResult> AssignRoles()
        {
            var users = this.userManager
                .Users
                .Select( u => new UserViewModel()
                {
                    UserName = u.UserName,
                    Id = u.Id,
                }).ToList();

            for (int i = 0; i < users.Count; i++)
            {
                users[i].Checked = await this.userManager.IsInRoleAsync(users[i].Id, "Admin");
            }

            var assignCourseViewModel = new AssignCourseViewModel()
            {
                Users = users
            };

            return View(assignCourseViewModel);
        }

        public async Task<ActionResult> MakeAdmin(AssignCourseViewModel assignCourseViewModel)
        {
            var userIds = assignCourseViewModel.Users.Select(y => y.Id).ToArray();
            var users = context.Users.Where(x => userIds.Contains(x.Id)).ToList();

            for (int i = 0; i < users.Count; i++)
            {
                if (assignCourseViewModel.Users[i].Checked==true)
                {
                    await this.userManager.AddToRoleAsync(users[i].Id, "Admin");
                }
                else
                {
                    var roles = await this.userManager.GetRolesAsync(users[i].Id);
                    await this.userManager.RemoveFromRolesAsync(users[i].Id, roles.ToArray());
                }
                
            }

            return RedirectToAction("AssignRoles");
        }

            public ActionResult GetJSON(bool _search, int rows, int page, string filters)
            {
                if (_search == false)
                {
                    return Json(this.gridServices.SearchFalseResult(page, rows), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(this.gridServices.SearchResultTrue(filters), JsonRequestBehavior.AllowGet);
                }

            }
        }
    }