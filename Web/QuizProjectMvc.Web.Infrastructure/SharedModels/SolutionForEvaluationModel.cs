namespace QuizProjectMvc.Web.Infrastructure.SharedModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    // Todo: Add custom validation - 1 selected answer per question
    public class SolutionForEvaluationModel
    {
        [Required]
        public string ForQuizId { get; set; }

        public IList<SelectedAnswerModel> Questions { get; set; }
    }
}
