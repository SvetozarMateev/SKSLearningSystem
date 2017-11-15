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
using SKSLearningSystem.Services.Contracts;
using Microsoft.AspNet.Identity;
using System.Collections;
using System.Collections.Generic;

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

        //Start of alternative
        [HttpGet]
        public ActionResult AssignDepToCourse()
        {
            return this.PartialView();
        }
        [HttpPost]
        public ActionResult AssignDepToCourse(string depName, string courseName)
        {
            var model = new DepToCourseViewModel() { Department = depName, CourseName = courseName };
            return this.PartialView("ConfirmDepToCourse",model);
        }

        [HttpPost]
        public ActionResult ConfirmDepToCourse(DepToCourseViewModel model)
        {
            this.dBServices.SaveAssignementsForDepartment(model);
            return RedirectToAction("AssignDepToCourse");
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
        public ActionResult AssignCourseToUsers(string courseName)
        {
            var course = this.dBServices.GetCoursesFromDBByName(courseName);
            var users = this.dBServices.GetUserViewModels();
            var model = new AssignCourseToUsersViewModel() { CourseId = course.Id, Users = users };
            return this.PartialView("Assigning",model);
        }

        public ActionResult FinalAssign(AssignCourseToUsersViewModel model)
        {
            this.dBServices.SaveAssignementsToDb(model.CourseId,model.Users.Where(x=>x.Checked).ToList());
            return this.RedirectToAction("AssignChoose");
        }



            //end of alternative
        // Assign Course Methods Start
        [HttpGet]
        public ActionResult AssignCourse()
        {
            var assignCourseViewModel = this.dBServices.GetUsersAndCoursesFromDB();

            return this.View(assignCourseViewModel);
        }

        public ActionResult ConfirmAssignment(AssignCourseViewModel assignCourseViewModel)
        {
            return View(assignCourseViewModel);
        }

        [HttpPost]
        public ActionResult SubmitAssignments(AssignCourseViewModel assignCourseViewModel)
        {
            this.adminServices.SaveAssignedCoursesToDb(assignCourseViewModel);

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

        public async Task<ActionResult> MakeAdmin(AssignCourseViewModel assignCourseViewModel)
        {
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