namespace QuizProjectMvc.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;
    using QuizProjectMvc.Common;

    public class Question : BaseModel<int>
    {
        private ICollection<Answer> answers;

        public Question()
        {
            this.answers = new HashSet<Answer>();
        }

        [Required]
        [MinLength(ModelConstraints.TitleMinLength)]
        [MaxLength(ModelConstraints.TitleMaxLength)]
        public string Title { get; set; }

        [MinLength(ModelConstraints.DescriptionMinLength)]
        [MaxLength(ModelConstraints.DescriptionMaxLength)]
        public string ResultDescription { get; set; }

        public virtual ICollection<Answer> Answers
        {
            get { return this.answers; }

            set { this.answers = value; }
        }

        public virtual Quiz Quiz { get; set; }

        public int QuizId { get; set; }
    }
}
