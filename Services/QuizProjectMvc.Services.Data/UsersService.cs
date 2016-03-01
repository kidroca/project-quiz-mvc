﻿namespace QuizProjectMvc.Services.Data
{
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Models.Account;
    using Protocols;
    using QuizProjectMvc.Data.Models;

    public class UsersService : IUsersService
    {
        private readonly UserManager<User> manager;

        public UsersService(UserManager<User> manager)
        {
            this.manager = manager;
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