(function () {
    'use strict';

    var BASE_PATH = '/api/categories/';

    /**
     * Controller
    */
    function CategoriesController($http, errorHandler) {
        var self = this;

        self.toggleMode = function toggleMode(category) {
            console.log('edit');
            category.editMode = !category.editMode;
        };

        self.save = function save(category) {
            $http.post(BASE_PATH + 'edit', category)
                .then(function () {
                    alert('Category Saved!');
                    self.toggleMode(category);

                }, errorHandler.handleEditCategoryError);
        };

        self.destroy = function destroy(category) {
            var ok = confirm("Are you sure you want to delete this category?");
            if (!ok) {
                return;
            }

            $http.delete(BASE_PATH + 'delete/' + category.id)
                .then(function () {
                    removeCategory(self.categories, category);
                }, errorHandler.handleEditCategoryError);
        };

        self.addNew = function addNew(category) {
            $http.post(BASE_PATH + 'addNew', category)
                .then(function (res) {
                    console.log(res);
                    addCategory(self.categories, category, res.data.id);

                }, errorHandler.handleEditCategoryError);
        }

        initCategories(self, $http);
    }

    /**
     * Directive
    */
    function editCategory() {
        return {
            templateUrl: '/administration/categories/getDisplayTemplate',
            restrict: 'AE',
            replace: true
        }
    }

    // ================ Helpers ==============================

    function initCategories(ctrl, $http) {
        console.log('requesting categories');
        $http.get(BASE_PATH + 'getAll')
            .then(function (res) {
                console.log(res);
                ctrl.categories = res.data;
            });

    }

    function removeCategory(arr, item) {
        for (var i = arr.length; i--;) {
            if (arr[i] === item) {
                arr.splice(i, 1);
            }
        }
    }

    function addCategory(categories, category, id) {
        var copy = angular.copy(category);
        copy.id = id;
        copy.quizzesCount = 0;

        categories.push(copy);

        category.name = null;
        category.avatarUrl = null;
    }



    // =======================================================

    angular.module('categoriesManager', ['errorHandler', 'ui.bootstrap'])
        .controller('CategoriesController', ['$http', 'errorHandler', CategoriesController])
        .directive('editCategory', [editCategory]);
})();