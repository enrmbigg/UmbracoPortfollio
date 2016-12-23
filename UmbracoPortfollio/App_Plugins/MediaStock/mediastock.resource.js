function mediaStockResource($q, $http, umbRequestHelper) {
    return {
        search: function (txt, page) {
            return umbRequestHelper.resourcePromise(
                $http.post("backoffice/MediaStock/MediaStockApi/Search?text=" + txt + "&page=" + page));
        },
        use: function (id, folderId, search) {
            return umbRequestHelper.resourcePromise(
                $http.post("backoffice/MediaStock/MediaStockApi/Use?id=" + id + "&folderId=" + folderId + "&search=" + search));
        }
    };
}
angular.module("umbraco.resources").factory("mediaStockResource", mediaStockResource);