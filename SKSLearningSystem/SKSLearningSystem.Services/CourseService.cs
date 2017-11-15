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
using SKSLearningSystem.Areas.Admin.Models;
using System.Data.Entity;

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

        public TakeTestViewModel GetTestViewModel(int courseStateId)
        {
            var courseState = this.context.CourseStates.First(x => x.Id == courseStateId);
            var questions = courseState.Course.Questions.Select(x => new QuestionViewModel()
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
            var model = new TakeTestViewModel()
            {
                Questions = questions,
                CourseName = courseState.Course.Name,
                CourseStateId = courseStateId
            };
            return model;
        }

        public bool ValidateTest(TakeTestViewModel questions)
        {
            if (questions.Questions.Any(x => x.Options.Where(y => y.IsSelected == true).Count() > 1))
            {
                return false;
            }
            return true;
        }

        public double GradeExam(TakeTestViewModel questions)
        {
            var correctAnswersCount = 0.0;
            for (int i = 0; i < questions.Questions.Count; i++)
            {
                var currQuestion = questions.Questions[i];
                var selectedOption = currQuestion.Options.First(x => x.IsSelected == true);
                var answer=this.context.Questions.First(x => x.Id == currQuestion.Id).Answer;
                if (selectedOption.Letter == answer)
                {
                    correctAnswersCount++;
                }
            }
            return correctAnswersCount/questions.Questions.Count*100;
        }

        public void ChangeCourseState(int courseStateId, string newState,double grade)
        {
            var course = this.context.CourseStates.First(x => x.Id == courseStateId);
            if (course.State != "Completed")
            {
                course.Grade = grade;
                course.Passed = true;
                course.State = newState;
                this.context.SaveChanges();
            }
            
        }

        
    }
}
