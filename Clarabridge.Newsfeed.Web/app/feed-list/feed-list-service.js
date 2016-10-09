(function () {
    'use strict';

    angular
        .module('app')
        .factory('feedListService', feedListService);

    feedListService.$inject = ['$http'];

    function feedListService($http) {
        var urlBase = 'http://localhost:61664/api/feeds';

        var service = {
            getFeed: getFeed,
            getFeeds: getFeeds,
            addFeed: addFeed
        };

        return service;

        function getFeeds(pageNumber) {
            var params = { 'page': pageNumber, 'pageSize': 10, 'bodySize': 100 };
            return $http.get(urlBase, { params: params });
        }

        function addFeed(feed) {
            return $http.post(urlBase, '"' + feed + '"' );
        }

        function getFeed(id) {
            return $http.get('http://localhost:61664/api/feeds/'+id);
        }
    }
})();