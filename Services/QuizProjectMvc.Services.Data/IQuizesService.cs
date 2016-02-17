namespace QuizProjectMvc.Services.Data
{
    using System.Linq;

    using QuizProjectMvc.Data.Models;
    using QuizProjectMvc.Web.Infrastructure.SharedModels.Evaluation;
    using QuizProjectMvc.Web.Infrastructure.SharedModels.Search;

    public interface IQuizesService
    {
        IQueryable<Quiz> GetRandomQuizzes(int count);

        QuizEvaluationResult EvaluateSolution(QuizSolution quizSolution);

        QuizSolution SaveSolution(SolutionForEvaluationModel quizSolution, Quiz quiz, string userId);

        Quiz GetById(string id);

        IQueryable<Quiz> SearchQuizzes(QuizSearchModel queryParameters);
    }
}
