namespace QuizProjectMvc.Web.Infrastructure.SharedModels
{
    using System.Collections.Generic;

    public class QuizEvaluationResult
    {
        public int ForQuizId { get; set; }

        public string Title { get; set; }

        public IList<QuestionResultModel> CorrectlyAnswered { get; set; }

        public IList<QuestionResultModel> IncorrectlyAnswered { get; set; }

        public int TotalQuestions
        {
            get { return this.CorrectlyAnswered.Count + this.IncorrectlyAnswered.Count; }
        }

        public double GetSuccessPercentage()
        {
            return ((double)this.CorrectlyAnswered.Count / this.TotalQuestions) * 100;
        }
    }
}
