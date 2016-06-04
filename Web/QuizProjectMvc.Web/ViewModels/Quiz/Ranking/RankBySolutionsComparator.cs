namespace QuizProjectMvc.Web.ViewModels.Quiz.Ranking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Services.Data.Models.DateRanges;

    /// <summary>
    /// Used to compare solutions count for a given date range
    /// </summary>
    public class RankBySolutionsComparator
    {
        private readonly Dictionary<DateRange, int> rangeCache;

        public RankBySolutionsComparator()
        {
            this.rangeCache = new Dictionary<DateRange, int>();
        }

        /// <summary>
        /// Returns the number of solutions for a given period
        /// </summary>
        /// <param name="range">The date range to count solutions for</param>
        /// <param name="solutionDates"> An iterable of <see cref="DateTime"/></param>
        /// <returns>An integer number</returns>
        public int GetRank(DateRange range, IEnumerable<DateTime> solutionDates)
        {
            if (!this.rangeCache.ContainsKey(range))
            {
                int result = solutionDates.Count(date => range.From <= date && date <= range.To);
                this.rangeCache[range] = result;
            }

            return this.rangeCache[range];
        }
    }
}
