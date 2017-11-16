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

        public List<CourseSateViewModel> Pendings { get; set; }

        public List<CourseSateViewModel> Overdues { get; set; }

        public List<CourseSateViewModel> Completed { get; set; }

        public List<CourseSateViewModel> Started { get; set; }

    }
}
