namespace QuizProjectMvc.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using Models;
    using Models.Evaluation;
    using Models.Search;
    using QuizProjectMvc.Data.Common;
    using QuizProjectMvc.Data.Models;
    using Web;

    // Todo: use quizzes creation exception with save quiz

    public class QuizzesService : IQuizzesService
    {
        private readonly IDbRepository<Quiz> quizzes;
        private readonly IDbRepository<QuizSolution> solutions;
        private readonly IIdentifierProvider identifierProvider;

        public QuizzesService(
            IDbRepository<Quiz> quizzes,
            IIdentifierProvider identifierProviders,
            IDbRepository<QuizSolution> solutions)
        {
            this.quizzes = quizzes;
            this.identifierProvider = identifierProviders;
            this.solutions = solutions;
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

        public QuizEvaluationResult EvaluateSolution(int solutionId)
        {
            var solution = this.solutions.GetById(solutionId);
            if (solution == null)
            {
                return null;
            }

            return this.EvaluateSolution(solution);
        }

        public QuizSolution SaveSolution(SolutionForEvaluationModel quizSolution, string userId)
        {
            var quiz = this.quizzes.GetById(quizSolution.ForQuizId);

            if (quizSolution.SelectedAnswerIds.Count != quiz.Questions.Count)
            {
                throw new QuizEvaluationException("Invalid Solution: Questions count mismatch");
            }

            List<Answer> selectedAnswers = this.ExtractSelectedAnswers(quiz, quizSolution);

            var newSolution = new QuizSolution
            {
                ByUserId = userId,
                ForQuiz = quiz,
                SelectedAnswers = selectedAnswers
            };

            try
            {
                this.solutions.Add(newSolution);
                this.solutions.Save();
            }
            catch (Exception ex)
            {
                // Todo: Implement concrete exception cases
                throw new QuizEvaluationException("Something went wrong while saving your solution.", ex);
            }

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

        public void Add(Quiz quiz)
        {
            this.quizzes.Add(quiz);
            this.quizzes.Save();
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

        private List<Answer> ExtractSelectedAnswers(Quiz quiz, SolutionForEvaluationModel quizSolution)
        {
            var result = quiz.Questions
                .SelectMany(q => q.Answers)
                .Where(a => quizSolution.SelectedAnswerIds.Any(id => id == a.Id))
                .ToList();

            return result;
        }
    }
}
