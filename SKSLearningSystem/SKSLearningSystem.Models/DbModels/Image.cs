using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Data.Models
{
   public class Image
    {
        public int Id { get; set; }

        public int? CourseId { get; set; }

        public string UserId { get; set; }

        [Required]
        public Byte[] CurrentImage { get; set; }
    }
}
