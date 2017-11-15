using Bytes2you.Validation;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace SKSLearningSystem.Services
{
    public class HomeServices : IHomeServices
    {
        private LearningSystemDbContext context;

        public HomeServices(LearningSystemDbContext context)
        {
            Guard.WhenArgument(context, "context").IsNull().Throw();
            this.context = context;
        }

        public MyProfileViewModel GetCourseStates(string username)
        {

            var myProfileViewModel = new MyProfileViewModel();
           
            var user = this.context.Users.First(x => x.UserName == username);
            var allStates = context.CourseStates
            .Where(x => x.UserId == user.Id)
            .Select(x => new CourseSateViewModel()
            {
                CourseId=x.CourseId,
                Id = x.Id,
                Grade = x.Grade,
                Passed = x.Passed,
                DueDate = x.DueDate,
                AssignmentDate = x.AssignmentDate,
                CompletionDate = x.CompletionDate,
                CourseName = x.Course.Name,
                Mandatory = x.Mandatory,
                State = x.State,
                PicId = x.Course.Images.FirstOrDefault().Id,
                Description = x.Course.Description
            }).ToList();
            myProfileViewModel.Overdues = allStates.Where(x => x.State == "Overdue").ToList();
            myProfileViewModel.Pendings = allStates.Where(x => x.State == "Pending").ToList();
            myProfileViewModel.Completed = allStates.Where(x => x.State == "Completed").ToList();
            myProfileViewModel.Started = allStates.Where(x => x.State == "Started").ToList();

            return myProfileViewModel;
        }

        public async Task SaveImagesToUser(Image file, string userId)
        {
            Guard.WhenArgument(file, "file").IsNull().Throw();
            var userRealId = this.context.Users.First(x => x.UserName == userId).Id;
            file.UserId = userRealId;
            context.Images.Add(file);
           await context.SaveChangesAsync();
        }
    }
}