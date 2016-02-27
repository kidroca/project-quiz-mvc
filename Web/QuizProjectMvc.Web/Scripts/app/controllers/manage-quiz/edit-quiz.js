(function () {
	'use strict';

	function EditQuizController($scope, $controller, $http, $location, $localStorage, errorHandler) {
	    var self = this;
	    var parent = $controller('ManageQuizController', { $scope: $scope });
	    angular.extend(self, parent);

		console.log('Hello from EditQuizController');

		parent.setQuiz(window.__editQuiz__);

		self.addQuiz = function addQuiz(quiz, form) {
			console.log('updating quiz...');
			console.log(quiz);

		    $http.put('/api/Quizzes/Update/' + quiz.id, quiz)
		        .then(function () {
		            // $scope.resetForm(form);
		            window.location = '/'; // Todo: $location.path('/') is not working for some reason, try to fix.
		        }, errorHandler.handleEditQuizError);
		};

		self.clearQuizCallback = function clearQuizCallback() {
		    parent.setQuiz({ id: window.__editQuiz__.id });
		}
	}

    angular.module('manageQuiz')
		.controller('EditQuizController', [
			'$scope',
			'$controller',
            '$http',
			'$location',
			'$localStorage',
			'errorHandler',
			EditQuizController
		]);
})()