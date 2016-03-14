(function () {
    'use strict';

    var BASE_PATH = '/api/categories/';

    /**
     * Controller
    */
    function CategoriesController($http, $uibModal, errorHandler) {
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

        self.openImagesMenu = function openImagesMenu(category) {
            var modalInstance = $uibModal.open({
                animation: true,
                appendTo: $('#categories-menu'),
                templateUrl: '/Administration/Categories/GetImagesModalTemplate',
                controller: 'ImagesModalController',
                controllerAs: 'ctrl'
            });

            modalInstance.result.then(function (imageSrc) {
                if (imageSrc !== null) {
                    category.avatarUrl = imageSrc;
                }

            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });

            modalInstance.closed.then(function () {
                console.log('close');
            });
        };

        initCategories(self, $http);
    }

    /**
     * Controller
    */
    function ImagesController($scope, $timeout, Upload) {
        var self = this;

        $scope.upload = function (dataUrl, name) {
            Upload.upload({
                url: '/api/categories/uploadImage',
                data: {
                    file: Upload.dataUrltoBlob(dataUrl, name)
                }
            }).then(function (response) {
                $timeout(function () {
                    $scope.result = response.data;
                });
            }, function (response) {
                if (response.status > 0) $scope.errorMsg = response.status
                    + ': ' + response.data;
            }, function (evt) {
                $scope.progress = parseInt(100.0 * evt.loaded / evt.total);
            });
        }
    }

    /**
     * Controller
    */
    function ImagesModalController($uibModalInstance, $http) {
        var self = this;

        self.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

        self.select = function select(imgSrc) {
            console.log(imgSrc);
            $uibModalInstance.close(imgSrc);
        }

        self.deleteImage = function deleteImage(src, index) {
            $http.delete(BASE_PATH + 'deleteImage?name=' + src)
                .then(function() {
                    self.categoryImages.splice(index, 1);
                });
        }

        $http.get(BASE_PATH + 'getAvailableImages')
            .then(function (res) {
                console.log(res);
                self.categoryImages = [];

                res.data.forEach(function (name) {
                    self.categoryImages.push('/Content/images/categories/' + name);
                });
            });
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

    angular.module('categoriesManager', ['ngFileUpload', 'ngImgCrop', 'errorHandler', 'ui.bootstrap'])
        .controller('CategoriesController', ['$http', '$uibModal', 'errorHandler', CategoriesController])
        .controller('ImagesController', ['$scope', '$timeout', 'Upload', ImagesController])
        .controller('ImagesModalController', ['$uibModalInstance', '$http', ImagesModalController])
        .directive('editCategory', [editCategory]);
})();