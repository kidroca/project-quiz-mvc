namespace QuizProjectMvc.Web.ViewModels.Quiz
{
    using Data.Models;
    using Infrastructure.Mapping;

    // Todo: Add Constraints
    public class QuizCategoryViewModel : IMapFrom<QuizCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string AvatarUrl { get; set; }

        // Perhaps add Count property
    }
}
