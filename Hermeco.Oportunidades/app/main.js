(function () {
    var app = angular.module('app', ["ngRoute"]);

    app.config(function ($routeProvider) {
        $routeProvider
            .when("/detail/:id", {
                templateUrl: "detail.html",
                controller: "detailCtrl"
            })
            .when("/apply/:id", {
                templateUrl: "Apply.html",
                controller: "detailCtrl"
            })
            .when("/success", {
                templateUrl: "Success.html"
            })
            .when("/", {
                templateUrl: "oportunities.html",
                controller: "Main"
            });
    });

    app.controller('Main', function ($scope, $http) {

        $scope.oportunidades = {};
        $http({
            method: 'GET',
            url: '/Hermeco.Oportunidades/api/Oportunidad'
        }).then(function (success) {
            $scope.oportunidades = success.data;
            $scope.cantidad = $scope.oportunidades.length;
        }, function (error) {
        });

    });

    app.controller("detailCtrl", function ($scope, $http, $routeParams) {
        $http({
            method: 'GET',
            url: '/Hermeco.Oportunidades/api/Oportunidad/' + $routeParams.id
        }).then(function (success) {
            $scope.oportunidad = success.data;
        }, function (error) {
            console.log(error);
        });

    });

    // grab an element
    var myElement = document.querySelector("header");
    // construct an instance of Headroom, passing the element
    var headroom = new Headroom(myElement);
    // initialise
    headroom.init();

    app.controller("ApplyController", function ($window, $scope, $http, $routeParams) {
        $scope.headerClass = '';
        $scope.showAlert = false;
        $scope.submit = function (Application) {
            console.log($scope.oportunidad);
            $scope.showAlert = false;
            var model = new FormData;
            model.append('IdRequisicion', $scope.oportunidad.Idrequisicion);
            model.append('Cargo', $scope.oportunidad.Cargo);
            model.append('Nombre', Application.Nombre);
            model.append('Telefono', Application.Telefono);
            model.append('Email', Application.Email);
            model.append('Doc', $(".file")[0].files[0]);
            $scope.showLoader = true;
            $scope.files = []; 
            $scope.jsonData = JSON.stringify(Application);
            $scope.files.push($(".file")[0].files[0]); 
            
            $http({
                url: '/Hermeco.Oportunidades/api/Aplicacion',
                method: 'POST',
                dataType: 'json',
                data: model,
                processData: false,
                contentType: false,
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
               

            }).then(function (success) {
                console.log(success);
                $scope.showLoader = false;
                $window.location.href = '#!/success';
            }, function (error) {
                console.log('error aqui' + error);
                $scope.showAlert = true;
                $scope.alertType = 'alert-danger';
                $scope.alertTitle = 'Ups';
                $scope.alertText = 'Se presentó un error al enviar tus datos.'
                $scope.showLoader = false;
            }).catch(function (e) {
                console.log(e);
            });
        };
    });
})();




