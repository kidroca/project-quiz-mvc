(function () {
	'use strict';

	function AboutController($http) {
		var self = this;
		console.log('Hello from About Quiz Controller');

	    self.slickConfig = {
	        dots: false,
	        infinite: true,
	        speed: 500,
	        fade: true,
	        cssEase: 'linear',
	        appendArrows: '.about.wrapper',
            prevArrow: '',
	        nextArrow: '.btn-next'
	    }
	}

	angular.module('about', ['ui.bootstrap', 'slickCarousel'])
		.controller('AboutController', [AboutController]);
})()