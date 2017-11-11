using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSLearningSystem.Models
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Statement { get; set; }
        public IList<OptionViewModel> Options { get; set; }
    }
}