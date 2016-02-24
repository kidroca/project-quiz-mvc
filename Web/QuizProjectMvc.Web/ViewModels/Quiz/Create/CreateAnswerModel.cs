namespace QuizProjectMvc.Web.ViewModels.Quiz.Create
{
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Data.Models;
    using Infrastructure.Mapping;

    public class CreateAnswerModel : IMapTo<Answer>
    {
        [Required]
        [MinLength(ModelConstraints.TitleMinLength)]
        [MaxLength(ModelConstraints.TitleMaxLength)]
        public string Text { get; set; }

        public bool IsCorrect { get; set; }
    }
}
