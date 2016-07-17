(function () {
    'use strict';

    angular
        .module('samplesApp')
        .controller('samplesController', samplesController);

    samplesController.$inject = ['$scope', 'Samples'];

    var showMessage = function (message) {
        alert(message);
    }

    function samplesController($scope, Samples) {
        var onSuccess = function () {
            setLoading(false);
        }

        var handleError = function (message) {
            setLoading(false);
            showMessage(message);
        }

        var setLoading = function (value) {
            $scope.IsLoading = value;
        }

        var getAllSamples = function () {
            setLoading(true)
            var onError = function (err) {
                handleError('There was a problem getting all of the samples: ' + err.data);
            }
            $scope.Samples = Samples.AllSamples.query({}, onSuccess, onError);
        }

        $scope.$watch('Search', function (newValue, oldValue) {
            if ($scope.ClearedByStatusChange) {
                $scope.ClearedByStatusChange = false;
                return;
            }

            $scope.SelectedStatus = null;
            if (newValue === '' || newValue === null) {
                getAllSamples();
                return;
            }
            if(newValue !== oldValue)
                getSamplesByUser(newValue);
        });

        var getSamplesByUser = function (userName) {
            var onError = function (err) {
                handleError('There was a problem getting the samples by user: ' + err.data);
            }
            $scope.Samples = Samples.SamplesByUser.query({ nameToSearch: userName }, onSuccess, onError);
        }

        $scope.select = function () {
            this.setSelectionRange(0, this.value.length);
        }

        $scope.statusChanged = function (statusId) {
            $scope.ClearedByStatusChange = true;
            $scope.Search = '';
            var onError = function (err) {
                handleError('There was an error getting the samples by status: ' + err.data);
            }

            if (statusId === null)
                getAllSamples();
            else
                $scope.Samples = Samples.SamplesByStatus.query({ status: statusId }, onSuccess, onError);
        }

        $scope.save = function () {
            var onSuccess = function () {
                $scope.clear();
                showMessage('Saved');
            }

            var onError = function (err) {
                handleError('There was an error saving the sample: ' + err.data);
            }

            var isSampleValid = function () {
                return $scope.NewSample.Barcode !== '' &&
                    $scope.NewSample.Barcode !== null &&
                    $scope.NewSample.Barcode !== undefined &&
                    $scope.NewSample.CreatedBy !== '' &&
                    $scope.NewSample.CreatedBy !== null &&
                    $scope.NewSample.CreatedBy !== undefined &&
                    $scope.NewSample.Status !== '' &&
                    $scope.NewSample.Status !== null &&
                    $scope.NewSample.Status !== undefined;
            }

            if (isSampleValid())
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
            $scope.Search = '';
            $scope.SelectedStatus = '';
            $scope.TableFilter = '';
            $scope.ClearedByStatusChange = false;
            getAllSamples();
        }

        getAllSamples();
        $scope.Statuses = Samples.SampleStatuses.query();
        $scope.Users = Samples.Users.query();
    }
})();