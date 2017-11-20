using System;
using System.ComponentModel.DataAnnotations;

namespace SKSLearningSystem.Areas.Admin.Models
{
    public class CourseStateRowViewModel
    {
        public CourseStateRowViewModel()
        {
            this.DueDate = DateTime.Now.AddDays(30);
            this.AssignementDate = DateTime.Now;
        }

        public int Index { get; set; }

        public string Username { get; set; }

        public string Coursename { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime AssignementDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DueDate { get; set; }

        public string State { get; set; }
    }
}