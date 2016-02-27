(function () {
	'use strict';

	function CreateQuizController($scope, $controller,$http, $location, $localStorage, errorHandler) {
		var self = this;
		var parent = $controller('ManageQuizController', { $scope: $scope });
		angular.extend(self, parent);

		console.log('Hello from CreateQuizController');

		$localStorage.quiz = $localStorage.quiz || {};
	    parent.setQuiz($localStorage.quiz);

		self.addQuiz = function addQuiz(quiz, form) {
			console.log('adding quiz...');
			console.log(quiz);

		    $http.post('/api/Quizzes/Create', quiz)
		        .then(function() {
		            // $scope.resetForm(form);
		            window.location = '/'; // Todo: $location.path('/') is not working for some reason, try to fix.
		        }, errorHandler.handleCreateQuizError);
		};

		self.clearQuizCallback = function clearQuizCallback() {
		    delete $localStorage.quiz;
		    $localStorage.quiz = {}; //angular.copy(DEFAULT_STORAGE);
		    // parent.setQuiz($localStorage.quiz);
		}
	}

    angular.module('manageQuiz')
		.controller('CreateQuizController', [
			'$scope',
			'$controller',
            '$http',
			'$location',
			'$localStorage',
			'errorHandler',
			CreateQuizController
		]);
})()