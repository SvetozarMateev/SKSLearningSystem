using Bytes2you.Validation;
using SKSLearningSystem.Data;
using SKSLearningSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SKSLearningSystem.Data.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace SKSLearningSystem.Services
{
    public class MyProfileService
    {
        private readonly LearningSystemDbContext db;
        private readonly ApplicationUserManager applicationUserManager;

        public MyProfileService(LearningSystemDbContext db, ApplicationUserManager applicationUserManager)
        {
            Guard.WhenArgument(db, "db").IsNull().Throw();

            this.db = db;
            this.applicationUserManager = applicationUserManager;
        }

        public  MyProfileViewModel GetCourseStates(MyProfileViewModel myProfileViewModel)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();

            return myProfileViewModel;
        }

        public void ReadImagesFromFiles(HttpPostedFileBase file)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            Guard.WhenArgument(file, "file").IsNull().Throw();

            Image image = new Image();
            
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                    image =new Image() { CurrentImage = array };
                }

            image.UserId = userId;

            db.Images.Add(image);
            db.SaveChanges();
        }



    }
}