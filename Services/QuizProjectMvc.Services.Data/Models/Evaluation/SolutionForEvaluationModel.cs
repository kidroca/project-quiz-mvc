namespace QuizProjectMvc.Services.Data.Models.Evaluation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    // Todo: Add custom validation - 1 selected answer per question
    public class SolutionForEvaluationModel
    {
        public int ForQuizId { get; set; }

        [Required]
        public IList<int> SelectedAnswerIds { get; set; }
    }
}
