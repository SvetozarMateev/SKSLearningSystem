using SKSLearningSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSLearningSystem.Models
{
    public class CourseStateViewModel
    {
        public string CourseName { get; set; }

        public bool Mandatory { get; set; }

        public bool Passed { get; set; }

        public double Grade { get; set; }

        public DateTime AssignmentDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CompletionDate { get; set; }
    }
}