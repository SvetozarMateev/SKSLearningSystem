using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SKSLearningSystem.Areas.Admin.Models;

namespace SKSLearningSystem.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        public AdminController()
        {
                
        }

        [HttpGet]
        public ActionResult UploadCourse()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult UploadCourse(UploadCourseViewModel model)
        {
            var supportedTypesForFile = new[] { "json" };
            var supportedTypesForPics = new[] { "png", "jpeg" };

            var fileExt = Path.GetExtension(model.CourseFile.FileName).Substring(1);

            if (!supportedTypesForFile.Contains(fileExt))
            {
                ModelState.AddModelError("file", "Invalid type. Only json is supported.");
                return View();
            }

            foreach (var item in model.Photos)
            {
                var picExt = Path.GetExtension(item.FileName).Substring(1);

                if (!supportedTypesForPics.Contains(picExt))
                {
                    ModelState.AddModelError("Picture", "Invalid type. Only png and jpeg are supported.");
                    return View();
                }
            }

            var stream = model.CourseFile.InputStream;
            //Course course;
            using (StreamReader reader = new StreamReader(stream))
            {
               var allContent= reader.ReadToEnd();
                // course = JsonConvert.DeserializeObject<Course>(allContent);
            }
                return this.View();
        }
    }
}