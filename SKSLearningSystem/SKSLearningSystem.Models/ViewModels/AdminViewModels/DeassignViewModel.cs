using SKSLearningSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Models.ViewModels.AdminViewModels
{
    public class DeassignViewModel
    {
        public bool Selected { get; set; }

        public CourseState CourseState { get; set; }
    }
}
