namespace QuizProjectMvc.Web.ViewModels.Quiz.Ranking
{
    using Services.Data.Models.DateRanges;

    public interface IRankedObject
    {
        /// <summary>
        /// Returns the rank of the object for a given period
        /// </summary>
        /// <param name="range">The date range to compare rankings</param>
        /// <returns>An integer number</returns>
        int GetRank(DateRange range);
    }
}
