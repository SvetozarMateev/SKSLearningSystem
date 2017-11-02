using Microsoft.AspNet.Identity.EntityFramework;
using SKSLearningSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Data
{
    public class LeatningSystemDbContext : IdentityDbContext<User>
    {
        public LeatningSystemDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static LeatningSystemDbContext Create()
        {
            return new LeatningSystemDbContext();
        }
    }
}
