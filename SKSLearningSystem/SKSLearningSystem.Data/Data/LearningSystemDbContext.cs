using Microsoft.AspNet.Identity.EntityFramework;
using SKSLearningSystem.Data.Models;
using System;
using System.Collections.Generic;
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

        public static LearningSystemDbContext Create()
        {
            return new LearningSystemDbContext();
        }
    }
}
