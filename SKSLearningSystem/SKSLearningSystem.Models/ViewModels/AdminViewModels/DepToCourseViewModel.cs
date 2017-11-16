using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Models.ViewModels.AdminViewModels
{
    public class DepToCourseViewModel
    {
        public string CourseName { get; set; }
        public string Department { get; set; }
        public DateTime DueDate { get; set; }
        public bool Mandatory { get; set; }
        public double Grade { get; set; }
    }
}
