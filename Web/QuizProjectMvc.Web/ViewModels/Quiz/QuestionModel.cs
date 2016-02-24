namespace QuizProjectMvc.Web.ViewModels.Quiz
{
    using System.Collections.Generic;
    using Data.Models;
    using Infrastructure.Mapping;

    public class QuestionModel : IMapFrom<Question>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public virtual IEnumerable<AnswerModel> Answers { get; set; }
    }
}
