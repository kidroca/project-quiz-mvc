namespace QuizProjectMvc.Web.ViewModels.Home
{
    using QuizProjectMvc.Data.Models;
    using QuizProjectMvc.Web.Infrastructure.Mapping;

    public class JokeCategoryViewModel : IMapFrom<JokeCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
