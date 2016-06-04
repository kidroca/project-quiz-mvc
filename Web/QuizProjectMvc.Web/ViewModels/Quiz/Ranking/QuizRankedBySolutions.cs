namespace QuizProjectMvc.Web.ViewModels.Quiz.Ranking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using Services.Data.Models.DateRanges;

    public class QuizRankedBySolutions : QuizRankedModel, IMapFrom<Quiz>, IHaveCustomMappings
    {
        private readonly RankBySolutionsComparator comparator;

        public QuizRankedBySolutions()
        {
            this.comparator = new RankBySolutionsComparator();
        }

        public IEnumerable<DateTime> SolutionDates { get; set; }

        /// <summary>
        /// Returns the number of solutions for a given period
        /// </summary>
        /// <param name="range">The date range to count solutions for</param>
        /// <returns>An integer number</returns>
        public override int GetRank(DateRange range)
        {
            return this.comparator.GetRank(range, this.SolutionDates);
        }

        public new void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Quiz, QuizRankedBySolutions>()
                .IncludeBase<Quiz, QuizBasicViewModel>()
                .ForMember(
                    dest => dest.SolutionDates,
                    opt => opt.MapFrom(src => src.Solutions.Select(s => s.CreatedOn)));
        }
    }
}
