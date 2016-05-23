namespace QuizProjectMvc.Web.ViewModels.Quiz.Ranking
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class QuizRankedCollection : IMapFrom<IEnumerable<IGrouping<Quiz, QuizSolution>>>, IHaveCustomMappings
    {
        public int MaxSolutionsForRankingPeriod { get; set; }

        public IEnumerable<QuizRankedModel> QuizRankedModels { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<IEnumerable<IGrouping<Quiz, QuizSolution>>, QuizRankedCollection>()
                .ForMember(
                    dest => dest.MaxSolutionsForRankingPeriod,
                    opt => opt.MapFrom(
                        model => model.FirstOrDefault() != null
                            ? model.First().Count()
                            : 0))
                .ForMember(
                    dest => dest.QuizRankedModels,
                    opt => opt.MapFrom(model => model.FirstOrDefault()));
        }
    }
}
