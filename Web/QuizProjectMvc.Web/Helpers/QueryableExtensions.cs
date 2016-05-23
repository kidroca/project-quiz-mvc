namespace QuizProjectMvc.Web.Helpers
{
    using System.Linq;
    using Services.Data.Models;

    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, Pager pager)
        {
            var result = query
                .Skip(pager.GetSkipCount())
                .Take(pager.PageSize);

            return result;
        }
    }
}
