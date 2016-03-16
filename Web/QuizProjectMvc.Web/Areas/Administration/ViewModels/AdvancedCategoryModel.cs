namespace QuizProjectMvc.Web.Areas.Administration.ViewModels
{
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using Web.ViewModels.Quiz;

    public class AdvancedCategoryModel : QuizCategoryViewModel, IHaveCustomMappings, IMapTo<QuizCategory>
    {
        public int QuizzesCount { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<QuizCategory, AdvancedCategoryModel>()
                .ForMember(
                    self => self.QuizzesCount,
                    opt => opt.MapFrom(model => model.Quizzes.Count));
        }
    }
}
