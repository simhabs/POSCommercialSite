﻿var myApp = angular.module('myApp', [])
var Mycntrl = myapp1.controller('Mycntrl', function ($scope, $http) {
    $scope.getdata = function () {
        $http.get('http://localhost:52800/api/resetpassword/GetResetPassword').then(function (res, data) {
            $scope.type = res.data;

            //alerts("hi");
        });
    }


    $scope.save = function (type) {

        var type = {

            Username: type.Username,
            OldPassword: type.OldPassword,
            NewPassword: type.NewPassword,
            ReEnterNewPassword: type.ReEnterNewPassword,

        };

        var req = {
            method: 'POST',
            url: 'http://localhost:52800/api/ValidateCredentials/savepassword',            
            //headers: {
            //    'Content-Type': undefined

            data: type


        }
        $http(req).then(function (response) {

        })
    }
});
