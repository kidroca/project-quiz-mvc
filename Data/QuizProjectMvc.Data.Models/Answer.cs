namespace QuizProjectMvc.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Models;
    using QuizProjectMvc.Common;

    public class Answer : BaseModel<int>
    {
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        [Required]
        [MinLength(ModelConstraints.TitleMinLength)]
        [MaxLength(ModelConstraints.TitleMaxLength)]
        public string Text { get; set; }

        public bool IsCorrect { get; set; }
    }
}