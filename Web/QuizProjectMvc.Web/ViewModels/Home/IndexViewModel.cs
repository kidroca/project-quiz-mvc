namespace QuizProjectMvc.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using Quiz;

    public class IndexViewModel
    {
        public IEnumerable<QuizBasicViewModel> Quizzes { get; set; }

        public IEnumerable<QuizCategoryViewModel> Categories { get; set; }
    }
}
