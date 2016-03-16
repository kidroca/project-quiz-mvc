namespace QuizProjectMvc.Web.ViewModels.Quiz
{
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Data.Models;
    using Infrastructure.Mapping;

    // Todo: Add Constraints
    public class QuizCategoryViewModel : IMapFrom<QuizCategory>, IMapTo<QuizCategory>
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstraints.NameMinLength)]
        [MaxLength(ModelConstraints.NameMaxLength)]
        public string Name { get; set; }

        [MinLength(ModelConstraints.UrlMinLength)]
        [MaxLength(ModelConstraints.UrlMaxLength)]
        public string AvatarUrl { get; set; }
    }
}
