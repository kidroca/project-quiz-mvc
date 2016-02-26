namespace QuizProjectMvc.Services.Data
{
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Protocols;
    using QuizProjectMvc.Data.Models;

    public class UsersService : IUsersService
    {
        private UserManager<User> manager;

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
    }
}