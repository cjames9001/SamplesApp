(function () {
    'use strict';

    var samplesService = angular.module('samplesService', ['ngResource']);
    samplesService.factory('Samples', ['$resource',
        function ($resource) {
            return {
                AllSamples: $resource('/api/samples', {}, {
                    query: { method: 'GET', params: {}, isArray: true }
                }),
                SamplesByStatus: $resource('/api/samples/status/:status', {}, {
                    query: { method: 'GET', params: { status: '@status' }, isArray: true }
                }),
                SamplesByUser: $resource('/api/samples/createdbyname/:nameToSearch', {}, {
                    query: { method: 'GET', params: { nameToSearch: '@nameToSearch' }, isArray: true }
                }),
                SampleStatuses: $resource('/api/status', {}, {
                    query: { method: 'GET', params: {}, isArray: true }
                }),
                Users: $resource('/api/user', {}, {
                    query: { method: 'GET', params: {}, isArray: true }
                }),
                CreateSample: $resource('/api/samples/create/:barcode/:createdBy/:status', {}, {
                    create: { method: 'POST', params: { barcode: '@barcode', createdBy: '@createdBy', status: '@status' }, isArray: false }
                })
            };
        }
    ]);
})();