(function () {
	'use strict';

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
}());