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

        IOrderedQueryable<QuizCategory> GetCategoriesOrderedBySolutions(DateRange range);

        IOrderedQueryable<Quiz> OrderBySolutions(IQueryable<Quiz> quizzesQuery, DateRange range);

        IOrderedQueryable<QuizCategory> OrderBySolutions(
            IQueryable<QuizCategory> categoriesQuery,
            DateRange range);
    }
}
