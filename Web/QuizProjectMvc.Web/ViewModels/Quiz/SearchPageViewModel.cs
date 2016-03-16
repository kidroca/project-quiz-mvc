namespace QuizProjectMvc.Web.ViewModels.Quiz
{
    using System.Collections.Generic;
    using Services.Data.Models.Search;

    public class SearchPageViewModel
    {
        public QuizSearchModel QuizSearchModel { get; set; }

        public IEnumerable<QuizBasicViewModel> Quizzes { get; set; }
    }
}
