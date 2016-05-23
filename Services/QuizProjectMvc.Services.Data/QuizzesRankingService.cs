namespace QuizProjectMvc.Services.Data
{
    using System.Linq;
    using Models.DateRanges;
    using Protocols;
    using QuizProjectMvc.Data.Common;
    using QuizProjectMvc.Data.Models;

    public class QuizzesRankingService : IQuizzesRankingService
    {
        private readonly IDbRepository<Quiz> quizzes;

        public QuizzesRankingService(IDbRepository<Quiz> quizzes)
        {
            this.quizzes = quizzes;
        }

        public IQueryable<Quiz> QuizAllTimeRanking()
        {
            var result = this.quizzes.All()
                .OrderByDescending(q => q.Solutions.Count)
                .ThenBy(q => q.CreatedOn);

            return result;
        }

        public int MostSolutionsBetween(DateRange range)
        {
            var result = this.quizzes.All()
                .Max(q => q.Solutions.Count(
                    s => range.From <= s.CreatedOn && s.CreatedOn <= range.To));

            return result;
        }

        public IOrderedQueryable<Quiz> GetQuizzesOrderedBySolutions(DateRange range)
        {
            var result = this.OrderByRanking(this.quizzes.All(), range);
            return result;
        }

        public IOrderedQueryable<Quiz> OrderByRanking(IQueryable<Quiz> quizzes, DateRange range)
        {
            var result = quizzes
                .OrderByDescending(q => q.Solutions.Count(
                    s => range.From <= s.CreatedOn && s.CreatedOn <= range.To))
                .ThenByDescending(q => q.Solutions.Count)
                .ThenBy(q => q.CreatedOn);

            return result;
        }
    }
}
