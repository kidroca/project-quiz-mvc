namespace QuizProjectMvc.Services.Data.Protocols
{
    using Models.Evaluation;
    using QuizProjectMvc.Data.Models;

    public interface IQuizzesEvalService
    {
        QuizEvaluationResult EvaluateSolution(QuizSolution quizSolution);

        QuizEvaluationResult EvaluateSolution(int solutionId);

        QuizSolution SaveSolution(SolutionForEvaluationModel quizSolution, string userId);
    }
}
