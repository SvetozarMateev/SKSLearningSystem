using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSLearningSystem.Areas.Admin.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            this.DueDate = DateTime.Now.AddDays(30);
        }

        public string Id { get; set; }

        public string UserName { get; set; }

        public bool Checked { get; set; }

        public DateTime DueDate { get; set; }

        public double Grade { get; set; }

        public bool Mandatory { get; set; }
    }
}