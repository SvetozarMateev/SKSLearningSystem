using SKSLearningSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SKSLearningSystem.Data.Models;

namespace SKSLearningSystem.Areas.Admin.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public HttpPostedFileBase ProfilePictureFile { get; set; }

        public Byte[] ProfilePictureArr { get; set; }

        public string UserName { get; set; }

        public bool Checked { get; set; }

        public DateTime DueDate { get; set; }

        public bool Mandatory { get; set; }

        public List<CourseStateViewModel> CourseStates { get; set; }
    }
}