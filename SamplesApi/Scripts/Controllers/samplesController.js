﻿(function () {
    'use strict';

    angular
        .module('samplesApp')
        .controller('samplesController', samplesController);

    samplesController.$inject = ['$scope', 'Samples'];

    var showMessage = function (message) {
        alert(message);
    }

    function samplesController($scope, Samples) {
        var getAllSamples = function () {
            var onError = function (err) {
                showMessage('There was a problem getting all of the samples: ' + err.data);
            }
            $scope.Samples = Samples.AllSamples.query({}, function () { }, onError);
        }

        $scope.$watch('search', function (newValue, oldValue) {
            if (newValue === '' || newValue === null) {
                getAllSamples();
                return;
            }
            if(newValue !== oldValue)
                getSamplesByUser(newValue);
        });

        var getSamplesByUser = function (userName) {
            var onError = function (err) {
                showMessage('There was a problem getting the samples by user: ' + err.data);
            }
            $scope.Samples = Samples.SamplesByUser.query({ nameToSearch: userName }, function () { }, onError);
        }

        $scope.select = function () {
            this.setSelectionRange(0, this.value.length);
        }

        $scope.statusChanged = function (statusId) {
            var onError = function (err) {
                showMessage('There was an error getting the samples by status: ' + err.data);
            }

            if (statusId === null)
                getAllSamples();
            else
                $scope.Samples = Samples.SamplesByStatus.query({ status: statusId }, function () { }, onError);
        }

        $scope.save = function () {
            var onSuccess = function () {
                $scope.clear();
                getAllSamples();
                showMessage('Saved');
            }

            var onError = function (err) {
                showMessage('There was an error saving the sample: ' + err.data);
            }

            if ($scope.NewSample.Barcode !== '' && $scope.NewSample.CreatedBy !== '' && $scope.NewSample.Status !== '')
                Samples.CreateSample.create({ barcode: $scope.NewSample.Barcode, createdBy: $scope.NewSample.CreatedBy, status: $scope.NewSample.Status }, onSuccess, onError);
            else
                $scope.NewSample.HasError = true;
        }

        $scope.NewSample = {
            Barcode: '',
            CreatedBy: '',
            Status: '',
            HasError: false
        };

        $scope.clear = function () {
            $scope.NewSample.Barcode = '';
            $scope.NewSample.CreatedBy = '';
            $scope.NewSample.Status = '';
            $scope.NewSample.HasError = false;
        }

        getAllSamples();
        $scope.Statuses = Samples.SampleStatuses.query();
        $scope.Users = Samples.Users.query();
    }
})();