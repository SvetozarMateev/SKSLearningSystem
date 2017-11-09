using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSLearningSystem.Areas.Admin.Services
{
    public class GridServices:IGridServices
    {
        public IList<User> Filtrator(string propertyName, string shortOp,string inputField,IList<User>users)
        {
            var result = new List<User>();
            switch (propertyName)
            {              
                case "Username":
                    result.AddRange(users.Where(x => x.UserName == inputField).ToList());
                    break;
                case "State":
                    result.AddRange(users.Where(x => x.CourseStates.Select(y=>y.State).Contains(inputField)));
                    break;
                case "Coursename":
                    result.AddRange(users.Where(x => x.CourseStates.Select(y => y.Course.Name).Contains(inputField)));
                    break;
            }

            return result;
        }
    }
}