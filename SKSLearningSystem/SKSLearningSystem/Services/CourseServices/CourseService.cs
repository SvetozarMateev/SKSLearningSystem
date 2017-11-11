using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKSLearningSystem.Data.Models;
using SKSLearningSystem.Data;
using System.IO;
using SKSLearningSystem.Models;
using Bytes2you.Validation;

namespace SKSLearningSystem.Services.CourseServices
{
    public class CourseService : ICourseService
    {
        private LearningSystemDbContext context;

        public CourseService(LearningSystemDbContext context)
        {
            Guard.WhenArgument(context, "context").IsNull().Throw();

            this.context = context;
        }

        public string GetCourseName(int courseId)
        {
            // Get the assigned from admin course to user
            var assignedCourse = this.context.Courses.First(c => c.Id == courseId);
            var courseName = assignedCourse.Name;

            return courseName;
        }

        public ICollection<Image> GetImages(int courseId)
        {
            var images = this.context.Courses.First(c => c.Id == courseId).Images;

            return images.ToList();
        }

        public IList<QuestionViewModel> GetQuestionsForCourse(int courseId)
        {
            var course = this.context.Courses.First(x => x.Id == courseId);
            var questions = course.Questions.Select(x => new QuestionViewModel()
            {
                Id = x.Id,
                CourseId = x.CourseId,
                Options = x.Options.Select(y => new OptionViewModel()
                {
                    Answer = y.Answer,
                    Letter = y.Letter,
                    Id = y.Id,
                    QuestionId = y.QuestionId
                }).ToList(),
                Statement = x.Statement
            }).ToList();
            return questions;
        }

        public bool ValidateTest(IList<QuestionViewModel> questions)
        {
            if (questions.Any(x => x.Options.Where(y => y.IsSelected == true).Count() > 1))
            {
                return false;
            }
            return true;
        }

        public double GradeExam(IList<QuestionViewModel> questions)
        {
            var correctAnswersCount = 0.0;
            for (int i = 0; i < questions.Count; i++)
            {
                var currQuestion = questions[i];
                var selectedOption = currQuestion.Options.First(x => x.IsSelected == true);
                var answer=this.context.Questions.First(x => x.Id == currQuestion.Id).Answer;
                if (selectedOption.Letter == answer)
                {
                    correctAnswersCount++;
                }
            }
            return correctAnswersCount/questions.Count*100;
        }
    }
}
