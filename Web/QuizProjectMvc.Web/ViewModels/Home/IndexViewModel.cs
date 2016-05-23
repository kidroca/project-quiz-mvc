namespace QuizProjectMvc.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using Quiz;

    public class IndexViewModel
    {
        public IEnumerable<QuizCategoryViewModel> Categories { get; set; }
    }
}
