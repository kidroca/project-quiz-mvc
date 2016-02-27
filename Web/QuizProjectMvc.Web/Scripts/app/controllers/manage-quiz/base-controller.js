(function () {
	'use strict';

	//var DEFAULT_STORAGE = {
	//	title: '',
	//	description: '',
	//	category: '',
	//	questions: []
	//};

	function ManageQuizController($scope, $localStorage, $uibModal) {
		var self = this;

		console.log('Hello from BaseController');

		self.setQuiz = function setQuiz(quiz) {
			$scope.quiz = quiz;
		}

		$scope.resetForm = function resetForm(form) {
			var check = confirm('Are you sure you want to reset, all fields and questions will be reset.');

			if (!check) {
				return;
			}

			form.$setPristine();
			form.$setUntouched();

			self.clearQuizCallback();

			//$localStorage.quiz = {} //angular.copy(DEFAULT_STORAGE);
			//$scope.quiz = $localStorage.quiz;
		};

		self.removeQuestion = function removeQuestion(index) {
			$scope.quiz.questions.splice(index, 1);
		};

		self.getCategories = function getCategories(pattern) {
			return $http.get('/api/categories/getByPattern?pattern=' + pattern)
				.then(function (res) {
					return res.data;
				});
		};

		self.openQuesitonMenu = function openQuesitonMenu(question) {
			var modalInstance = $uibModal.open({
				animation: true,
				templateUrl: '/ManageQuiz/AddQuestionTemplate',
				controller: 'AddQuestionController',
				controllerAs: 'ctrl',
				resolve: {
					items: question
				}
			});

			modalInstance.result.then(function (question) {
				if (question !== null) {
					$scope.quiz.questions.push(question);
				}
			}, function () {
				console.log('Modal dismissed at: ' + new Date());
			});
		};
	}

	angular.module('manageQuiz', [
		'ui.bootstrap', 'ngStorage', 'paging', 'toggle-switch', 'errorHandler', 'addQuestion'])
		.controller('ManageQuizController', [
			'$scope',
			'$localStorage',
			'$uibModal',
			ManageQuizController
		]);
})()