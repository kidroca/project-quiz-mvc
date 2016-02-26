namespace QuizProjectMvc.Web.ViewModels.ProfileInformation
{
    using Data.Models;
    using Infrastructure.Mapping;

    public class CreatedQuizInfo : IMapFrom<Quiz>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
