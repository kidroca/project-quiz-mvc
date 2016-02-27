namespace QuizProjectMvc.Web.ViewModels.Quiz.Manage
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Data.Models;
    using Infrastructure.Mapping;

    public class ManageQuestionModel : IMapTo<Question>, IMapFrom<Question>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstraints.TitleMinLength)]
        [MaxLength(ModelConstraints.TitleMaxLength)]
        public string Title { get; set; }

        [MinLength(ModelConstraints.DescriptionMinLength)]
        [MaxLength(ModelConstraints.DescriptionMaxLength)]
        public string ResultDescription { get; set; }

        public virtual ICollection<ManageAnswerModel> Answers { get; set; }
    }
}
