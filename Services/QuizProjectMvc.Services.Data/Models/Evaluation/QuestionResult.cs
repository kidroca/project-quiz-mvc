namespace QuizProjectMvc.Services.Data.Models.Evaluation
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using QuizProjectMvc.Data.Models;
    using QuizProjectMvc.Web.Infrastructure.Mapping;

    public class QuestionResult : IQuestionResult, IMapFrom<Question>
    {
        // Todo: Add setters for automapper ?
        public string Title { get; }

        public int Id { get; }

        public int CorrentAnswerId => this.Answers.First(a => a.IsCorrect).AnswerId;

        public int SelectedAnswerId { get; set; }

        public string ResultDescription { get; }

        public bool AnsweredCorrectly => this.CorrentAnswerId == this.SelectedAnswerId;

        public IList<IAvailableAnswer> Answers { get; }
    }
}