namespace QuizProjectMvc.Services.Data.Protocols
{
    using System.Linq;
    using QuizProjectMvc.Data.Models;

    public interface ICategoriesService
    {
        IQueryable<QuizCategory> GetAll();

        QuizCategory EnsureCategory(string name);

        IQueryable<QuizCategory> GetTop(int count);

        IQueryable<QuizCategory> FilterByPattern(string pattern, int count);

        QuizCategory GetById(int id);

        void Save();

        bool Delete(int id);

        void Create(QuizCategory category);
    }
}
