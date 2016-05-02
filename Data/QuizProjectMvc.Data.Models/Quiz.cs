namespace QuizProjectMvc.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Common.Models;
    using QuizProjectMvc.Common;

    public class Quiz : BaseModel<int>
    {
        private ICollection<Question> questions;
        private ICollection<QuizSolution> solutions;
        private ICollection<Comment> comments;

        public Quiz()
        {
            this.questions = new HashSet<Question>();
            this.solutions = new HashSet<QuizSolution>();
            this.comments = new HashSet<Comment>();
        }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(ModelConstraints.TitleMinLength)]
        [MaxLength(ModelConstraints.TitleMaxLength)]
        public string Title { get; set; }

        public int CategoryId { get; set; }

        public virtual QuizCategory Category { get; set; }

        [MinLength(ModelConstraints.DescriptionMinLength)]
        [MaxLength(ModelConstraints.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string CreatedById { get; set; }

        public virtual User CreatedBy { get; set; }

        public bool IsPrivate { get; set; }

        public bool ShuffleAnswers { get; set; }

        [Range(ModelConstraints.MinQuestionsCount, int.MaxValue)]
        public int NumberOfQuestions { get; set; }

        public virtual ICollection<Question> Questions
        {
            get { return this.questions; }

            set { this.questions = value; }
        }

        public virtual ICollection<QuizSolution> Solutions
        {
            get { return this.solutions; }
            set { this.solutions = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}
