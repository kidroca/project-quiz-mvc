namespace QuizProjectMvc.Services.Data.Models.Evaluation.Contracts
{
    public interface IAvailableAnswer
    {
        int Id { get; }

        string Text { get; }

        bool IsCorrect { get; }
    }
}
