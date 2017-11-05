using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Data.Models
{
    public class Course
    {
        public Course()
        {

        }

        public int Id { get; set; }

        public ICollection<CourseState> Registry { get; set; }

    }
}
