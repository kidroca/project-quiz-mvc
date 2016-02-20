namespace QuizProjectMvc.Services.Data
{
    using System.Linq;
    using Models.Evaluation;
    using Models.Search;
    using QuizProjectMvc.Data.Models;

    public interface IQuizesService
    {
        IQueryable<Quiz> GetRandomQuizzes(int count);

        QuizEvaluationResult EvaluateSolution(QuizSolution quizSolution);

        QuizSolution SaveSolution(SolutionForEvaluationModel quizSolution, Quiz quiz, string userId);

        Quiz GetById(string id);

        IQueryable<Quiz> SearchQuizzes(QuizSearchModel queryParameters);
    }
}
