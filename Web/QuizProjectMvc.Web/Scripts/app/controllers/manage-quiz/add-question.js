(function (angular) {
    'use strict';

    if (!angular) {
        throw new Error('Module add-question.js is missing angular dependency');
    }

    function AddQuestionController($uibModalInstance, resource) {
        var self = this,
            backup = '';

        self.letters = 'abcdefghijk';

        if (resource) {
            self.question = resource;
            backup = JSON.stringify(resource);
        } else {
            self.question = {
                title: '',
                answers: []
            };
        }

        self.ok = function () {
            if (backup) {
                // if backup exists then this is an edit, no need to return the question
                // it is already passed by reference
                $uibModalInstance.close(null);
            } else {
                // return the question to be added to the quiz
                $uibModalInstance.close(self.question);
            }
        };

        self.cancel = function () {
            if (backup) {
                restoreBackup(resource, backup);

                console.log(resource);
            }

            $uibModalInstance.dismiss('cancel');
        };

        self.addAnswer = function addAnswer() {
            var answer = {
                text: ''
            };

            if (!self.question.answers.length) {
                answer.isCorrect = true;
            }

            self.question.answers.push(answer);
            var last = self.question.answers.length - 1;

            setTimeout(function () {
                try {
                    document.getElementsByClassName('answer-field')[last].focus();
                } catch (e) {
                    
                }
                
            }, 100);
        };

        self.removeAnswer = function removeAnswer(index) {
            self.question.answers.splice(index, 1);
            if (self.question.answers.length === 1) {
                self.question.answers[0].isCorrect = true;
            }
        };

        self.markCorrect = function markCorrect(index) {
            self.question.answers.forEach(function (answer, i) {
                answer.isCorrect = i === index;
            });
        };

        self.hasCorrect = function hasCorrect() {
            return self.question.answers.some(function (answer) {
                return answer.isCorrect;
            });
        };

        self.saveIsAvailable = function saveIsAvailable(questionForm) {
            var result = questionForm.$valid && self.hasCorrect() && self.question.answers.length >= 2;

            return result;
        }
    }

    function restoreBackup(obj, backup) {
        var backupAsObject = JSON.parse(backup),
            prop;

        for (prop in backupAsObject) {
            obj[prop] = backupAsObject[prop];
        }
    }

    angular.module('manageQuiz')
        .controller('AddQuestionController', [
                '$uibModalInstance',
                'items',
                AddQuestionController
        ]);
})(window.angular);