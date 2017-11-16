using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSLearningSystem.Models
{
    public class OptionViewModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public bool IsSelected { get; set; }
        public string Letter { get; set; }
        public string Answer { get; set; }
    }
}