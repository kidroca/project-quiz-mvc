namespace QuizProjectMvc.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using Models.Evaluation;
    using Protocols;
    using QuizProjectMvc.Data.Common;
    using QuizProjectMvc.Data.Models;

    public class QuizzesEvalService : IQuizzesEvalService
    {
        private readonly IDbRepository<Quiz> quizzes;
        private readonly IDbRepository<QuizSolution> solutions;

        public QuizzesEvalService(IDbRepository<Quiz> quizzes, IDbRepository<QuizSolution> solutions)
        {
            this.quizzes = quizzes;
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

            if (quizSolution.SelectedAnswerIds.Count != quiz.NumberOfQuestions &&
                quizSolution.SelectedAnswerIds.Count != quiz.Questions.Count)
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

        public void Save()
        {
            this.quizzes.Save();
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
