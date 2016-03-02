namespace QuizProjectMvc.Web.ViewModels.Quiz.Manage
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Common;
    using Data.Models;
    using Infrastructure.Mapping;

    public class ManageQuestionModel : IMapTo<Question>, IMapFrom<Question>, IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstraints.TitleMinLength)]
        [MaxLength(ModelConstraints.TitleMaxLength)]
        public string Title { get; set; }

        [MinLength(ModelConstraints.DescriptionMinLength)]
        [MaxLength(ModelConstraints.DescriptionMaxLength)]
        public string ResultDescription { get; set; }

        public virtual ICollection<ManageAnswerModel> Answers { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            bool hasCorrectAnswer = this.Answers.Any(a => a.IsCorrect);

            if (!hasCorrectAnswer)
            {
                yield return new ValidationResult("A question must have a correct answer");
            }
        }
    }
}
