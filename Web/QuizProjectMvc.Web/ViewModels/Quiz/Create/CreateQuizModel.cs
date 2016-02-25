namespace QuizProjectMvc.Web.ViewModels.Quiz.Create
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Common;
    using Data.Models;
    using Infrastructure.Mapping;

    public class CreateQuizModel : IMapTo<Quiz>, IHaveCustomMappings, IValidatableObject
    {
        [Required]
        [MinLength(ModelConstraints.TitleMinLength)]
        [MaxLength(ModelConstraints.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(ModelConstraints.DescriptionMinLength)]
        [MaxLength(ModelConstraints.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public ICollection<CreateQuestionModel> Questions { get; set; }

        public bool IsPrivate { get; set; }

        [Required]
        [MinLength(ModelConstraints.NameMinLength)]
        [MaxLength(ModelConstraints.NameMaxLength)]
        public string Category { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<CreateQuizModel, Quiz>()
                .ForMember(self => self.Category, opt => opt.Ignore());
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
