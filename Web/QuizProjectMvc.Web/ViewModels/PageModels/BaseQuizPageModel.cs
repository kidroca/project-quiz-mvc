namespace QuizProjectMvc.Web.ViewModels
{
    using System.Collections.Generic;
    using Quiz.Ranking;
    using Services.Data.Models.DateRanges;

    public class BaseQuizPageModel
    {
        public BaseQuizPageModel(int maxSolutionsForRankingPeriod, DateRange dateRange)
        {
            this.DateRange = dateRange;
            this.MaxSolutionsForRankingPeriod = maxSolutionsForRankingPeriod;
        }

        public int MaxSolutionsForRankingPeriod { get; }

        public DateRange DateRange { get; }

        public IEnumerable<QuizRankedModel> Quizzes { get; set; }
    }
}
