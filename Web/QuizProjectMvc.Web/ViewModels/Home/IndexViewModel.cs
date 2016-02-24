namespace QuizProjectMvc.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using Quiz;
    using Services.Data.Models;

    public class IndexViewModel
    {
        public IEnumerable<QuizBasicViewModel> Quizzes { get; set; }

        public IEnumerable<QuizCategoryViewModel> Categories { get; set; }

        public Pager Pager { get; set; }
    }
}
