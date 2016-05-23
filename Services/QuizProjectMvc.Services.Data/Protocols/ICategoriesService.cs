namespace QuizProjectMvc.Services.Data.Protocols
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using QuizProjectMvc.Data.Models;

    public interface ICategoriesService
    {
        IQueryable<QuizCategory> GetAll();

        IQueryable<QuizCategory> GetTop(int count);

        IQueryable<QuizCategory> FilterByPattern(string pattern);

        QuizCategory GetById(int id);

        void Save();

        bool Delete(int id);

        void Create(QuizCategory category);

        IEnumerable<SelectListItem> GetCategoryOptions();
    }
}
