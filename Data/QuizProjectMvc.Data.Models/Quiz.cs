namespace QuizProjectMvc.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Common.Models;
    using QuizProjectMvc.Common;

    public class Quiz : BaseModel<int>
    {
        private ICollection<QuizRating> rattings;
        private ICollection<Question> questions;
        private ICollection<QuizSolution> solutions;

        public Quiz()
        {
            this.rattings = new HashSet<QuizRating>();
            this.questions = new HashSet<Question>();
            this.solutions = new HashSet<QuizSolution>();
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

        public virtual ICollection<QuizRating> Ratings
        {
            get { return this.rattings; }

            set { this.rattings = value; }
        }

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
    }
}
