namespace QuizProjectMvc.Web.ViewModels.ProfileInformation
{
    using System;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class TakenQuizInfo : IMapFrom<QuizSolution>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<QuizSolution, TakenQuizInfo>()
                .ForMember(
                    self => self.Title,
                    opt => opt.MapFrom(model => model.ForQuiz.Title));
        }
    }
}
