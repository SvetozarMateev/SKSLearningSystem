using Bytes2you.Validation;
using Newtonsoft.Json;
using SKSLearningSystem.Areas.Admin.Models;
using SKSLearningSystem.Data;
using SKSLearningSystem.Data.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SKSLearningSystem.Areas.Admin.Services
{
    public class AdminServices : IAdminServices
    {
        private readonly LearningSystemDbContext db;

        public AdminServices(LearningSystemDbContext db)
        {
            Guard.WhenArgument(db, "db").IsNull().Throw();

            this.db = db;
        }

        public bool ValidateInputFiles(UploadCourseViewModel model)
        {
            if (model == null)
            {
                return false;
            }
            var supportedTypesForFile = new[] { "json" };
            var supportedTypesForPics = new[] { "png", "jpg", "jpeg" };

            var fileExt = Path.GetExtension(model.CourseFile.FileName).Substring(1);

            if (!supportedTypesForFile.Contains(fileExt.ToLower()))
            {
                return false;
            }

            foreach (var item in model.Photos)
            {
                var picExt = Path.GetExtension(item.FileName).Substring(1);

                if (!supportedTypesForPics.Contains(picExt))
                {
                    return false;
                }
            }
            return true;
        }

        public Course ReadCourseFromJSON(HttpPostedFileBase model)
        {
            Guard.WhenArgument(model, "model").IsNull().Throw();

            var stream = model.InputStream;
            Course course;
            using (StreamReader reader = new StreamReader(stream))
            {
                var allContent = reader.ReadToEnd();
                Guard.WhenArgument(allContent, "allContent").IsNullOrWhiteSpace().IsEmpty().Throw();
                course = JsonConvert.DeserializeObject<Course>(allContent);
            }

            Guard.WhenArgument(course.Name, "course name").IsNullOrWhiteSpace().IsEmpty().Throw();
            Guard.WhenArgument(course.Description, "course description").IsNullOrWhiteSpace().IsEmpty().Throw();

            return course;
        }

        public ICollection<Image> ReadImagesFromFiles(IEnumerable<HttpPostedFileBase> model)
        {
            Guard.WhenArgument(model, "model").IsNull().Throw();

            ICollection<Image> images = new List<Image>();
            foreach (var item in model)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    item.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                    images.Add(new Image() { CurrentImage = array });
                }
            }
            return images;
        }

        public void SaveCourseToDB(Course course)
        {
            Guard.WhenArgument(course, "course").IsNull().Throw();
            this.db.Courses.Add(course);
            this.db.SaveChanges();
        }

        public AssignCourseViewModel GetUsersAndCoursesFromDB()
        {
            var users = this.db
               .Users
               .Select(u => new UserViewModel()
               {
                   UserName = u.UserName,
                   Id = u.Id
               }).ToList();

            var courses = this.db
                .Courses
                .Select(c => new CourseViewModel()
                {
                    Name = c.Name,
                    Id = c.Id
                })
                .ToList();

            var assignCourseViewModel = new AssignCourseViewModel()
            {
                Courses = courses,
                Users = users
            };

            return assignCourseViewModel;
        }

        private bool ValidateState(string userId, int courseId)
        {
            bool answer = db.CourseStates.Any(x => userId == x.UserId && courseId == x.CourseId);

            return answer;
        }

        public void SaveAssignedCoursesToDb(AssignCourseViewModel assignCourseViewModel)
        {
            var userIds = assignCourseViewModel.Users.Select(y => y.Id).ToArray();
            var courseIds = assignCourseViewModel.Courses.Select(cr => cr.Id).ToArray();

            var users = db.Users.Where(x => userIds.Contains(x.Id)).ToList();
            var courses = db.Courses.Where(c => courseIds.Contains(c.Id)).ToList();


            for (int i = 0; i < users.Count; i++)
            {
                for (int j = 0; j < courses.Count; j++)
                {
                    if (!ValidateState(userIds[i], courseIds[j]))
                    {
                        CourseState state = new CourseState()
                        {
                            User = users[i],
                            Course = courses[j],
                            DueDate = assignCourseViewModel.Users[i].DueDate,
                            Mandatory = assignCourseViewModel.Users[i].Mandatory
                        };

                        users[i].CourseStates.Add(state);
                        courses[j].Registry.Add(state);
                        db.CourseStates.Add(state);
                    }


                }
            }

            db.SaveChanges();
        }
    }
}