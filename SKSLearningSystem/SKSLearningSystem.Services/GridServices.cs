using Bytes2you.Validation;
using Newtonsoft.Json;
using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SKSLearningSystem.Areas.Admin.Services
{
    public class GridServices:IGridServices
    {
        private readonly LearningSystemDbContext db;

        public GridServices(LearningSystemDbContext db)
        {
            Guard.WhenArgument(db, "db").IsNull().Throw();
            this.db = db;
        }
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
        public object SearchResultTrue(string filters)
        {
            var parsedFilters = JsonConvert.DeserializeObject<GridRequestViewModel>(filters);
            var counter = 1;
            var users = this.db.Users.ToList();
            var propertyName = parsedFilters.rules.First().field;
            var shortenedOperator = parsedFilters.rules.First().op;
            var inputedField = parsedFilters.rules.First().data;


            var result = new List<CourseStateRowViewModel>();
            foreach (var user in this.Filtrator(propertyName, shortenedOperator, inputedField, users))
            {
                foreach (var courses in user.CourseStates)
                {
                    if (courses.DueDate < DateTime.Now)
                    {
                        courses.State = "Overdue";
                    }
                    result.Add(new CourseStateRowViewModel()
                    {
                        Index = counter,
                        Username = user.UserName,
                        Coursename = courses.Course.Name,
                        AssignementDate = courses.AssignmentDate,
                        DueDate = courses.DueDate,
                        State = courses.State
                    });
                }
            }
            var needed = new { total = 1, page = 1, records = result.Count, rows = result };
            return needed;
        }
        public object SearchFalseResult(int page, int rows)
        {
            var counter = page*rows-9;
            var users = db.Users.ToList();
            var result = new List<CourseStateRowViewModel>();
            foreach (var user in users)
            {
                foreach (var courses in user.CourseStates)
                {
                    if (courses.DueDate < DateTime.Now)
                    {
                        courses.State = "Overdue";
                    }
                    result.Add(new CourseStateRowViewModel()
                    {
                        Index = counter,
                        Username = user.UserName,
                        Coursename = courses.Course.Name,
                        AssignementDate = courses.AssignmentDate,
                        DueDate = courses.DueDate,
                        State = courses.State
                    });
                    counter++;
                }
            }
            var needed = new { total = result.Count/rows+1, page = page, records = result.Count, rows = result.Skip((page-1)*rows).Take(rows).ToList() };
            return needed;
        }
    }
}