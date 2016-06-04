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
        private readonly IDbRepository<QuizCategory> categories;

        public QuizzesRankingService(IDbRepository<Quiz> quizzes, IDbRepository<QuizCategory> categories)
        {
            this.quizzes = quizzes;
            this.categories = categories;
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
            var result = this.OrderBySolutions(this.quizzes.All(), range);
            return result;
        }

        public IOrderedQueryable<QuizCategory> GetCategoriesOrderedBySolutions(DateRange range)
        {
            var result = this.OrderBySolutions(this.categories.All(), range);
            return result;
        }

        public IOrderedQueryable<Quiz> OrderBySolutions(IQueryable<Quiz> quizzesQuery, DateRange range)
        {
            var result = quizzesQuery
                .OrderByDescending(q => q.Solutions.Count(
                    s => range.From <= s.CreatedOn && s.CreatedOn <= range.To))
                .ThenByDescending(q => q.Solutions.Count)
                .ThenBy(q => q.CreatedOn);

            return result;
        }

        public IOrderedQueryable<QuizCategory> OrderBySolutions(IQueryable<QuizCategory> categoriesQuery, DateRange range)
        {
            var result = categoriesQuery
                .OrderByDescending(
                    c =>
                        c.Quizzes.Sum(q => q.Solutions.Count(s => range.From <= s.CreatedOn && s.CreatedOn <= range.To)))
                .ThenByDescending(c => c.Quizzes.Sum(q => q.Solutions.Count))
                .ThenBy(c => c.CreatedOn);

            return result;
        }
    }
}
