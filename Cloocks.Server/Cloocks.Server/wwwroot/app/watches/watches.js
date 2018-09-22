(function () {

    angular.module('cloocks', [])
        .component('cloocks', {
            controller: cloocksController,
            templateUrl: '/app/watches/watches.html'
        });

    function cloocksController(cloocksService, $uibModal) {
        var ctrl = this;
  
        cloocksService.get(0).then(function (data) {
             ctrl.cloocks = data;
        });

        ctrl.get = function () {
            cloocksService.get(ctrl.cloocks.length).then(function (data) {
  
                ctrl.cloocks.push(...data);
            });
        }

        ctrl.modal = function cloockFunction(clock) {
            debugger;
            //var a = document.getElementById("cloockDialog");
            //a.showModal();
            //var date = dateYearMonthDaata(new Date(clock.date));
            $uibModal.open({
                component: 'modal'
            });
            //a.innerHTML = $compile(`<modal clooks = ></modal>`)($scope);

            //a.innerHTML = `${clock.name}
            //          <br> ${clock.price} 
            //          <br> ${date}
            //          <br> ${clock.data}`;
        }

        function dateYearMonthDaata(date) {
            return `${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}`;
        }
    }



})();