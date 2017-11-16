using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Models.ViewModels.AdminViewModels
{
    public class DepToCourseViewModel
    {
        [Required]
        public string CourseName { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public bool Mandatory { get; set; }
        public double Grade { get; set; }
    }
}
