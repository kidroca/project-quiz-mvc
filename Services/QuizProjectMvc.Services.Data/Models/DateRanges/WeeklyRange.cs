namespace QuizProjectMvc.Services.Data.Models.DateRanges
{
    using System;

    public class WeeklyRange : DateRange
    {
        public WeeklyRange()
        {
            this.From = this.StartOfWeek();
            this.To = this.From.AddDays(7).AddSeconds(-1);
        }

        public override string RangeName => "Weekly";

        private DateTime StartOfWeek()
        {
            System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
            DayOfWeek fdow = ci.DateTimeFormat.FirstDayOfWeek;
            return DateTime.Today.AddDays(-(DateTime.Today.DayOfWeek - fdow));
        }
    }
}
