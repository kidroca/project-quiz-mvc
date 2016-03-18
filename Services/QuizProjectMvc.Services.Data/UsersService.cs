namespace QuizProjectMvc.Services.Data
{
    using System;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Models;
    using Models.Account;
    using Protocols;
    using QuizProjectMvc.Data.Models;
    using Web;

    public class UsersService : IUsersService
    {
        private readonly UserManager<User> manager;
        private readonly ICacheService cache;

        public UsersService(UserManager<User> manager, ICacheService cache)
        {
            this.manager = manager;
            this.cache = cache;
        }

        public User ById(string id)
        {
            return this.manager.FindById(id);
        }

        public IQueryable<User> AllUsers()
        {
            return this.manager.Users;
        }

        public IdentityResult Update(BasicAccountInfoViewModel model, string userId)
        {
            var user = this.ById(userId);
            this.UpdateUserCommonProperties(user, model);
            var result = this.manager.Update(user);

            return result;
        }

        public int GetTotalPages(int pageSize)
        {
            int usersCount = this.AllUsers().Count();
            int result = (int)Math.Ceiling(usersCount / (double)pageSize);

            return result;
        }

        public IQueryable<User> GetPage(Pager pager)
        {
            var result = this.AllUsers()
                .OrderBy(u => u.CreatedOn)
                .Skip(pager.GetSkipCount())
                .Take(pager.PageSize);

            return result;
        }

        public int GetMaxCreatedQuizzesCount()
        {
            var result = this.cache.Get(
                "MaxQuizzesCreated",
                () => this.AllUsers().Max(u => u.QuizzesCreated.Count),
                durationInSeconds: 5 * 60);

            return result;
        }

        private void UpdateUserCommonProperties(User user, BasicAccountInfoViewModel model)
        {
            user.AvatarUrl = model.AvatarUrl;
            user.Bio = model.Bio;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
        }
    }
}
