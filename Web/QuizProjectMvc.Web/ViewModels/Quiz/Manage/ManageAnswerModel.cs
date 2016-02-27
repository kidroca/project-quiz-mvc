namespace QuizProjectMvc.Web.ViewModels.Quiz.Manage
{
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Data.Models;
    using Infrastructure.Mapping;

    public class ManageAnswerModel : IMapTo<Answer>, IMapFrom<Answer>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstraints.TitleMinLength)]
        [MaxLength(ModelConstraints.TitleMaxLength)]
        public string Text { get; set; }

        public bool IsCorrect { get; set; }
    }
}
