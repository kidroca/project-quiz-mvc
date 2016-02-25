namespace QuizProjectMvc.Web.ViewModels.Quiz.Solve
{
    using System.Collections.Generic;
    using Data.Models;
    using Infrastructure.Mapping;

    public class QuizForSolvingModel : IMapFrom<Quiz>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<QuestionModel> Questions { get; set; }
    }
}
