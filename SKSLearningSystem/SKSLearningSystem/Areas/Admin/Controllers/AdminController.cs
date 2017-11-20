using Bytes2you.Validation;
using Microsoft.AspNet.Identity;
using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Areas.Admin.Services;
using SKSLearningSystem.Models.ViewModels.AdminViewModels;
using SKSLearningSystem.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SKSLearningSystem.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminServices adminServices;
        private readonly IGridServices gridServices;
        private readonly ApplicationUserManager userManager;
        private readonly IDBServices dBServices;       

        public AdminController(IAdminServices services, ApplicationUserManager userManager, 
            IGridServices gridServices, IDBServices dBServices)

        {
            Guard.WhenArgument(services, "services").IsNull().Throw();
            Guard.WhenArgument(userManager, "userManager").IsNull().Throw();
            Guard.WhenArgument(gridServices, "gridServices").IsNull().Throw();
            Guard.WhenArgument(dBServices, "dBServices").IsNull().Throw();

            this.adminServices = services;
            this.userManager = userManager;
            this.dBServices = dBServices;
            this.gridServices = gridServices;           
        }
       
        [HttpGet]
        public ActionResult UploadCourse()
        {                      
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadCourse(UploadCourseViewModel model)
        {
            var IsValid = this.adminServices.ValidateInputFiles(model);
            var infoModel = new UploadedCourseInfoViewModel();
            if (IsValid)
            {
                var course = this.adminServices.ReadCourseFromJSON(model.CourseFile);
                var images = this.adminServices.ReadImagesFromFiles(model.Photos);
                infoModel.CourseName = course.Name;
                infoModel.PhotosCount = images.Count;
                course.Images = images;
                this.adminServices.SaveCourseToDB(course);
            }
            else
            {
                this.ModelState.AddModelError("file", "You can upload only json, png or jpg files.");
            }

            return RedirectToAction("AlertUploadCourses");
        }

        [HttpGet]
        public ActionResult AssignDepToCourse()
        {
            return this.PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignDepToCourse(string depName, string courseName)
        {
            var model = new DepToCourseViewModel() { Department = depName, CourseName = courseName };
            return this.PartialView("ConfirmDepToCourse",model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDepToCourse(DepToCourseViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.dBServices.SaveAssignementsForDepartment(model);
            }
           
            return RedirectToAction("AssignChoose");
        }

        public ActionResult AssignChoose()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult AssignCourseToUsers()
        {
            return this.PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignCourseToUsers(string courseName)
        {
            var course = this.dBServices.GetCoursesFromDBByName(courseName);
            var users = this.dBServices.GetUserViewModels();
            var model = new AssignCourseToUsersViewModel() { CourseId = course.Id, Users = users };
            return this.PartialView("Assigning",model);
        }

        [ValidateAntiForgeryToken]
        public ActionResult FinalAssign(AssignCourseToUsersViewModel model)
        {
            this.dBServices.SaveAssignementsToDb(model.CourseId,model.Users.Where(x=>x.Checked).ToList());
            return this.RedirectToAction("AssignChoose");
        }

        [HttpGet]
        public ActionResult AssignCourse()
        {
            var assignCourseViewModel = this.dBServices.GetUsersAndCoursesFromDB();

            return this.View(assignCourseViewModel);
        }

       
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmAssignment(AssignCourseViewModel assignCourseViewModel)
        {
            return View(assignCourseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitAssignments(AssignCourseViewModel assignCourseViewModel)
        {
            this.adminServices.SaveAssignedCoursesToDb(assignCourseViewModel);

            return RedirectToAction("AlertUser");
        }

        [HttpGet]
        public ActionResult Deassign()
        {
            DeassignViewModel model = new DeassignViewModel();
            var courseStates = this.dBServices.GetAllStates();
            var listViewModels = courseStates.Select(x => new DeassignViewModel() { CourseState = x }).ToList();
            
            return this.View(listViewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deassign(List<DeassignViewModel> listViewModels)
        {
            this.adminServices.DeleteCourseStates(listViewModels);
            return RedirectToAction("Deassign");
        }

        public ActionResult AlertUser()
        {
            return View();
        }

        public ActionResult AlertUploadCourses()
        {
            return this.View();
        }      

        public ActionResult MonitorUsersProgress()
        {
            return this.View();
        }

        public  ActionResult AssignRoles()
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
                users[i].Checked =  this.userManager.IsInRole(users[i].Id, "Admin");
            }

            var assignCourseViewModel = new AssignCourseViewModel()
            {
                Users = users
            };

            return View(assignCourseViewModel);
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MakeAdmin(AssignCourseViewModel assignCourseViewModel)
        {
            if(this.ModelState.IsValid == false)
            {
                return RedirectToAction("AssignRoles");
            }
            var userIds = assignCourseViewModel.Users.Select(y => y.Id).ToArray();
            var users = this.dBServices.GetUsersFromDB(userIds).ToList();

            for (int i = 0; i < users.Count; i++)
            {
               
                if (assignCourseViewModel.Users[i].Checked==true)
                {
                    await this.userManager.AddToRoleAsync(users.Single(x => x.Id == userIds[i]).Id, "Admin");
                }
                else 
                {
                    var roles = await  this.userManager.GetRolesAsync(users.Single(x => x.Id == userIds[i]).Id);
                    await this.userManager.RemoveFromRolesAsync(users.Single(x=>x.Id==userIds[i]).Id, roles.ToArray());
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