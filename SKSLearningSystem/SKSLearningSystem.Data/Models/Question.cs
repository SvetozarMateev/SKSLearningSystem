using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Data.Models
{
   public class Question
    {
        private ICollection<Option> options;

        public Question()
        {
            this.options = new HashSet<Option>();
        }
        public int Id { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public string Statement { get; set; }

        public string Answer { get; set; }

        public virtual ICollection<Option> Options { get; set; }
    }
}
