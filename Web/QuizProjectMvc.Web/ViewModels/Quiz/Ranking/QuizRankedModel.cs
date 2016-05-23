namespace QuizProjectMvc.Web.ViewModels.Quiz.Ranking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using Services.Data.Models.DateRanges;

    /// <summary>
    /// A Quiz that can be ranked by number of solutions for a given time period
    /// </summary>
    public class QuizRankedModel : QuizBasicViewModel, IMapFrom<Quiz>, IHaveCustomMappings
    {
        private readonly Dictionary<DateRange, int> rangeCache;

        public QuizRankedModel()
        {
            this.rangeCache = new Dictionary<DateRange, int>();
        }

        public IEnumerable<DateTime> SolutionDates { get; set; }

        /// <summary>
        /// Returns the number of solutions for a given period
        /// </summary>
        /// <param name="range">The date range to count solutions for</param>
        /// <returns>An integer number</returns>
        public int SolutionsCountForRankingPeriod(DateRange range)
        {
            if (!this.rangeCache.ContainsKey(range))
            {
                int result = this.SolutionDates.Count(date => range.From <= date && date <= range.To);
                this.rangeCache[range] = result;
            }

            return this.rangeCache[range];
        }

        public new void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Quiz, QuizRankedModel>()
                .IncludeBase<Quiz, QuizBasicViewModel>()
                .ForMember(
                    dest => dest.SolutionDates,
                    opt => opt.MapFrom(model => model.Solutions.Select(s => s.CreatedOn)));
        }
    }
}
