using System;
using System.ComponentModel.DataAnnotations;

namespace SKSLearningSystem.Areas.Admin.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            this.DueDate = DateTime.Now.AddDays(30);
        }

        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        public bool Checked { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DueDate { get; set; }

        [Required]
        public double Grade { get; set; }

        public bool Mandatory { get; set; }
    }
}