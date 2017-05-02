namespace QuizProjectMvc.Services.Data.Models.Evaluation.Contracts
{
    using System.Collections.Generic;

    public interface IQuizEvaluationResult
    {
        int ForQuizId { get; }

        string Title { get; }

        ICollection<IQuestionResult> QuestionResults { get; }

        int TotalQuestions { get; }

        double GetSuccessPercentage();
    }
}
