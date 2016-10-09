(function () {
    'use strict';

    angular
        .module('app')
        .controller('feedListController', feedListController);

    feedListController.$inject = ['$location', '$scope', 'feedListService'];

    function feedListController($location, $scope, feedListService) {
        // display feed page
        $scope.feeds = {};
        $scope.pageNumber = 1;
        getCurrentFeedsPage();
        
        // display details
        $scope.feedFullText = {};
        $scope.showDetailId = 0;
        $scope.showDetails = function (feedId) {
            $scope.showDetailId = feedId;
            feedListService.getFeed(feedId).success(function (feed) {
                $scope.feedFullText = feed.body;
            });
        };

        $scope.hideDetails = function () {
            $scope.feedFullText = {};
            $scope.showDetailId = 0;
        };

        // add new feed
        $scope.submitNewFeed = function () {
            feedListService.addFeed($scope.newFeed).success(function () {
                getCurrentFeedsPage();
            }).error(function () {

            });
        };

        activate();

        function activate() {
        }

        function getCurrentFeedsPage() {
            feedListService.getFeeds($scope.pageNumber).success(function (feeds) {
                $scope.feeds = feeds;
            });
        }
    }
})();
