namespace QuizProjectMvc.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Core;
    using System.Linq;
    using AutoMapper;
    using Exceptions;
    using Models.Evaluation;
    using Models.Evaluation.Contracts;
    using Protocols;
    using QuizProjectMvc.Data.Common;
    using QuizProjectMvc.Data.Models;

    public class QuizzesEvalService : IQuizzesEvalService
    {
        private readonly IDbRepository<Quiz> quizzes;
        private readonly IDbRepository<QuizSolution> solutions;
        private readonly IMapper mapper;

        public QuizzesEvalService(
            IDbRepository<Quiz> quizzes, IDbRepository<QuizSolution> solutions, IMapper mapper)
        {
            this.quizzes = quizzes;
            this.solutions = solutions;
            this.mapper = mapper;
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
            catch (Exception ex) // Todo: Implement concrete exception cases
            {
                throw new QuizEvaluationException("Something went wrong while saving your solution.", ex);
            }

            return newSolution;
        }

        public void Save()
        {
            this.quizzes.Save();
        }

        public IQuizEvaluationResult Evaluate(int solutionId)
        {
            var solution = this.solutions.GetById(solutionId);
            if (solution == null)
            {
                throw new ObjectNotFoundException("Falied to find solution by Id");
            }

            return this.Evaluate(solution);
        }

        public IQuizEvaluationResult Evaluate(QuizSolution solution)
        {
            var result = this.CreateEvaluation(solution);
            var answersByQuestionId = this.GetAnswersByQuestionId(solution);

            return this.AddSelectedAnswers(answersByQuestionId, result);
        }

        private List<Answer> ExtractSelectedAnswers(Quiz quiz, SolutionForEvaluationModel quizSolution)
        {
            var result = quiz.Questions
                .SelectMany(q => q.Answers)
                .Where(a => quizSolution.SelectedAnswerIds.Any(id => id == a.Id))
                .ToList();

            return result;
        }

        private IQuizEvaluationResult CreateEvaluation(QuizSolution solution)
        {
            var result = this.mapper.Map<QuizEvaluationResult>(solution.ForQuiz);

            return result;
        }

        private IDictionary<int, Answer> GetAnswersByQuestionId(QuizSolution solution)
        {
            var result = solution.SelectedAnswers.ToDictionary(key => key.ForQuestionId);
            return result;
        }

        private IQuizEvaluationResult AddSelectedAnswers(
            IDictionary<int, Answer> answersByQuestionId, IQuizEvaluationResult evaluation)
        {
            var allQuestions = evaluation.QuestionResults.ToArray();
            evaluation.QuestionResults.Clear();

            foreach (var question in allQuestions)
            {
                if (answersByQuestionId.ContainsKey(question.Id))
                {
                    question.SelectedAnswerId = answersByQuestionId[question.Id].Id;
                    evaluation.QuestionResults.Add(question);
                }
            }

            return evaluation;
        }
    }
}
