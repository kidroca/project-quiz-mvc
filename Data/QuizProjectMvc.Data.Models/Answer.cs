namespace QuizProjectMvc.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;
    using QuizProjectMvc.Common;

    public class Answer : IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        public int ForQuestionId { get; set; }

        public virtual Question ForQuestion { get; set; }

        [Required]
        [MinLength(ModelConstraints.TitleMinLength)]
        [MaxLength(ModelConstraints.TitleMaxLength)]
        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
