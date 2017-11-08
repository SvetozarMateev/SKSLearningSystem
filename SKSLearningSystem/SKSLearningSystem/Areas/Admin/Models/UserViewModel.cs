using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSLearningSystem.Areas.Admin.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public bool Checked { get; set; }

        public DateTime DueDate { get; set; }

        public bool Mandatory { get; set; }
    }
}