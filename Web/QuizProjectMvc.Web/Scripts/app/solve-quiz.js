(function () {
	'use strict';

	function SolveQuizController($http, errorHandler) {
		var self = this;
		console.log('Hello from Solve Quiz Controller');
		console.log(quiz);

		self.quiz = quiz;
		self.currentPage = 1;
		self.questionsPerPage = 2;

		self.progress = function progress() {
			var total = self.quiz.Questions.length,
				selected = self.quiz.Questions.filter(function (question) {
					return question.selected !== undefined;
				}).length;

			var completedInPercent = (selected / total) * 100;

			return completedInPercent;
		};

		self.submit = function submit() {
			var data = {
				forQuizId: quiz.Id,
				selectedAnswerIds: quiz.Questions.map(function (question) {
					console.log(question.Answers[question.selected].Id);
					var id = question.Answers[question.selected].Id;
					return id;
				})
			};

			console.log("posting data....", data);
			$http.post('/SolveQuiz/solve', data)
				.then(function (response) {
				    console.log(response);
			        document.open();
				    document.write(response.data);
				    document.close();
				}, errorHandler.handleSoveQuizError);
		}
	}

	angular.module('solveQuiz', ['ui.bootstrap', 'paging', 'errorHandler'])
		.controller('SolveQuizController', ['$http', 'errorHandler', SolveQuizController]);
})()