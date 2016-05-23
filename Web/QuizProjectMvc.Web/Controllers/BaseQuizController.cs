namespace QuizProjectMvc.Web.Controllers
{
    using Common;
    using Services.Data.Models.DateRanges;
    using Services.Data.Protocols;

    public abstract class BaseQuizController : BaseController
    {
        protected BaseQuizController(IQuizzesRankingService ranking)
        {
            this.Ranking = ranking;
        }

        protected IQuizzesRankingService Ranking { get; }

        protected int GetMaxSolutions(DateRange range)
        {
            var maxSolutions = this.Cache.Get(
                range.GetType().Name,
                () => this.Ranking.MostSolutionsBetween(range),
                CacheConstants.DurationMaxSolutions);

            return maxSolutions;
        }
    }
}
