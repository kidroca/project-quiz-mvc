(function(angular, quiz) {
    'use strict';

    if (!angular || !quiz) {
        throw new Error('Solve quiz module has missing dependencies');
    }

    function SolveQuizController($http, $timeout, $document, errorHandler) {

        var self = this;

        self.$http = $http;
        self.$timeout = $timeout;
        self.$document = $document;
        self.errorHandler = errorHandler;

        /**
         * Mark the selected answer of quiz a question
         * @param {object} question 
         * @param {int} selectedAnswerIndex 
         * @returns {void} 
         */
        self.select = function (question, selectedAnswerIndex) {

            var hasPrevSelected = angular.isDefined(question.selected);

            question.selected = selectedAnswerIndex;

            self.$questionsPaging.find('.active').addClass('answered');

            if (!hasPrevSelected) self.next();

            self.progress();
        };

        /**
         * Updates the progressbar position
         * @returns {number} progress percentage 
         */
        self.progress = function progress() {
            var total = self.questionsCount;
            var answered = self.quiz.Questions.filter(function(q) {
                return q.selected >= 0;
            }).length;

            self.completedPercent = (answered / total) * 100;

            return self.completedPercent;
        };

        self.submit = function submit() {
            var data = {
                forQuizId: quiz.Id,
                selectedAnswerIds: quiz.Questions.map(function(question) {
                    console.log(question.Answers[question.selected].Id);
                    var id = question.Answers[question.selected].Id;
                    return id;
                })
            };

            $http.post('/SolveQuiz/solve', data)
                .then(function(response) {
                    console.log(response);
                    document.open();
                    document.write(response.data);
                    document.close();
                }, errorHandler.handleSoveQuizError);
        };

        self._init();
    }

    /**
     * Initializes the properties of the SolveQuizController
     * @private
     */
    SolveQuizController.prototype._init = function _init() {

        var self = this;

        console.log('Hello from Solve Quiz Controller');

        self.questionTemplate = '/Content/templates/solve-question-template.html';
        self.quiz = quiz;
        self.questionsCount = quiz.Questions.length;

        self.pager = {
            currentPage: 1,
            pageSize: 1,
            totalPages: self.questionsCount
        };

        self.answeredQuestionsCount = 0;

        self.$questionsPaging = self.$document.find('.pagination');
    };

    SolveQuizController.prototype.next = function () {

        if (!this.isLastPage()) {
            this.pager.currentPage++;
        }
    };

    SolveQuizController.prototype.prev = function () {

        if (!this.isFirstPage()) {
            this.pager.currentPage--;
        }
    };

    SolveQuizController.prototype.isLastPage = function () {
        return this.pager.currentPage === this.pager.totalPages;
    };

    SolveQuizController.prototype.isFirstPage = function () {
        return this.pager.currentPage === 1;
    };

    angular.module('solveQuiz', ['ui.bootstrap', 'slickCarousel', 'paging', 'errorHandler'])
        .controller('SolveQuizController', ['$http', '$timeout', '$document', 'errorHandler', SolveQuizController]);
})(window.angular, window.quiz);
