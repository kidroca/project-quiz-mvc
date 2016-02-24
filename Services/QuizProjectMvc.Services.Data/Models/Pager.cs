namespace QuizProjectMvc.Services.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Pager
    {
        public const int DefaultPageSize = 10;

        public Pager()
        {
            this.Page = 1;
            this.PageSize = DefaultPageSize;
        }

        [Range(0, int.MaxValue)]
        public int TotalPages { get; set; }

        [Range(1, int.MaxValue)]
        public int Page { get; set; }

        [Range(1, int.MaxValue)]
        public int PageSize { get; set; }
    }
}
