namespace QuizProjectMvc.Services.Data.Models.Evaluation
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using QuizProjectMvc.Data.Models;

    public class QuizEvaluationResult : IQuizEvaluationResult
    {
        public QuizEvaluationResult(Quiz quiz)
        {
            this.ForQuizId = quiz.Id;
            this.Title = quiz.Title;
            this.QuestionResults = new HashSet<IQuestionResult>();
        }

        public int ForQuizId { get; }

        public string Title { get; }

        public ICollection<IQuestionResult> QuestionResults { get; }

        public int TotalQuestions => this.QuestionResults.Count;

        public double GetSuccessPercentage()
        {
            var correctlyAnsweredCount = (double)this.QuestionResults.Count(q => q.AnsweredCorrectly);
            return (correctlyAnsweredCount / this.TotalQuestions) * 100;
        }
    }
}
