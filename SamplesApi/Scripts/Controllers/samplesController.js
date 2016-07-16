(function () {
    'use strict';

    angular
        .module('samplesApp')
        .controller('samplesController', samplesController);

    samplesController.$inject = ['$scope', 'Samples'];

    function samplesController($scope, Samples) {
        var getAllSamples = function () {
            $scope.Samples = Samples.AllSamples.query();
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
            $scope.Samples = Samples.SamplesByUser.query({ nameToSearch: userName });
        }

        $scope.select = function () {
            this.setSelectionRange(0, this.value.length);
        }

        $scope.statusChanged = function (statusId) {
            if (statusId === null)
                getAllSamples();
            else
                $scope.Samples = Samples.SamplesByStatus.query({ status: statusId });
        }

        $scope.save = function () {
            var onSuccess = function () {
                alert('Saved');
            }

            var onError = function (err) {
                alert('There was an error saving the sample: ' + err.data);
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