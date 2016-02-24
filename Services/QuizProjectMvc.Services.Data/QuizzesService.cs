namespace QuizProjectMvc.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Models;
    using Models.Evaluation;
    using Models.Search;
    using QuizProjectMvc.Data.Common;
    using QuizProjectMvc.Data.Models;
    using Web;

    public class QuizzesService : IQuizzesService
    {
        private readonly IDbRepository<Quiz> quizzes;
        private readonly UserManager<User> manager;
        private readonly IIdentifierProvider identifierProvider;

        public QuizzesService(
            IDbRepository<Quiz> quizzes,
            IIdentifierProvider identifierProviders,
            UserManager<User> manager)
        {
            this.quizzes = quizzes;
            this.identifierProvider = identifierProviders;
            this.manager = manager;
        }

        public QuizEvaluationResult EvaluateSolution(QuizSolution quizSolution)
        {
            var result = new QuizEvaluationResult
            {
                ForQuizId = quizSolution.ForQuizId,
                Title = quizSolution.ForQuiz.Title,
                CorrectlyAnswered = new List<QuestionResultModel>(),
                IncorrectlyAnswered = new List<QuestionResultModel>()
            };

            foreach (Answer answer in quizSolution.SelectedAnswers)
            {
                var questionResult = new QuestionResultModel
                {
                    Question = answer.ForQuestion.Title,
                    IsCorrect = answer.IsCorrect,
                    ResultDescription = answer.ForQuestion.ResultDescription,
                    SelectedAnswer = answer.Text,
                    CorrectAnswer = answer.ForQuestion
                            .Answers.First(a => a.IsCorrect).Text
                };

                if (answer.IsCorrect)
                {
                    result.CorrectlyAnswered.Add(questionResult);
                }
                else
                {
                    result.IncorrectlyAnswered.Add(questionResult);
                }
            }

            return result;
        }

        public QuizSolution SaveSolution(SolutionForEvaluationModel quizSolution, Quiz quiz, string userId)
        {
            var selectedAnswers = new List<Answer>();
            foreach (var answerId in quizSolution.SelectedAnswerIds)
            {
                selectedAnswers.Add(new Answer
                {
                    Id = answerId
                });
            }

            var newSolution = new QuizSolution
            {
                ByUserId = userId,
                CreatedOn = DateTime.Now,
                SelectedAnswers = selectedAnswers,
            };

            quiz.Solutions.Add(newSolution);
            this.quizzes.Save();

            return newSolution;
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
            var result = this.quizzes.All();
            this.ApplyFiltering(result, queryParameters);
            this.ApplyOrdering(result, queryParameters);

            return result;
        }

        public IQueryable<Quiz> GetPage(Pager pager)
        {
            var result = this.quizzes.All()
                .OrderByDescending(q => q.CreatedOn)
                .Skip(this.GetSkipCount(pager))
                .Take(pager.PageSize);

            return result;
        }

        private void ApplyFiltering(IQueryable<Quiz> result, QuizSearchModel queryParameters)
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

            if (queryParameters.MinRating != null)
            {
                result = result.Where(q => q.Ratings.Average(r => r.Value) >= queryParameters.MinRating);
            }

            if (queryParameters.MaxRating != null)
            {
                result = result.Where(q => q.Ratings.Average(r => r.Value) <= queryParameters.MaxRating);
            }
        }

        private void ApplyOrdering(IQueryable<Quiz> result, QuizSearchModel queryParameters)
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
                case ResultOrder.ByRating:
                    result = queryParameters.OrderDescending
                        ? result.OrderByDescending(q => q.Ratings.Average(r => r.Value))
                        : result.OrderBy(q => q.Ratings.Average(r => r.Value));
                    break;
                case ResultOrder.ByNumberOfQuestions:
                    result = queryParameters.OrderDescending
                        ? result.OrderByDescending(q => q.Questions.Count)
                        : result.OrderBy(q => q.Questions.Count);
                    break;
                case ResultOrder.ByTimesTaken:
                    result = queryParameters.OrderDescending
                        ? result.OrderByDescending(q => q.Solutions.Count)
                        : result.OrderBy(q => q.Solutions.Count);
                    break;
            }
        }

        private int GetSkipCount(Pager pager)
        {
            return (pager.Page - 1) * pager.PageSize;
        }
    }
}
