namespace QuizProjectMvc.Data.Models
{
    using System.Collections.Generic;
    using Common.Models;

    public class QuizSolution : BaseModel<int>
    {
        private ICollection<Answer> selectedAnswers;

        public QuizSolution()
        {
            this.selectedAnswers = new HashSet<Answer>();
        }

        public int ForQuizId { get; set; }

        public virtual Quiz ForQuiz { get; set; }

        public string ByUserId { get; set; }

        public virtual User ByUser { get; set; }

        public virtual ICollection<Answer> SelectedAnswers
        {
            get { return this.selectedAnswers; }
            set { this.selectedAnswers = value; }
        }
    }
}
