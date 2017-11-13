using SKSLearningSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSLearningSystem.Areas.Admin.Services
{
    public interface IGridServices
    {
       // IList<User> Filtrator(string propertyName, string shortOp, string inputField, IList<User> users);

         object SearchFalseResult();

        object SearchResultTrue(string filters);
    }
}