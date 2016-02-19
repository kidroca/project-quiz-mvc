namespace QuizProjectMvc.Services.Data.SharedModels.Evaluation
{
    public class QuestionResultModel
    {
        public string Question { get; set; }

        public string SelectedAnswer { get; set; }

        public string CorrectAnswer { get; set; }

        public string ResultDescription { get; set; }

        public bool IsCorrect { get; set; }
    }
}
