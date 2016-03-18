namespace QuizProjectMvc.Services.Data.Protocols
{
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Models;
    using Models.Account;
    using QuizProjectMvc.Data.Models;

    public interface IUsersService
    {
        User ById(string id);

        IQueryable<User> AllUsers();

        IdentityResult Update(BasicAccountInfoViewModel model, string userId);

        int GetTotalPages(int pageSize);

        IQueryable<User> GetPage(Pager pager);

        int GetMaxCreatedQuizzesCount();
    }
}
