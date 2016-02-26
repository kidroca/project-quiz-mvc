namespace QuizProjectMvc.Web.ViewModels.ProfileInformation
{
    using System.Linq;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class PublicProfileInformation : UserBasicInfoViewModel, IMapFrom<User>, IHaveCustomMappings
    {
        public int QuizzesCreated { get; set; }

        public double Rating { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<User, PublicProfileInformation>()
                .ForMember(
                    self => self.Rating,
                    opt => opt.MapFrom(
                        model => model.QuizzesCreated.SelectMany(q => q.Ratings)
                            .Average(r => r.Value)));
        }
    }
}
