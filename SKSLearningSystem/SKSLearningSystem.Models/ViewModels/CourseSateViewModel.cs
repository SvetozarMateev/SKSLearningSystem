using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKSLearningSystem.Models.ViewModels
{
    public class CourseSateViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int PicId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string CourseName { get; set; }

        [Required]
        public bool Passed { get; set; }

        public bool Mandatory { get; set; }

        public double Grade { get; set; }

        [Required]
        public string State { get; set; }

        public int CourseId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Column(TypeName = "DateTime2")]
        public DateTime AssignmentDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Column(TypeName = "DateTime2")]
        public DateTime DueDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Column(TypeName = "DateTime2")]
        public DateTime CompletionDate { get; set; }
    }
}
