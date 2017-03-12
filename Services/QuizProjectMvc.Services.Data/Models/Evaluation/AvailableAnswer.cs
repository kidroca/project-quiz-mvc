namespace QuizProjectMvc.Services.Data.Models.Evaluation
{
    using Contracts;
    using QuizProjectMvc.Data.Models;
    using QuizProjectMvc.Web.Infrastructure.Mapping;

    public class AvailableAnswer : IAvailableAnswer, IMapFrom<Answer>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsCorrect { get; set; }
    }
}
