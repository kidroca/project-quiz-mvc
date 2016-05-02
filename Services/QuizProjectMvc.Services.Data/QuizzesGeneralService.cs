namespace QuizProjectMvc.Services.Data
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Exceptions;
    using Models;
    using Models.Search;
    using Protocols;
    using QuizProjectMvc.Data.Common;
    using QuizProjectMvc.Data.Models;
    using Web;

    // Todo: use quizzes creation exception with save quiz
    public class QuizzesGeneralService : IQuizzesGeneralService
    {
        private readonly IIdentifierProvider identifierProvider;
        private readonly ICacheService cache;
        private readonly IDbRepository<Quiz> quizzes;

        public QuizzesGeneralService(
            IDbRepository<Quiz> quizzes,
            IIdentifierProvider identifierProviders,
            ICacheService cache)
        {
            this.quizzes = quizzes;
            this.identifierProvider = identifierProviders;
            this.cache = cache;
        }

        public Quiz GetById(string id)
        {
            var intId = this.identifierProvider.DecodeId(id);
            var quiz = this.quizzes.GetById(intId);
            return quiz;
        }

        public Quiz GetById(int id)
        {
            var quiz = this.quizzes.GetById(id);
            return quiz;
        }

        public IQueryable<Quiz> GetRandomQuizzes(int count)
        {
            return this.quizzes.All().OrderBy(x => Guid.NewGuid()).Take(count);
        }

        public IQueryable<Quiz> SearchQuizzes(QuizSearchModel queryParameters)
        {
            if (queryParameters == null)
            {
                throw new ArgumentNullException(nameof(queryParameters), "No query parameters!");
            }

            var result = this.quizzes.All();
            result = this.ApplyFiltering(result, queryParameters);
            result = this.ApplyOrdering(result, queryParameters);

            return result;
        }

        public IQueryable<Quiz> GetPage(Pager pager)
        {
            var result = this.quizzes.All()
                .Where(q => q.Category.Name.ToLower() == pager.CategoryName.ToLower())
                .OrderByDescending(q => q.CreatedOn)
                .Skip(pager.GetSkipCount())
                .Take(pager.PageSize);

            return result;
        }

        public void Add(Quiz quiz)
        {
            bool alreadyExists = this.quizzes.AllWithDeleted()
                .Any(q => q.Title.Equals(quiz.Title, StringComparison.CurrentCultureIgnoreCase));

            if (alreadyExists)
            {
                throw new QuizCreationException("Sorry there is already a quiz by that name in our database");
            }

            this.quizzes.Add(quiz);
            this.quizzes.Save();
        }

        public void Save()
        {
            this.quizzes.Save();
        }

        public int GetTotalPages(string categoryName, int pageSize)
        {
            int quizzesCount = this.quizzes
                .All()
                .Count(q => q.Category.Name.ToLower() == categoryName.ToLower());
            int result = (int)Math.Ceiling(quizzesCount / (double)pageSize);

            return result;
        }

        public void Delete(Quiz quiz)
        {
            this.quizzes.Delete(quiz);
            this.Save();
        }

        public int GetMaxSolutionsCount()
        {
            int result = this.cache.Get(
                "MaxSolutions",
                () => this.quizzes.All()
                    .Max(q => q.Solutions.Count),
                    durationInSeconds: 5 * 60);

            return result;
        }

        /// <summary>
        /// Returns QuizzesService to be fluent and chain expressions
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns>Returns Class instance to support chaining</returns>
        private QuizzesGeneralService UpdatePropertyValue<T>(T target, Expression<Func<T, object>> expression, object value)
        {
            var memberSelectorExpression = expression.Body as MemberExpression;
            if (memberSelectorExpression != null)
            {
                var property = memberSelectorExpression.Member as PropertyInfo;
                if (property != null)
                {
                    property.SetValue(target, value, null);
                }
            }

            return this;
        }

        private IQueryable<Quiz> ApplyFiltering(IQueryable<Quiz> result, QuizSearchModel queryParameters)
        {
            if (queryParameters.Category != null)
            {
                result = result.Where(q => q.Category.Name.Equals(queryParameters.Category, StringComparison.OrdinalIgnoreCase));
            }

            if (queryParameters.KeyPhrase != null)
            {
                string phrase = queryParameters.KeyPhrase.ToLower();

                result = result.Where(q => q.Title.ToLower().Contains(phrase)
                                             || q.Description.ToLower().Contains(phrase));
            }

            if (queryParameters.FromDate != null)
            {
                result = result.Where(q => q.CreatedOn >= queryParameters.FromDate);
            }

            if (queryParameters.ToDate != null)
            {
                result = result.Where(q => q.CreatedOn <= queryParameters.ToDate);
            }

            if (queryParameters.MinQuestions != null)
            {
                result = result.Where(q => q.Questions.Count >= queryParameters.MinQuestions);
            }

            if (queryParameters.MaxQuestions != null)
            {
                result = result.Where(q => q.Questions.Count <= queryParameters.MaxQuestions);
            }

            return result;
        }

        private IQueryable<Quiz> ApplyOrdering(IQueryable<Quiz> result, QuizSearchModel queryParameters)
        {
            if (queryParameters.OrderBy == null)
            {
                result = result.OrderByDescending(q => q.CreatedOn);
            }

            switch (queryParameters.OrderBy)
            {
                case ResultOrder.ByDate:
                    result = queryParameters.OrderDescending
                        ? result.OrderByDescending(q => q.CreatedOn)
                        : result.OrderBy(q => q.CreatedOn);
                    break;
                case ResultOrder.ByNumberOfQuestions:
                    result = queryParameters.OrderDescending
                        ? result.OrderByDescending(q => q.NumberOfQuestions).ThenByDescending(q => q.Questions.Count)
                        : result.OrderBy(q => q.NumberOfQuestions).ThenBy(q => q.Questions.Count);
                    break;
                case ResultOrder.ByTimesTaken:
                    result = queryParameters.OrderDescending
                        ? result.OrderByDescending(q => q.Solutions.Count)
                        : result.OrderBy(q => q.Solutions.Count);
                    break;
            }

            return result;
        }
    }
}
