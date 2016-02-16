namespace QuizProjectMvc.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using QuizProjectMvc.Data.Common;
    using QuizProjectMvc.Data.Models;
    using QuizProjectMvc.Web.Infrastructure.SharedModels.Evaluation;
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
            foreach (var answeredQuestion in quizSolution.Questions)
            {
                selectedAnswers.Add(new Answer
                {
                    Id = answeredQuestion.SelectedAnswerId
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

        public IQueryable<Quiz> GetRandomQuizzes(int count)
        {
            return this.quizzes.All().OrderBy(x => Guid.NewGuid()).Take(count);
        }
    }
}
