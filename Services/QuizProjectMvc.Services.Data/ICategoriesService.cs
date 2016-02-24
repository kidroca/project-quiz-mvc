namespace QuizProjectMvc.Services.Data
{
    using System.Linq;

    using QuizProjectMvc.Data.Models;

    public interface ICategoriesService
    {
        IQueryable<QuizCategory> GetAll();

        QuizCategory EnsureCategory(string name);

        IQueryable<QuizCategory> GetTop(int count);
    }
}
