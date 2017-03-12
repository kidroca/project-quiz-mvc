namespace QuizProjectMvc.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Common;
    using Data.Models;
    using Services.Data.Exceptions;
    using Services.Data.Models.Evaluation;
    using Services.Data.Protocols;
    using ViewModels.Quiz.Create;
    using ViewModels.Quiz.Edit;

    public class QuizzesController : BaseController
    {
        private readonly IQuizzesGeneralService quizzes;
        private readonly ICategoriesService categories;
        private readonly IQuizzesEvalService quizzesEvalService;

        public QuizzesController(
            IQuizzesGeneralService quizzes,
            ICategoriesService categories,
            IQuizzesEvalService quizzesEvalService)
        {
            this.quizzes = quizzes;
            this.categories = categories;
            this.quizzesEvalService = quizzesEvalService;
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Create(CreateQuizModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var category = this.categories.GetById(model.Category.Id);
            if (category == null)
            {
                return this.BadRequest(this.ModelState);
            }

            var quiz = this.Mapper.Map<Quiz>(model);
            quiz.Category = category;
            quiz.CreatedById = this.UserId;

            try
            {
                this.quizzes.Add(quiz);
                return this.CreatedAtRoute(
                "QuizApi", new { action = "Solve", id = quiz.Id }, new { id = quiz.Id });
            }
            catch (QuizCreationException ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public IHttpActionResult Update(int id, EditQuizModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var dataQuiz = this.quizzes.GetById(id);
            bool userIsAdmin = this.User.IsInRole(GlobalConstants.AdministratorRoleName);

            if (dataQuiz == null || (this.UserId != dataQuiz.CreatedById && !userIsAdmin))
            {
                return this.NotFound();
            }

            var category = this.categories.GetById(model.Category.Id);
            if (category == null)
            {
                return this.BadRequest(this.ModelState);
            }

            this.Mapper.Map(model, dataQuiz);
            this.MapQuestions(model, dataQuiz);
            dataQuiz.Category = category;

            try
            {
                this.quizzes.Save();
                return this.Ok(new { message = "Quiz updated successfully" });
            }
            catch (QuizCreationException ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Solve(SolutionForEvaluationModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            try
            {
                var result = this.quizzesEvalService.SaveSolution(model, this.UserId);
                return this.Ok(this.quizzesEvalService.Evaluate(result));
            }
            catch (QuizEvaluationException ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        private void MapQuestions(EditQuizModel source, Quiz destination)
        {
            var existingQuestions = new List<Question>(destination.Questions);
            destination.Questions.Clear();

            foreach (var question in source.Questions)
            {
                var existingQuestion = existingQuestions.FirstOrDefault(q => q.Id == question.Id);
                if (existingQuestion == null)
                {
                    destination.Questions.Add(this.Mapper.Map<Question>(question));
                }
                else
                {
                    destination.Questions.Add(existingQuestion);

                    this.Mapper.Map(question, existingQuestion);
                    var existingAnswers = new List<Answer>(existingQuestion.Answers);
                    existingQuestion.Answers.Clear();

                    foreach (var answer in question.Answers)
                    {
                        var existingAnswer = existingAnswers.FirstOrDefault(a => a.Id == answer.Id);
                        if (existingAnswer == null)
                        {
                            existingQuestion.Answers.Add(this.Mapper.Map<Answer>(answer));
                        }
                        else
                        {
                            this.Mapper.Map(answer, existingAnswer);
                            existingQuestion.Answers.Add(existingAnswer);
                        }
                    }

                    this.Mapper.Map(question, existingQuestion);
                }
            }
        }
    }
}
