namespace QuizProjectMvc.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Common.Models;
    using QuizProjectMvc.Common;

    public class QuizCategory : BaseModel<int>
    {
        private ICollection<Quiz> quizzes;

        public QuizCategory()
        {
            this.quizzes = new HashSet<Quiz>();
            this.AvatarUrl = GlobalConstants.DefaultCategoryAvatarUrl;
        }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(ModelConstraints.NameMinLength)]
        [MaxLength(ModelConstraints.NameMaxLength)]
        public string Name { get; set; }

        [MinLength(ModelConstraints.UrlMinLength)]
        [MaxLength(ModelConstraints.UrlMaxLength)]
        public string AvatarUrl { get; set; }

        public virtual ICollection<Quiz> Quizzes
        {
            get { return this.quizzes; }
            set { this.quizzes = value; }
        }
    }
}
