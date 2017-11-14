using SKSLearningSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Models.ViewModels
{
   public class MyProfileViewModel
    {
        public Image ProfilePicture { get; set; }

        public List<CourseSateViewModel> CourseStates { get; set; }

    }
}
