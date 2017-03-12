namespace QuizProjectMvc.Services.Data.Models.Evaluation.Contracts
{
    using System.Collections.Generic;

    public interface IQuestionResult
    {
        string Question { get; }

        int QuestionId { get; }

        int CorrentAnswer { get; }

        int SelectedAnswer { get; }

        bool AnsweredCorrectly { get; }

        IList<IAvailableAnswer> Answers { get; }
    }
}
