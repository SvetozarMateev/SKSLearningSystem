using Microsoft.AspNet.Identity.EntityFramework;
using SKSLearningSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Data
{
    public class LearningSystemDbContext : IdentityDbContext<User>
    {
        public LearningSystemDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Course> Courses { get; set; }

        public virtual IDbSet<CourseState> CourseStates { get; set; }

        public static LearningSystemDbContext Create()
        {
            return new LearningSystemDbContext();
        }
    }
}
