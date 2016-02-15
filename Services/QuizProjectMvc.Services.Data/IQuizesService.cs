namespace QuizProjectMvc.Services.Data
{
    using System.Linq;

    using QuizProjectMvc.Data.Models;

    public interface IQuizesService
    {
        IQueryable<Quiz> GetRandomQuizzes(int count);

        Quiz GetById(string id);
    }
}
