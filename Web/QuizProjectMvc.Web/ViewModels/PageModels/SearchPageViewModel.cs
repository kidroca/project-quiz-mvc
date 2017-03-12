namespace QuizProjectMvc.Web.ViewModels.PageModels
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Services.Data.Models.DateRanges;
    using Services.Data.Models.Search;

    public class SearchPageViewModel : BaseQuizPageModel
    {
        public SearchPageViewModel(int maxSolutionsForRankingPeriod, DateRange dateRange)
            : base(maxSolutionsForRankingPeriod, dateRange)
        {
        }

        public QuizSearchModel QuizSearchModel { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
