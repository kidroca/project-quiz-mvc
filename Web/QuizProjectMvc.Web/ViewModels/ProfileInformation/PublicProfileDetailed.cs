namespace QuizProjectMvc.Web.ViewModels.ProfileInformation
{
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class PublicProfileDetailed : PublicProfile, IMapFrom<User>, IHaveCustomMappings
    {
        public static int MaxQuizzesCreated { get; set; }

        public int QuizzesCreated { get; set; }

        public double Rating => (this.QuizzesCreated / (double)MaxQuizzesCreated) * 10;

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<User, PublicProfileDetailed>()
                .ForMember(
                    self => self.QuizzesCreated,
                    opt => opt.MapFrom(model => model.QuizzesCreated.Count));
        }
    }
}
