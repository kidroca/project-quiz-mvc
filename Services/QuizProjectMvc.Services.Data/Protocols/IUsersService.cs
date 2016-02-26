namespace QuizProjectMvc.Services.Data.Protocols
{
    using System.Linq;
    using QuizProjectMvc.Data.Models;

    public interface IUsersService
    {
        User ById(string id);

        IQueryable<User> AllUsers();
    }
}
