using SKSLearningSystem.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Web;
using System.Web.Mvc;

namespace SKSLearningSystem.Areas.Admin.Services
{
    public class AdminServices:IAdminServices
    {
        public bool ValidateInputFiles(UploadCourseViewModel model)
        {
            if (model == null)
            {
                return false;

            }
            var supportedTypesForFile = new[] { "json" };
            var supportedTypesForPics = new[] { "png", "jpeg" };

            var fileExt = Path.GetExtension(model.CourseFile.FileName).Substring(1);

            if (!supportedTypesForFile.Contains(fileExt))
            {
                return false;
            }

            foreach (var item in model.Photos)
            {
                var picExt = Path.GetExtension(item.FileName).Substring(1);

                if (!supportedTypesForPics.Contains(picExt))
                {
                    return false;
                }
            }
            return true;
        }

        //public Course ReadCourseFromJSON(UploadCourseViewModel model)
        //{
        //    var stream = model.CourseFile.InputStream;
        //    //Course course;
        //    using (StreamReader reader = new StreamReader(stream))
        //    {
        //        var allContent = reader.ReadToEnd();
        //        // course = JsonConvert.DeserializeObject<Course>(allContent);
        //    }
        //    return course;
        //}
    }
}