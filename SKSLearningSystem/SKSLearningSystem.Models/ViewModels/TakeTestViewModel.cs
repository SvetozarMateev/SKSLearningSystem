using SKSLearningSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSLearningSystem.Areas.Admin.Models
{
    public class TakeTestViewModel
    {
        public List<QuestionViewModel> Questions { get; set; }
        public string CourseName { get; set; }
        public int CourseStateId { get; set; }
        public double Grade { get; set; }
    }
}