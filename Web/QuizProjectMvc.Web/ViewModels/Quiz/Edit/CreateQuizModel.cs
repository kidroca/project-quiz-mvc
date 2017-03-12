namespace QuizProjectMvc.Web.ViewModels.Quiz.Create
{
    using Data.Models;
    using Infrastructure.Mapping;
    using Manage;

    public class CreateQuizModel : ManageQuizModel, IMapTo<Quiz>
    {
    }
}
