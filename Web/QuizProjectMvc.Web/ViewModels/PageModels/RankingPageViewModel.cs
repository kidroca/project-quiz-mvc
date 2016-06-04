namespace QuizProjectMvc.Web.ViewModels.PageModels
{
    using System.Collections.Generic;
    using Quiz.Ranking;
    using Services.Data.Models.DateRanges;

    public class RankingPageViewModel : BaseQuizPageModel
    {
        public RankingPageViewModel(int maxSolutionsForRankingPeriod, DateRange dateRange)
            : base(maxSolutionsForRankingPeriod, dateRange)
        {
        }

        public IEnumerable<CategoryRankedBySolutions> Categories { get; set; }
    }
}
