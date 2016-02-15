namespace QuizProjectMvc.Web.ViewModels.Home
{
    using QuizProjectMvc.Data.Models;
    using QuizProjectMvc.Web.Infrastructure.Mapping;

    public class QuizCategoryViewModel : IMapFrom<QuizCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string AvatarUrl { get; set; }

        // Perhaps add Count property
    }
}
