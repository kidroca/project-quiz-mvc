namespace QuizProjectMvc.Services.Data
{
    using System.Linq;

    using QuizProjectMvc.Data.Common;
    using QuizProjectMvc.Data.Models;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDbRepository<QuizCategory> categories;

        public CategoriesService(IDbRepository<QuizCategory> categories)
        {
            this.categories = categories;
        }

        public QuizCategory EnsureCategory(string name)
        {
            var category = this.categories.All().FirstOrDefault(x => x.Name == name);
            if (category != null)
            {
                return category;
            }

            // Todo: Proper category creation with all properties (maybe only by admin)
            category = new QuizCategory { Name = name };
            this.categories.Add(category);
            this.categories.Save();
            return category;
        }

        public IQueryable<QuizCategory> GetAll()
        {
            return this.categories.All().OrderBy(x => x.Name);
        }
    }
}
