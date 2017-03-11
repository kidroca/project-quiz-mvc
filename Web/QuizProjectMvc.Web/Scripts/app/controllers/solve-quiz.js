(function(angular, Bookblock, quiz) {
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
            self.progress();

            question.selected = selectedAnswerIndex;

            self.$questionsPaging.find('.active').addClass('answered');

            self.pager.currentPage++;
            self.flip(self.pager.currentPage);
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

            var completedInPercent = (answered / total) * 100;

            self.$progressBar.style.width = completedInPercent + '%';

            return completedInPercent;
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

            console.log("posting data....", data);
            $http.post('/SolveQuiz/solve', data)
                .then(function(response) {
                    console.log(response);
                    document.open();
                    document.write(response.data);
                    document.close();
                }, errorHandler.handleSoveQuizError);
        };

        self.flip = function (toPageNumber) {

            $timeout(function () {
                self.bookBlock.jump(toPageNumber);
            }, 60);
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
        console.log(quiz);

        self.questionTemplate = '/Content/templates/solve-question-template.html';
        self.quiz = quiz;
        self.questionsCount = quiz.Questions.length;

        self.pager = {
            currentPage: 1,
            pageSize: 1,
            totalPages: self.questionsCount
        };

        self.answeredQuestionsCount = 0;

        $(document).ready(function() {

            self.$progressBar = document.getElementById('progress');

            // This is delayed because apparently the html hasn't been loaded yet
            self.$book = document.getElementById('bb-blockbook');

            self.bookBlock = new BookBlock(self.$book, {
                speed: 500,
                shadowSides: 0.8,
                shadowFlip: 0.7
            });

            self.$questionsPaging = $('.pagination');
        });
    };

    angular.module('solveQuiz', ['ui.bootstrap', 'slickCarousel', 'paging', 'errorHandler'])
        .controller('SolveQuizController', ['$http', '$timeout', 'errorHandler', SolveQuizController]);
})(window.angular, window.BookBlock, window.quiz);