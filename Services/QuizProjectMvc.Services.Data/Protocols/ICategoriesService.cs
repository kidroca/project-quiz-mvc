namespace QuizProjectMvc.Services.Data.Protocols
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using QuizProjectMvc.Data.Models;

    public interface ICategoriesService
    {
        IQueryable<QuizCategory> GetAll();

        /// <summary>
        /// Returns the top <see cref="count"/> of solutions ordered by solutions count descending
        /// </summary>
        /// <param name="count">The number of solutions to return</param>
        /// <returns>Returns a queryable that can be additionally filtered before materializing</returns>
        IQueryable<QuizCategory> GetTop(int count);

        IQueryable<QuizCategory> FilterByPattern(string pattern);

        QuizCategory GetById(int id);

        void Save();

        bool Delete(int id);

        void Create(QuizCategory category);

        IEnumerable<SelectListItem> GetCategoryOptions();
    }
}
