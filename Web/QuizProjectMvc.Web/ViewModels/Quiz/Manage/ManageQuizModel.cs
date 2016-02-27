namespace QuizProjectMvc.Web.ViewModels.Quiz.Manage
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Common;
    using Data.Models;
    using Infrastructure.Mapping;

    public class ManageQuizModel : IMapFrom<Quiz>, IMapTo<Quiz>, IHaveCustomMappings, IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstraints.TitleMinLength)]
        [MaxLength(ModelConstraints.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(ModelConstraints.DescriptionMinLength)]
        [MaxLength(ModelConstraints.DescriptionMaxLength)]
        public string Description { get; set; }

        public bool IsPrivate { get; set; }

        [Required]
        [MinLength(ModelConstraints.NameMinLength)]
        [MaxLength(ModelConstraints.NameMaxLength)]
        public string Category { get; set; }

        [Required]
        public ICollection<ManageQuestionModel> Questions { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<ManageQuizModel, Quiz>()
                .ForMember(self => self.Category, opt => opt.Ignore());

            configuration.CreateMap<Quiz, ManageQuizModel>()
                .ForMember(
                    self => self.Category,
                    opt => opt.MapFrom(model => model.Category.Name));
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Questions == null ||
                this.Questions.Count < ModelConstraints.MinQuestionsCount)
            {
                yield return new ValidationResult(
                    string.Format(
                        "Question count must be at least {0}",
                        ModelConstraints.MinQuestionsCount));
            }
        }
    }
}
