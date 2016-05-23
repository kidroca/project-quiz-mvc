namespace QuizProjectMvc.Services.Data.Models.DateRanges
{
    using System;

    public class AllTime : DateRange
    {
        public AllTime()
        {
            this.From = new DateTime(2016, 1, 1);
            this.To = DateTime.Now;
        }

        public override string RangeName => "All";
    }
}
