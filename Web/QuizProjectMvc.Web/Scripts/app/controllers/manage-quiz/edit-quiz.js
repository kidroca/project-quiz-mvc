(function (angular) {
    'use strict';

    if (!angular) {
        throw new Error('Module edit-quiz.js is missing angular dependency');
    }

    var EMPTY_QUIZ = {
        id: 0,
        title: '',
        description: '',
        category: '',
        questions: []
    };

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

		$scope.resetForm = function resetForm(form) {
		    if (parent.resetForm(form)) {
		        var id = quiz.id;
		        quiz = angular.copy(EMPTY_QUIZ);
		        quiz.id = id;

		        parent.setQuiz(quiz);
            }
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
})(window.angular)