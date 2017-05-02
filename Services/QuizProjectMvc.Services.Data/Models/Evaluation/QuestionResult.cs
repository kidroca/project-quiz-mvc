namespace QuizProjectMvc.Services.Data.Models.Evaluation
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Contracts;
    using QuizProjectMvc.Data.Models;
    using QuizProjectMvc.Web.Infrastructure.Mapping;

    public class QuestionResult : IQuestionResult, IMapFrom<Question>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public int Id { get; set; }

        public int CorrentAnswerId => this.Answers.First(a => a.IsCorrect).Id;

        public int SelectedAnswerId { get; set; }

        public string ResultDescription { get; set; }

        public bool AnsweredCorrectly => this.CorrentAnswerId == this.SelectedAnswerId;

        public IList<IAvailableAnswer> Answers { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Answer, IAvailableAnswer>().As<AvailableAnswer>();
        }
    }
}
