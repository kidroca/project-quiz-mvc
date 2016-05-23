namespace QuizProjectMvc.Services.Data.Models.DateRanges
{
    using System;

    public class DateRange
    {
        public DateRange(DateTime from, DateTime to)
        {
            this.From = from;
            this.To = to;
        }

        protected DateRange()
        {
        }

        /// <summary>
        /// Gets or sets the name for the <see cref="DateRange"/> period
        /// <example>Weekly, Monthly etc...</example>
        /// </summary>
        /// <value>
        /// The name for the <see cref="DateRange"/> period
        /// <example>Weekly, Monthly etc...</example>
        /// </value>
        public virtual string RangeName { get; set; }

        public DateTime From { get; protected set; }

        public DateTime To { get; protected set; }

        public override int GetHashCode()
        {
            int result = this.From.GetHashCode() + this.GetType().Name.GetHashCode();
            return result;
        }

        public override bool Equals(object obj)
        {
            var other = obj as DateRange;
            if (other == null)
            {
                return false;
            }

            return this.From.Equals(other.From) && this.To.Equals(other.To);
        }
    }
}
