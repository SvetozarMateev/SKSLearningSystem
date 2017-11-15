using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Data.Models
{
    public class Course
    {
        private ICollection<CourseState> registry;
        private ICollection<Image> images;
        private ICollection<Question> questions;

        public Course()
        {
            this.registry = new HashSet<CourseState>();
            this.images = new HashSet<Image>();
            this.questions = new HashSet<Question>();
        }

        public int Id { get; set; }

       
        [Required]
       
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<CourseState> Registry { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

    }
}
