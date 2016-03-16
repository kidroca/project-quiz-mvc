namespace QuizProjectMvc.Web.ViewModels.Quiz.Edit
{
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using Manage;

    public class EditQuizModel : ManageQuizModel, IMapFrom<Quiz>, IMapTo<Quiz>, IHaveCustomMappings
    {
        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<EditQuizModel, Quiz>()
                .ForMember(
                    self => self.Questions,
                    opt => opt.Ignore());
        }
    }
}
