namespace QuizProjectMvc.Services.Data.Protocols
{
    using System.Linq;
    using Models;
    using Models.Search;
    using QuizProjectMvc.Data.Models;

    public interface IQuizzesGeneralService
    {
        IQueryable<Quiz> GetRandomQuizzes(int count);

        Quiz GetById(string id);

        Quiz GetById(int id);

        IQueryable<Quiz> SearchQuizzes(QuizSearchModel queryParameters);

        IQueryable<Quiz> GetPage(Pager pager);

        void Add(Quiz quiz);

        void Save();

        int GetTotalPages(string categoryName, int pageSize);

        void Delete(Quiz quiz);

        int GetMaxSolutionsCount();
    }
}
