using System;
using System.ComponentModel.DataAnnotations;

namespace SKSLearningSystem.Models.ViewModels.AdminViewModels
{
    public class DepToCourseViewModel
    {
        public DepToCourseViewModel()
        {
            this.DueDate = DateTime.Now;       
        }
        
        [Required]
        public string CourseName { get; set; }

        [Required]        
        public string Department { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DueDate { get; set; }

        public bool Mandatory { get; set; }

        public double Grade { get; set; }
    }
}
