(function () {
    'use strict';

    angular
        .module('app')
        .controller('feedListController', feedListController);

    feedListController.$inject = ['$location', '$scope', 'feedListService'];

    function feedListController($location, $scope, feedListService) {
        // display feed page
        $scope.feeds = {};
        $scope.hasStatus = false;
        $scope.status = '';
        $scope.feedFullText;
        $scope.showDetailsId = 0;
        $scope.pageSizeOptions = ['10', '20', '30'];
        $scope.selectedPageSize = $scope.pageSizeOptions[1]; // set 20 as the default
        $scope.pageNumber = 1;

        getFeeds();
        
        $scope.hasPrevious = function () { return $scope.pageNumber > 1 };
        $scope.hasNext = function () { return true; 
            // lets assume there is always a next page 
            // don't want to track the number of pages - may involve table scan on database, unless we have a cache to keep track of count
        }

        $scope.nextPage = function () {
            getFeeds($scope.pageNumber + 1);
        }

        $scope.prevPage = function () {
            if ($scope.pageNumber > 1) {
                getFeeds($scope.pageNumber - 1);
            }
        }

        $scope.pageSizeChange = function () {
            getFeeds($scope.pageNumber);
        }

        $scope.submitNewFeed = function () {
           
            feedListService.addFeed($scope.newFeed).then(
                function (response) {
                    getFeeds($scope.pageNumber);
                    $scope.newFeed = '';
                    $scope.feedsForm.$setPristine();
                },
                function (error) {
                    $scope.formStatus = 'Failed';
                    $scope.formStatusValue = 'Failed to add new feed :' + error.message;
                });
        };

        // display details
        $scope.showDetails = function (feedId) {
            feedListService.getFeed(feedId).then(function (feed) {
                $scope.feedFullText = feed;
                $scope.showDetailsId = feedId;
            }, function (error) {
                $scope.hasStatus = true;
                $scope.status = 'Error getting detail news item: ' + error.statusText;
            });
        };

        $scope.hideDetails = function () {
            $scope.feedFullText = {};
            $scope.showDetailsId = 0;
        };


        // add new feed
        function getFeeds(pageNumber, pageSize) {
            if (pageNumber === undefined)
                pageNumber = $scope.pageNumber;
            if (pageSize === undefined)
                pageSize = $scope.selectedPageSize;

            $scope.hasStatus = true;
            $scope.status = 'Loading...';

            feedListService.getFeeds(pageNumber, pageSize).then(function (feeds) {
                if (feeds.length <= 0) {
                    $scope.hasStatus = true;
                    if (pageNumber == 1) 
                        $scope.status = 'No news feed found.';
                    else 
                        $scope.status = 'No more news feed found.';
                }
                else {
                    $scope.feeds = feeds;
                    $scope.pageNumber = pageNumber;
                    $scope.hasStatus = false;
                    $scope.feedFullText = '';
                    $scope.showDetailsId = 0;
                }
            },
            function (error) {
                $scope.hasStatus = true;
                $scope.status = 'Error Getting feeds : ' + error.statusText;
            });
        }

        activate();

        function activate() {
        }
    }
})();
