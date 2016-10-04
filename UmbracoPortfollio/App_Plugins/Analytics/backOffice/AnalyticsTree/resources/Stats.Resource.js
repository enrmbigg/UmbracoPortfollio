﻿
angular.module("umbraco.resources")
    .factory("statsResource", function ($http, settingsResource) {
        return {
            
            //TODO: Get Profile ID from saved profile in settings

            getlanguage: function (profileID, startDate, endDate) {
              
                return $http.get(settingsResource.getApiPath() + "Analytics/AnalyticsApi/GetLanguage", { params: { profile: profileID, startDate: startDate, endDate: endDate } });
            },

            getcountries: function (profileID, startDate, endDate) {
              
                return $http.get(settingsResource.getApiPath() + "Analytics/AnalyticsApi/GetCountry", { params: { profile: profileID, startDate: startDate, endDate: endDate } });
            },

            getbrowsers: function (profileID, startDate, endDate) {
              
                return $http.get(settingsResource.getApiPath() + "Analytics/AnalyticsApi/GetBrowser", { params: { profile: profileID, startDate: startDate, endDate: endDate } });
            },

            getbrowserspecifics: function (profileID, startDate, endDate) {
                
                return $http.get(settingsResource.getApiPath() + "Analytics/AnalyticsApi/GetBrowserVersion", { params: { profile: profileID, startDate: startDate, endDate: endDate } });
            },

            getdevicetypes: function (profileID, startDate, endDate) {
               
                return $http.get(settingsResource.getApiPath() + "Analytics/AnalyticsApi/GetDeviceTypes", { params: { profile: profileID, startDate: startDate, endDate: endDate } });
            },

            getdevices: function (profileID, startDate, endDate) {
               
                return $http.get(settingsResource.getApiPath() + "Analytics/AnalyticsApi/GetDevices", { params: { profile: profileID, startDate: startDate, endDate: endDate } });
            },

            getresolutions: function (profileID, startDate, endDate) {
              
                return $http.get(settingsResource.getApiPath() + "Analytics/AnalyticsApi/GetScreenRes", { params: { profile: profileID, startDate: startDate, endDate: endDate } });
            },

            getos: function (profileID, startDate, endDate) {
               
                return $http.get(settingsResource.getApiPath() + "Analytics/AnalyticsApi/GetOperatingSystems", { params: { profile: profileID, startDate: startDate, endDate: endDate } });
            },

            getosversions: function (profileID, startDate, endDate) {
               
                return $http.get(settingsResource.getApiPath() + "Analytics/AnalyticsApi//GetOperatingSystemVersions", { params: { profile: profileID, startDate: startDate, endDate: endDate } });
            },

            getsocialnetworks: function (profileID, startDate, endDate) {
               
                return $http.get(settingsResource.getApiPath() + "Analytics/AnalyticsApi/GetSocialNetworkSources", { params: { profile: profileID, startDate: startDate, endDate: endDate } });
            },

            getkeywords: function (profileID, startDate, endDate) {
                return $http.get(settingsResource.getApiPath() + "Analytics/AnalyticsApi/GetKeywords", { params: { profile: profileID, startDate: startDate, endDate: endDate } });
            },

            getvisits: function (profileID, startDate, endDate) {
                return $http.get(settingsResource.getApiPath() + "Analytics/AnalyticsApi/GetVisits", { params: { profile: profileID, startDate: startDate, endDate: endDate } });
            },

            getsources: function (profileID, startDate, endDate) {
               
                return $http.get(settingsResource.getApiPath() + "Analytics/AnalyticsApi/GetSources", { params: { profile: profileID, startDate: startDate, endDate: endDate } });
            },

            getvisitcharts: function (profileID, startDate, endDate) {
                return $http.get(settingsResource.getApiPath() + "Analytics/AnalyticsApi/GetVisitsOverTime", { params: { profile: profileID, startDate: startDate, endDate: endDate } });
            },

            gettransactions: function (profileID, startDate, endDate) {
                return $http.get(settingsResource.getApiPath() + "Analytics/AnalyticsApi/GetTransactions", { params: { profile: profileID, startDate: startDate, endDate: endDate } });
            },

            gettransactionscharts: function (profileID, startDate, endDate) {
                return $http.get(settingsResource.getApiPath() + "Analytics/AnalyticsApi/GetTransactionsOverTime", { params: { profile: profileID, startDate: startDate, endDate: endDate } });
            },

            getproductperformance: function (profileID, startDate, endDate) {
                return $http.get(settingsResource.getApiPath() + "Analytics/AnalyticsApi/GetProductPerformance", { params: { profile: profileID, startDate: startDate, endDate: endDate } });
            },

            getproductperformancecharts: function (profileID, startDate, endDate) {
                return $http.get(settingsResource.getApiPath() + "Analytics/AnalyticsApi/GetProductPerformanceOverTime", { params: { profile: profileID, startDate: startDate, endDate: endDate } });
            },

            getsalesperformance: function (profileID, startDate, endDate) {
                return $http.get(settingsResource.getApiPath() + "Analytics/AnalyticsApi/GetSalesPerformance", { params: { profile: profileID, startDate: startDate, endDate: endDate } });
            },

            getsalesperformancecharts: function (profileID, startDate, endDate) {
                return $http.get(settingsResource.getApiPath() + "Analytics/AnalyticsApi/GetSalesPerformanceOverTime", { params: { profile: profileID, startDate: startDate, endDate: endDate } });
            },

            getstoredetails: function (profileID, startDate, endDate) {
                return $http.get(settingsResource.getApiPath() + "Analytics/AnalyticsApi/GetStoreDetails", { params: { profile: profileID, startDate: startDate, endDate: endDate } });
            },

            getbestsellers: function (profileID, startDate, endDate) {
                return $http.get(settingsResource.getApiPath() + "Analytics/AnalyticsApi/GetBestSellers", { params: { profile: profileID, startDate: startDate, endDate: endDate } });
            },

            getrevenuepersource: function (profileID, startDate, endDate) {
                return $http.get(settingsResource.getApiPath() + "Analytics/AnalyticsApi/GetRevenuePerSource", { params: { profile: profileID, startDate: startDate, endDate: endDate } });
            }
        };
    });