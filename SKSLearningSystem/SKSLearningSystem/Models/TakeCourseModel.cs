using System.Collections.Generic;
using SKSLearningSystem.Data.Models;

namespace SKSLearningSystem.Models
{
    public class TakeCourseModel
    {
        public int Id { get; set; }

        public string CourseName { get; set; }

        public int CourseId { get; set; }

        public string Description { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}