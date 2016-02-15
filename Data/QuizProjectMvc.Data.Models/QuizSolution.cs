namespace QuizProjectMvc.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;

    public class Solution : BaseModel<int>
    {
        private ICollection<Answer> selectedAnswers;

        public Solution()
        {
            this.selectedAnswers = new HashSet<Answer>();
        }

        public int ForQuizId { get; set; }

        public virtual Quiz ForQuiz { get; set; }

        [Required]
        public string ByUserId { get; set; }

        public virtual User ByUser { get; set; }

        public virtual ICollection<Answer> SelectedAnswers
        {
            get { return this.selectedAnswers; }
            set { this.selectedAnswers = value; }
        }
    }
}