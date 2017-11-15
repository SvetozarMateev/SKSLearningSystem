using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Data
{
    public class OverdueCheckJob : IJob
    {
        private LearningSystemDbContext dbContext = new LearningSystemDbContext();

        //public OverdueCheckJob(LearningSystemDbContext dbContext)
        //{
        //    this.dbContext = dbContext; 
        //}
        public void Execute(IJobExecutionContext context)
        {
            var courseStates = this.dbContext.CourseStates.ToList();
            for (int i = 0; i < courseStates.Count; i++)
            {
                if (courseStates[i].DueDate < DateTime.Now&& courseStates[i].State!="Overdue")
                {
                    courseStates[i].State = "Overdue";
                }
            }
            this.dbContext.SaveChanges();
        }
    }
}
