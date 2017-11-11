using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSLearningSystem.Areas.Admin.Models
{
    public class CourseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Checked { get; set; }
    }
}