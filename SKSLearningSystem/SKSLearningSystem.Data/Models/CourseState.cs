using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Data.Models
{
    public class CourseState
    {
        public CourseState()
        {

        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public int CourseId { get; set; }

        public bool Passed { get; set; }

        public virtual User User { get; set; }

        public virtual Course Course { get; set; }

        public bool Mandatory { get; set; }

        [Range(0,100)]
        public double Grade { get; set; }
        
        public string State { get; set; }

        public DateTime AssignmentDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CompletionDate { get; set; }
    }
}
