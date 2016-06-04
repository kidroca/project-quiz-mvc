namespace QuizProjectMvc.Web.ViewModels.Quiz.Ranking
{
    using Services.Data.Models.DateRanges;

    /// <summary>
    /// A Quiz that can be ranked by number of solutions for a given time period
    /// </summary>
    public abstract class QuizRankedModel : QuizBasicViewModel, IRankedObject
    {
        /// <summary>
        /// Returns the quiz rank for a given period
        /// </summary>
        /// <param name="range">The date range to compare rankings</param>
        /// <returns>An integer number</returns>
        public abstract int GetRank(DateRange range);
    }
}
