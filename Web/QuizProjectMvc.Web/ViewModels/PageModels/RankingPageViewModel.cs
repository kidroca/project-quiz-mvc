namespace QuizProjectMvc.Web.ViewModels.PageModels
{
    using Services.Data.Models.DateRanges;

    public class RankingPageViewModel : BaseQuizPageModel
    {
        public RankingPageViewModel(int maxSolutionsForRankingPeriod, DateRange dateRange)
            : base(maxSolutionsForRankingPeriod, dateRange)
        {
        }
    }
}
