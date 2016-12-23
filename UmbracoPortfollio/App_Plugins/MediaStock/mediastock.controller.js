function mediaStockController($scope, umbRequestHelper, $log, $http, mediaStockResource, dialogService, notificationsService) {

    $scope.search = function () {
        $scope.searching = true;
        $scope.page = 1;
        mediaStockResource.search($scope.txt, $scope.page).then(
            function (response) {
                var result = angular.fromJson(response.Data);
                $scope.info = result.total + ' photo' + (result.total == 1 ? '' : 's') + ' found for \'' + $scope.txt + "'";
                $scope.images = result.results;
            },
            function (error) {
                console.log(error);
            }
        );
        $scope.searching = false;
    };

    $scope.showMoreResults = function () {
        $scope.searching = true;
        $scope.page++;
        mediaStockResource.search($scope.txt, $scope.page).then(
            function (response) {
                var result = angular.fromJson(response.Data);
                $scope.images = $scope.images.concat(result.results);
            },
            function (error) {
                console.log(error);
            }
        );
        $scope.searching = false;
    }

    $scope.savePhoto = function (item) {
        if (item.isFolder) {
            $scope.folderId = item.id;
            notificationsService.info("Downloading", "Just downloading the photo...");
            mediaStockResource.use($scope.photoId, $scope.folderId, $scope.txt).then(
                function (response) {
                    notificationsService.success("Success", "The photo was added to your library!");
                },
                function (error) {
                    notificationsService.error("Error", "Something went wrong");
                }
            );
        } else {
            notificationsService.error("Error", "You must choose a folder!");
        }
    }

    $scope.use = function (id) {
        $scope.photoId = id;
        dialogService.mediaPicker({ callback: $scope.savePhoto, onlyFolders: true })
    }


    $scope.ready = true;
    $scope.searching = false;
    $scope.images = [];
    $scope.info = '';
    $scope.page = 1;
}
angular.module("umbraco").controller("Umbraco.Dashboard.MediaStockController", mediaStockController);