namespace QuizProjectMvc.Web.ViewModels.Quiz.Ranking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using Services.Data.Models.DateRanges;

    public class CategoryRankedBySolutions : IMapFrom<QuizCategory>, IHaveCustomMappings, IRankedObject
    {
        private readonly RankBySolutionsComparator comparator;

        public CategoryRankedBySolutions()
        {
            this.comparator = new RankBySolutionsComparator();
        }

        public string Name { get; set; }

        public string AvatarUrl { get; set; }

        public IEnumerable<DateTime> SolutionDates { get; set; }

        /// <summary>
        /// Returns the number of solutions for a given period
        /// </summary>
        /// <param name="range">The date range to count solutions for</param>
        /// <returns>An integer number</returns>
        public int GetRank(DateRange range)
        {
            return this.comparator.GetRank(range, this.SolutionDates);
        }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<QuizCategory, CategoryRankedBySolutions>()
                .ForMember(
                    dest => dest.SolutionDates,
                    opt => opt.MapFrom(src => src.Quizzes.SelectMany(
                        q => q.Solutions).Select(s => s.CreatedOn)));
        }
    }
}
