using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SKSLearningSystem.Data.Models
{
    public class User : IdentityUser
    {
        //private ICollection<CourseState> courseStates;

        public User()
        {
            //this.courseState = new HashSet<CourseState>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string Department { get; set; }

        //public virtual ICollection<CourseState> CourseStates
        //{
        //    get
        //    {
        //        return this.courseStates;
        //    }
        //    set
        //    {
        //        this.courseStates = value;
        //    }
        //}
    }
}
