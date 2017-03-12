namespace QuizProjectMvc.Services.Data.Models.Evaluation.Contracts
{
    using System.Collections.Generic;

    public interface IQuestionResult
    {
        string Title { get; }

        int Id { get; }

        int CorrentAnswerId { get; }

        int SelectedAnswerId { get; set; }

        string ResultDescription { get; }

        bool AnsweredCorrectly { get; }

        IList<IAvailableAnswer> Answers { get; }
    }
}
