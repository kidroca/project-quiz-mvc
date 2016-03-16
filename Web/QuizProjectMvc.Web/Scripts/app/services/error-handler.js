(function () {
	'use strict';

	var DEFFAULT_ERRORS = {

		CREATE_QUIZ: 'Something went wrong creating your qiuz... ' +
		'Please check that everithing is filled out and try againg',

		EDIT_QUIZ: 'Something went wroung updating the quiz...' +
		'Please try agian after you make sure everithing is ok',

		SOLVE_QUIZ: 'Something is wrong with the received data, please try again',

        EDIT_CATEGORY: 'Category save failed'
	}

	function errorHandler() {
		var self = this;        
	}

	errorHandler.prototype.handleCreateQuizError = function(resonse) {
	    var message = extractModelStateErrors(resonse.data.modelState) ||
                                                    resonse.data.message ||
                                                    DEFFAULT_ERRORS.CREATE_QUIZ;

		// Todo: beautify this notification
		alert(message);
	};

	errorHandler.prototype.handleEditQuizError = function (resonse) {
	    var message = extractModelStateErrors(resonse.data.modelState) ||
                                                    resonse.data.message ||
                                                    DEFFAULT_ERRORS.EDIT_QUIZ;

	    // Todo: beautify this notification
	    alert(message);
	};

	errorHandler.prototype.handleSoveQuizError = function (response) {
	    var message = extractModelStateErrors(response.data.modelState) ||
                                                    response.data.message ||
                                                    DEFFAULT_ERRORS.SOLVE_QUIZ;

		// Todo: beautify this notification
		alert(message);
	};

	errorHandler.prototype.handleEditCategoryError = function (response) {
	    var message = extractModelStateErrors(response.data.modelState) ||
	                                                response.data.message ||
	                                                DEFFAULT_ERRORS.EDIT_CATEGORY;

	    alert(message);
	};

	function extractModelStateErrors(modelState) {
		if (!modelState) return null;

		var message = '';

		for (var prop in modelState) {
			if (modelState.hasOwnProperty(prop)) {
				message += modelState[prop] + '\n';
			}
		}

		return message.trim();
	}

	angular.module('errorHandler', [])
		.service('errorHandler', [errorHandler]);
}());