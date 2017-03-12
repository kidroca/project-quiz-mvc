namespace QuizProjectMvc.Services.Data.Protocols
{
    using Models.Evaluation;
    using Models.Evaluation.Contracts;
    using QuizProjectMvc.Data.Models;

    public interface IQuizzesEvalService
    {
        IQuizEvaluationResult Evaluate(QuizSolution solution);

        IQuizEvaluationResult Evaluate(int solutionId);

        QuizSolution SaveSolution(SolutionForEvaluationModel quizSolution, string userId);
    }
}
