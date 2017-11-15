using SKSLearningSystem.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Models.ViewModels.AdminViewModels
{
    public class AssignCourseToUsersViewModel
    {
        public IList<UserViewModel> Users { get; set; }
        public int CourseId { get; set; }
    }
}
