namespace QuizProjectMvc.Web.ViewModels.Quiz.Create
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Common;
    using Data.Models;
    using Infrastructure.Mapping;

    public class CreateQuizModel : IMapTo<Quiz>, IHaveCustomMappings
    {
        [Required]
        [MinLength(ModelConstraints.TitleMinLength)]
        [MaxLength(ModelConstraints.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(ModelConstraints.DescriptionMinLength)]
        [MaxLength(ModelConstraints.DescriptionMaxLength)]
        public string Description { get; set; }

        public ICollection<CreateQuestionModel> Questions { get; set; }

        public bool IsPrivate { get; set; }

        [Required]
        [MinLength(ModelConstraints.NameMinLength)]
        [MaxLength(ModelConstraints.NameMaxLength)]
        public string Category { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<CreateQuizModel, Quiz>()
                .ForMember(
                    dest => dest.Category,
                    opt => opt.MapFrom(self => new QuizCategory { Name = self.Category }));
        }
    }
}
