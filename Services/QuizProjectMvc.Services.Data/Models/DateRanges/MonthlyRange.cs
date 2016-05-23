namespace QuizProjectMvc.Services.Data.Models.DateRanges
{
    using System;

    public class MonthlyRange : DateRange
    {
        public MonthlyRange()
        {
            this.From = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.To = this.From.AddMonths(1).AddSeconds(-1);
        }

        public override string RangeName => "Monthly";
    }
}
