namespace QuizProjectMvc.Services.Data.Exceptions
{
    using System;

    public class QuizCreationException : ApplicationException
    {
        public QuizCreationException(string message)
            : base(message)
        {
        }
    }
}
