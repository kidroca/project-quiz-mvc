namespace QuizProjectMvc.Services.Data.Models.Evaluation.Contracts
{
    public interface IAvailableAnswer
    {
        int AnswerId { get; }

        string Text { get; }

        bool IsCorrect { get; }
    }
}
