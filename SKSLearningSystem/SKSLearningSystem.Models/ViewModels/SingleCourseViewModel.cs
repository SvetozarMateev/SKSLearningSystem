using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Models.ViewModels
{
    public class SingleCourseViewModel
    {
        public int CourseStateId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Descrtiption { get; set; }
        public int CourseImageId { get; set; }
    }
}
