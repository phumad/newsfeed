(function () {
    'use strict';

    angular
        .module('app')
        .factory('feedListService', feedListService);

    feedListService.$inject = ['$http', 'appSettings', '$q'];

    function feedListService($http, appSettings, $q) {
        var urlBase = appSettings.apiUrl; //'http://localhost:61664/api/feeds';

        var service = {
            getFeed: getFeed,
            getFeeds: getFeeds,
            addFeed: addFeed
        };

        return service;

        function getFeeds(pageNumber, pageSize) {
            var params = { 'page': pageNumber, 'pageSize': pageSize, 'bodySize': 100 };
            return $http.get(urlBase, { params: params }).then(
                function (response) {
                    return response.data;
                },
                function (error) {
                    return $q.reject(error);
                }
                );
        }

        function addFeed(feed) {
            return $http.post(urlBase, '"' + feed + '"' );
        }

        function getFeed(id) {
            return $http.get(urlBase + id).then(
                function (response) {
                    return response.data;
                },
                function (error) {
                    return $q.reject(error);
                }
                );
        }
    }
})();