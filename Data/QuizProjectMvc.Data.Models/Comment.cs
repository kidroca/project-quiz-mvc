namespace QuizProjectMvc.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Models;
    using QuizProjectMvc.Common;

    public class Comment : BaseModel<int>
    {
        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        [Required]
        [MinLength(ModelConstraints.DescriptionMinLength)]
        [MaxLength(ModelConstraints.DescriptionMaxLength)]
        public string Content { get; set; }

        public int QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }
    }
}
