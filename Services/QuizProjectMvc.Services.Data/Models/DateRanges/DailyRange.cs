namespace QuizProjectMvc.Services.Data.Models.DateRanges
{
    using System;

    public class DailyRange : DateRange
    {
        public DailyRange()
            : base(DateTime.Today, DateTime.Today.AddHours(24).AddSeconds(-1))
        {
        }

        public override string RangeName => "Daily";
    }
}
