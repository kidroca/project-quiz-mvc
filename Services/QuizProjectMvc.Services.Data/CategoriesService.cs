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

        public IQueryable<QuizCategory> GetTop(int count)
        {
            return this.categories.All()
                .OrderByDescending(c => c.Quizzes.Count)
                .Take(10);
        }

        public IQueryable<QuizCategory> FilterByPattern(string pattern, int count)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                return this.GetAll().Take(count);
            }

            return this.GetAll()
                .Where(c => c.Name.ToLower().Contains(pattern.ToLower()))
                .Take(count);
        }

        public IQueryable<QuizCategory> GetAll()
        {
            return this.categories.All().OrderBy(x => x.Name);
        }
    }
}
