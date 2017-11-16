using System.Collections.Generic;
using SKSLearningSystem.Data.Models;

namespace SKSLearningSystem.Models
{
    public class TakeCourseModel
    {
        public string CourseName { get; set; }

        public int CourseId { get; set; }

        public int CourseStateId { get; set; }

        public string Description { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}