using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSLearningSystem.Areas.Admin.Models
{
    public class GridRequestViewModel
    {       
        public string groupOp { get; set; }
        public List<RulesViewModel> rules { get; set; }
    }
}