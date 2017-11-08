using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSLearningSystem.Areas.Admin.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public bool Checked { get; set; }
    }
}