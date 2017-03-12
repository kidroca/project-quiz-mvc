namespace QuizProjectMvc.Services.Data.Protocols
{
    using System;
    using Models.Evaluation;
    using Models.Evaluation.Contracts;
    using QuizProjectMvc.Data.Models;

    public interface IQuizzesEvalService
    {
        [Obsolete("Please use the new Evaluate method")]
        QuizEvaluationResult2 EvaluateSolution(QuizSolution quizSolution);

        [Obsolete("Please use the new Evaluate method")]
        QuizEvaluationResult2 EvaluateSolution(int solutionId);

        IQuizEvaluationResult Evaluate(QuizSolution solution);

        IQuizEvaluationResult Evaluate(int solutionId);

        QuizSolution SaveSolution(SolutionForEvaluationModel quizSolution, string userId);
    }
}
