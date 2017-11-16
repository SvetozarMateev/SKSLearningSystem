using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSLearningSystem.Areas.Admin.Models
{
    public class AssignCourseViewModel
    {
        public List<UserViewModel> Users { get; set; }

        public List<CourseViewModel> Courses { get; set; }

        public List<CourseSateViewModel> CourseStates { get; set; }

    }
}