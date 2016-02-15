namespace QuizProjectMvc.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<QuizBasicViewModel> Quizzes { get; set; }

        public IEnumerable<QuizCategoryViewModel> Categories { get; set; }
    }
}
