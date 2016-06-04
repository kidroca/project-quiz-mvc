namespace QuizProjectMvc.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Services.Data.Models.DateRanges;
    using Services.Data.Protocols;
    using ViewModels.PageModels;
    using ViewModels.Quiz.Ranking;

    public class RankingController : BaseQuizController
    {
        public RankingController(IQuizzesRankingService ranking)
            : base(ranking)
        {
        }

        public ActionResult Classification()
        {
            var pageModel = this.GetPageModelFromRange(new WeeklyRange());

            return this.View(pageModel);
        }

        public ActionResult Daily()
        {
            var pageModel = this.GetPageModelFromRange(new DailyRange());

            return this.PartialView("_RankingResultsPartial", pageModel);
        }

        public ActionResult Weekly()
        {
            var pageModel = this.GetPageModelFromRange(new WeeklyRange());

            return this.PartialView("_RankingResultsPartial", pageModel);
        }

        public ActionResult Monthly()
        {
            var pageModel = this.GetPageModelFromRange(new MonthlyRange());

            return this.PartialView("_RankingResultsPartial", pageModel);
        }

        public ActionResult AllTime()
        {
            var pageModel = this.GetPageModelFromRange(new AllTime());

            return this.PartialView("_RankingResultsPartial", pageModel);
        }

        private RankingPageViewModel GetPageModelFromRange(DateRange range)
        {
            var pageModel = new RankingPageViewModel(this.GetMaxSolutions(range), range)
            {
                Quizzes = this.Ranking
                    .GetQuizzesOrderedBySolutions(range)
                    .To<QuizRankedBySolutions>()
                    .ToList(),
                Categories = this.Ranking
                    .GetCategoriesOrderedBySolutions(range)
                    .To<CategoryRankedBySolutions>()
                    .Take(10)
                    .ToList()
            };

            return pageModel;
        }
    }
}
