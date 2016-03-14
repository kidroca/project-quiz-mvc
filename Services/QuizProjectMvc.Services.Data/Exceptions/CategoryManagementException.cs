namespace QuizProjectMvc.Services.Data.Exceptions
{
    using System;

    public class CategoryManagementException : ApplicationException
    {
        public CategoryManagementException(string message)
            : base(message)
        {
        }

        public CategoryManagementException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
