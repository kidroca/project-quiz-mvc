namespace QuizProjectMvc.Services.Data.Protocols
{
    using System.Linq;
    using Models.DateRanges;
    using QuizProjectMvc.Data.Models;

    public interface IQuizzesRankingService
    {
        IQueryable<Quiz> QuizAllTimeRanking();

        int MostSolutionsBetween(DateRange range);

        IOrderedQueryable<Quiz> GetQuizzesOrderedBySolutions(DateRange range);

        IOrderedQueryable<Quiz> OrderByRanking(IQueryable<Quiz> quizzes, DateRange range);
    }
}
