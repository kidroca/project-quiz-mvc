namespace QuizProjectMvc.Web.ViewModels.Quiz.Solve
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class QuizForSolvingModel : IMapFrom<Quiz>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<QuestionModel> Questions { get; set; }

        public string Description { get; set; }

        public string AvatarUrl { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Quiz, QuizForSolvingModel>()
                .ForMember(
                    self => self.Questions,
                    opt => opt.MapFrom(model => model.NumberOfQuestions > 0
                        ? model.Questions.OrderBy(q => Guid.NewGuid()).Take(model.NumberOfQuestions)
                        : model.Questions.OrderBy(q => q.CreatedOn)))
                .ForMember(
                    self => self.AvatarUrl,
                    opt => opt.MapFrom(model => model.Category.AvatarUrl));
        }
    }
}
