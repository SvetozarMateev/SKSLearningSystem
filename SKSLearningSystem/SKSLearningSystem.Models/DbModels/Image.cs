using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Data.Models
{
   public class Image
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public Byte[] CurrentImage { get; set; }
    }
}
