using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSLearningSystem.Areas.Admin.Models
{
    public class CourseStateRowViewModel
    {
        public int Index { get; set; }
        public string Username { get; set; }
        public string Coursename { get; set; }
        public DateTime AssignementDate { get; set; }
        public DateTime DueDate { get; set; }
        public string State { get; set; }
        public bool Checked { get; set; }
    }
}