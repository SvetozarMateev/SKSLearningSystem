using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Data.Models
{
    public class Course
    {
        private ICollection<CourseState> registry;
        private ICollection<Image> images;

        public Course()
        {
            this.registry = new HashSet<CourseState>();
            this.images = new HashSet<Image>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<CourseState> Registry { get; set; }

        public virtual ICollection<Image> Images { get; set; }

    }
}
