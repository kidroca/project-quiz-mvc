namespace QuizProjectMvc.Services.Data.Exceptions
{
    using System;

    public class QuizEvaluationException : ApplicationException
    {
        public QuizEvaluationException(string message)
            : base(message)
        {
        }

        public QuizEvaluationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
