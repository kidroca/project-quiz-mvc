namespace QuizProjectMvc.Web.ViewModels.PageModels
{
    using System.Collections.Generic;
    using Quiz;
    using Services.Data.Models;
    using Services.Data.Models.DateRanges;

    public class HomePageViewModel : BaseQuizPageModel
    {
        public HomePageViewModel(int maxSolutionsForRankingPeriod, DateRange dateRange, Pager pager)
            : base(maxSolutionsForRankingPeriod, dateRange)
        {
            this.Pager = pager;
        }

        public IEnumerable<QuizCategoryViewModel> Categories { get; set; }

        public Pager Pager { get; }
    }
}
