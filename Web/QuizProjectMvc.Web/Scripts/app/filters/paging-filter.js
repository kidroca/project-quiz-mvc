(function (angular) {
    'use strict';

    if (!angular) {
        throw new Error('Module paging-filter.js is missing angular dependency');
    }

	function paging() {
		return function (collection, page, size) {
			page = +page; //parse to int
			size = +size;

			var start = (page - 1) * size,
				end = page * size;
			return collection.slice(start, end);
		};
	}

	angular.module('paging', [])
		.filter('paging', [paging]);
}(window.angular));