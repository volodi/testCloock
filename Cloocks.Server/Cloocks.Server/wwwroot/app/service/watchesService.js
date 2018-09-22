(function () {
    angular.module('cloocks')
        .service('cloocksService', cloocksService);
    

    function cloocksService($http) {
        var service = this;
        debugger;
        service.get = function (skip) {
            return $http.get('/api/watches', { params: {page : skip}}).then(function (answer) {
                return answer.data;
            });
        };

        

    }



})();