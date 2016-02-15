namespace QuizProjectMvc.Services.Data
{
    using System;
    using System.Linq;

    using QuizProjectMvc.Data.Common;
    using QuizProjectMvc.Data.Models;
    using Web;

    public class QuizesService : IQuizesService
    {
        private readonly IDbRepository<Quiz> quizzes;
        private readonly IIdentifierProvider identifierProvider;

        public QuizesService(IDbRepository<Quiz> quizzes, IIdentifierProvider identifierProvider)
        {
            this.quizzes = quizzes;
            this.identifierProvider = identifierProvider;
        }

        public Quiz GetById(string id)
        {
            var intId = this.identifierProvider.DecodeId(id);
            var quiz = this.quizzes.GetById(intId);
            return quiz;
        }

        public IQueryable<Quiz> GetRandomQuizzes(int count)
        {
            return this.quizzes.All().OrderBy(x => Guid.NewGuid()).Take(count);
        }
    }
}
