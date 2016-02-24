namespace QuizProjectMvc.Web.ViewModels.Quiz
{
    using Data.Models;
    using Infrastructure.Mapping;

    public class AnswerModel : IMapFrom<Answer>
    {
        public int Id { get; set; }

        public string Text { get; set; }
    }
}
