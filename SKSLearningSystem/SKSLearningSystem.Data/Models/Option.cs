using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Data.Models
{
   public class Option
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        public string Letter { get; set; }

        public string Answer { get; set; }
    }
}
