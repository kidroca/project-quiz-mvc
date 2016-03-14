namespace QuizProjectMvc.Services.Data
{
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using Exceptions;
    using Protocols;
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

            // Todo: Proper category creation with all properties (maybe only by admin or certain trusted users)
            category = new QuizCategory { Name = name };
            this.categories.Add(category);
            this.Save();
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

        public QuizCategory GetById(int id)
        {
            return this.categories.GetById(id);
        }

        public void Save()
        {
            this.categories.Save();
        }

        public bool Delete(int id)
        {
            var category = this.categories.AllWithDeleted()
                .FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return false;
            }

            if (category.Quizzes.Count > 0)
            {
                throw new CategoryManagementException("The category cannot be deleted because it contains quizzes!");
            }

            this.categories.HardDelete(category);
            this.Save();
            return true;
        }

        public void Create(QuizCategory category)
        {
            try
            {
                this.categories.Add(category);
                this.Save();
            }
            catch (DbUpdateException ex)
            {
                throw new CategoryManagementException(ex.Message, ex);
            }
        }

        public IQueryable<QuizCategory> GetAll()
        {
            return this.categories.AllWithDeleted().OrderBy(x => x.Name);
        }
    }
}
