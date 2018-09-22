(function () { 
    angular.module('cloocks')
        .component('modal', {
            controller: modalController,
            templateUrl: '/app/watches/modalWatchs.html',
            bindigs: { resolve: '<', close: '&', dismiss:'&'}

        });

    function modalController() {
        var ctrl = this;



    }

    
})();