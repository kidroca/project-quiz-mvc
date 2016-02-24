namespace QuizProjectMvc.Web.ViewModels.Quiz.Create
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Data.Models;
    using Infrastructure.Mapping;

    public class CreateQuestionModel : IMapTo<Question>
    {
        [Required]
        [MinLength(ModelConstraints.TitleMinLength)]
        [MaxLength(ModelConstraints.TitleMaxLength)]
        public string Title { get; set; }

        public virtual ICollection<CreateAnswerModel> Answers { get; set; }
    }
}
