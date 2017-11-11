using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Data.Models
{
   public class Option
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        [Required]
        [StringLength(2)]
        public string Letter { get; set; }

        [Required]
        [StringLength(maximumLength:100,MinimumLength =2)]
        public string Answer { get; set; }
    }
}
