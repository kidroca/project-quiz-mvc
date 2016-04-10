namespace QuizProjectMvc.Services.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Pager
    {
        public const int DefaultPageSize = 3;
        public const int PaginationNumberOfDisplayedPages = 5;

        public Pager()
        {
            this.Page = 1;
            this.PageSize = DefaultPageSize;
        }

        public string CategoryName { get; set; }

        [Range(0, int.MaxValue)]
        public int TotalPages { get; set; }

        [Range(1, int.MaxValue)]
        public int Page { get; set; }

        [Range(1, int.MaxValue)]
        public int PageSize { get; set; }

        public int FirstVisiblePage()
        {
            int first = this.Page - (PaginationNumberOfDisplayedPages / 2);
            if (first < 1)
            {
                first = 1;
            }

            return first;
        }

        public int LastVisiblePage()
        {
            int last = this.FirstVisiblePage() + PaginationNumberOfDisplayedPages;
            if (last > this.TotalPages)
            {
                last = this.TotalPages;
            }

            return last;
        }

        public int GetSkipCount()
        {
            return (this.Page - 1) * this.PageSize;
        }
    }
}
