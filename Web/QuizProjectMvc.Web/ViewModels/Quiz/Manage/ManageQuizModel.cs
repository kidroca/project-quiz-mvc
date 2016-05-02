namespace QuizProjectMvc.Web.ViewModels.Quiz.Manage
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common;

    public abstract class ManageQuizModel : IValidatableObject
    {
        protected ManageQuizModel()
        {
            this.Questions = new List<ManageQuestionModel>();
        }

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

        public bool ShuffleAnswers { get; set; }

        [Range(ModelConstraints.MinQuestionsCount, int.MaxValue)]
        public int NumberOfQuestions { get; set; }

        [Required]
        public QuizCategoryViewModel Category { get; set; }

        [Required]
        public ICollection<ManageQuestionModel> Questions { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Questions == null ||
                this.Questions.Count < ModelConstraints.MinQuestionsCount)
            {
                yield return new ValidationResult(
                    $"Question count must be at least {ModelConstraints.MinQuestionsCount}");
            }
        }
    }
}
