(function(angular, quiz) {
    'use strict';

    var INITIAL_TRANSLATE = 14.5;
    var TRANSLATE_STEP = 100;

    if (!angular || !quiz) {
        throw new Error('Solve quiz module has missing dependencies');
    }

    angular.module('solveQuiz', ['ui.bootstrap', 'slickCarousel', 'paging', 'errorHandler'])
           .controller('SolveQuizController', ['$http', '$window', 'errorHandler', SolveQuizController])
           .directive('kidQuestionPagination', ['uibPaginationDirective', questionPagination]);

    function SolveQuizController($http, $window, errorHandler) {

        var self = this;

        self.$http = $http;
        self.$window = $window;
        self.errorHandler = errorHandler;

        /**
         * Mark the selected answer of quiz a question
         * @param {int} questionIndex
         * @param {int} selectedAnswerIndex 
         * @returns {void} 
         */
        self.select = function (questionIndex, selectedAnswerIndex) {

            var question = self.quiz.Questions[questionIndex];

            var hasPrevSelected = angular.isDefined(question.selected);

            question.selected = selectedAnswerIndex;

            self.pager.toggleAnswered(questionIndex + 1, true);
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

    Object.defineProperties(SolveQuizController.prototype, {

        currentPage: {
            get: function () {
                return this._currentPage;
            },
            set: function (pageNumber) {

                var pageDiff = Math.abs(this.currentPage - pageNumber);
                this.transformTransition = pageDiff < 2 ? 'transform 0.6s' : 'transform 0s';

                this._currentPage = pageNumber;
            }
        }
    });

    /**
     * Initializes the properties of the SolveQuizController
     * @private
     */
    SolveQuizController.prototype._init = function _init() {

        var self = this;

        console.log('Hello from Solve Quiz Controller');

        var mq = self.$window && self.$window.matchMedia( '(max-width: 767px)' );

        if (mq.matches) {
            INITIAL_TRANSLATE = 0;
        }

        self.questionTemplate = '/Content/templates/solve-question-template.html';
        self.quiz = quiz;
        self.questionsCount = quiz.Questions.length;

        self.pager = {
            pageSize: 1,
            totalPages: self.questionsCount
        };

        self.currentPage = 1;

        self.sliderWidth = self.questionsCount * 100 + 'vw';
    };

    SolveQuizController.prototype.next = function () {

        if (!this.isLastPage()) {
            this.currentPage++;
        }
    };

    SolveQuizController.prototype.prev = function () {

        if (!this.isFirstPage()) {
            this.currentPage--;
        }
    };

    SolveQuizController.prototype.isLastPage = function () {
        return this.currentPage === this.pager.totalPages;
    };

    SolveQuizController.prototype.isFirstPage = function () {
        return this.currentPage === 1;
    };

    SolveQuizController.prototype.getCurrentTranslate = function () {

        var translate = ((this.currentPage - 1) * -TRANSLATE_STEP) + INITIAL_TRANSLATE;
        return 'translateX(' + translate + 'vw)';
    };

    // ### Pagination Extension ###

    function questionPagination(uibPaginationDirective) {

        var config = uibPaginationDirective[0];

        var extension = {
            name: 'kidQuestionPagination',
            templateUrl: '/Content/templates/kid-question-paging.html',
            require: ['kidQuestionPagination', '?ngModel'],
            link: function (scope, el, attrs) {

                config.link.apply(this, arguments);
                scope.answered = {};

                scope.toggleAnswered = function (pageNumber, state) {

                    if (angular.isDefined(state)) {
                        scope.answered[pageNumber] = state;
                    }
                    else {
                        scope.answered[pageNumber] = !scope.answered[pageNumber];
                    }
                }
            }
        };

        var extended = angular.extend({}, config, extension);
        extended.scope.toggleAnswered = '=';
        delete extended.compile;

        return extended;
    }

})(window.angular, window.quiz);
