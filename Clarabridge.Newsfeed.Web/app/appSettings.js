(function () {
    'use strict';

    angular
        .module('app')
        .constant('appSettings', {
            apiUrl: 'http://localhost:61664/api/feeds/'
        });
})();
