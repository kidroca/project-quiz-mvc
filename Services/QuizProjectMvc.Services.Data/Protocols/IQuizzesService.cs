namespace QuizProjectMvc.Services.Data.Protocols
{
    using System.Linq;
    using Models;
    using Models.Evaluation;
    using Models.Search;
    using QuizProjectMvc.Data.Models;

    public interface IQuizzesService
    {
        IQueryable<Quiz> GetRandomQuizzes(int count);

        QuizEvaluationResult EvaluateSolution(QuizSolution quizSolution);

        QuizEvaluationResult EvaluateSolution(int solutionId);

        QuizSolution SaveSolution(SolutionForEvaluationModel quizSolution, string userId);

        Quiz GetById(string id);

        Quiz GetById(int id);

        IQueryable<Quiz> SearchQuizzes(QuizSearchModel queryParameters);

        IQueryable<Quiz> GetPage(Pager pager);

        void Add(Quiz quiz);

        void Save();

        int GetTotalPages(int pageSize);

        void Delete(Quiz quiz);
    }
}
