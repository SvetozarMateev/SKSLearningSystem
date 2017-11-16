using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Models.ViewModels
{
    public class CourseSateViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int PicId { get; set; }

        public string Description { get; set; }

        public string CourseName { get; set; }

        public bool Passed { get; set; }

        public bool Mandatory { get; set; }

        public double Grade { get; set; }

        public string State { get; set; }

        public int CourseId { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime AssignmentDate { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime DueDate { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime CompletionDate { get; set; }
    }
}
