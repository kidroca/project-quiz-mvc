namespace QuizProjectMvc.Services.Data.Models.Evaluation
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Contracts;
    using QuizProjectMvc.Data.Models;
    using QuizProjectMvc.Web.Infrastructure.Mapping;

    public class QuizEvaluationResult : IQuizEvaluationResult, IHaveCustomMappings
    {
        public int ForQuizId { get; set; }

        public string Title { get; set; }

        public ICollection<IQuestionResult> QuestionResults { get; set; }

        public int TotalQuestions => this.QuestionResults.Count;

        public double GetSuccessPercentage()
        {
            var correctlyAnsweredCount = (double)this.QuestionResults.Count(q => q.AnsweredCorrectly);
            return (correctlyAnsweredCount / this.TotalQuestions) * 100;
        }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Question, IQuestionResult>().As<QuestionResult>();

            configuration.CreateMap<Quiz, QuizEvaluationResult>()
                .ForMember(
                    self => self.ForQuizId,
                    opt => opt.MapFrom(m => m.Id))
                .ForMember(
                    self => self.QuestionResults,
                    opt => opt.MapFrom(m => m.Questions));
        }
    }
}
