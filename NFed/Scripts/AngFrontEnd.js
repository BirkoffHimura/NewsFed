var app = angular.module('NFedApp', ['angularUtils.directives.dirPagination']).directive('ngFiles', ['$parse', function ($parse) {
    function fn_link(scope, element, attrs) {
        var onChange = $parse(attrs.ngFiles);
        element.on('change', function (event) {
            onChange(scope, { $files: event.target.files });
        });
    };

    return {
        link: fn_link
    }
}]);


app.controller('NewsFeedItems', function ($scope, $http) {
   
    var formdata = new FormData();
    $scope.getTheFiles = function ($files) {
        angular.forEach($files, function (value, key) {
            formdata.append(key, value);
        });
    };

    $scope.refreshNews = function () {
        $("#loading").show();
        var request = {
            method: 'POST',
            url: '/NewsFeedItems/GetNewsFeedItems/',
            data: formdata,
            headers: {
                'Content-Type': undefined
            },
            params: {}
        };

        $http(request).then(function (response) {
            $scope.NewsFeedItemList = response.data;
            $("#loading").hide();
        });
        //$http.get('/NewsFeedItems/GetNewsFeedItems').then(function (response) {
        //    delete $scope.NewsFeedItemList;
        //    $scope.NewsFeedItemList = response.data;
        //    $("#loading").hide();
        //});
    }

    $scope.GetAllNewsItems = function () {
        $("#loading").show();
        var request = {
            method: 'POST',
            url: '/NewsFeedItems/GetNewsFeedItems/',
            data: formdata,
            headers: {
                'Content-Type': undefined
            },
            params: {}
        };

        $http(request).then(function (response) {
            $scope.NewsFeedItemList = response.data;
            $("#loading").hide();
        });
        //$http.get('/NewsFeedItems/GetNewsFeedItems').then(function (response) {
        //    delete $scope.NewsFeedItemList;
        //    $scope.NewsFeedItemList = response.data;
        //    $("#loading").hide();
        //});
        $scope.$broadcast('GetProfileInfoCurrentUser', { ID: "" });
    }

    $scope.GetNewsFeedItemsSubscriptions = function () {
        var request = {
            method: 'POST',
            url: '/NewsFeedItems/GetNewsFeedItemsSubscriptions/',
            data: formdata,
            headers: {
                'Content-Type': undefined
            },
            params: {}
        };

        $http(request).then(function (response) {
            $scope.NewsFeedItemList = response.data;
            
        });
        //$http.get('/NewsFeedItems/GetNewsFeedItemsSubscriptions').then(function (response) {
        //    delete $scope.NewsFeedItemList;
        //    $scope.NewsFeedItemList = response.data;
        //});
        $scope.$broadcast('GetProfileInfoCurrentUser', { ID: "" });
    }

    $scope.GetMyNewsFeedItems = function () {
        var request = {
            method: 'POST',
            url: '/NewsFeedItems/GetNewsFeedItems/',
            data: formdata,
            headers: {
                'Content-Type': undefined
            },
            params: {}
        };

        $http(request).then(function (response) {
            $scope.NewsFeedItemList = response.data;
        });
        //$http.get('/NewsFeedItems/GetMyNewsFeedItems').then(function (response) {
        //    delete $scope.NewsFeedItemList;
        //    $scope.NewsFeedItemList = response.data;
        //});
        $scope.$broadcast('GetProfileInfoCurrentUser', { ID: "" });
    }

    $scope.GetNewsFeedItemsByUserID = function (uid) {
        var request = {
            method: 'POST',
            url: '/NewsFeedItems/GetNewsFeedItemsByUserID/',
            data: formdata,
            headers: {
                'Content-Type': undefined
            },
            params: { ID: uid }
        };

        $http(request).then(function (response) {
            delete $scope.NewsFeedItemList;
            $scope.NewsFeedItemList = response.data;
        });
        $scope.$broadcast('GetProfileInfoByID', { ID: uid });

        
    }

    $scope.NewsFeedItemSearch = function (keyEvent) {        
        if (keyEvent.which === 13) {
            //alert('Executing now...');
            
            //$("#Subject").val() + '/' + $("#Post_Body").val()
            //alert($("#NewsFeedItem_Title").val() + ':' + $("#NewsFeedItem_Body").val());
            var request = {
                method: 'POST',
                url: '/NewsFeedItems/ItemSearch/',
                data: formdata,
                headers: {
                    'Content-Type': undefined
                },
                params: { criteria: $("#NewsFeedItemSearchCriteria").val() }
            };

            $http(request).then(function (response) {
                //alert('returned');
                $("#NewsFeedItemSearchCriteria").val("");
                delete $scope.NewsFeedItemList;
                $scope.NewsFeedItemList = response.data;
            });
        }
    }

    $scope.CreateNewsFeedItem = function () {
        var request = {
            method: 'POST',
            url: '/NewsFeedItems/CreateNewsFeedItem/',
            data: formdata,
            headers: {
                'Content-Type': undefined
            },
            params: { NewsFeedItem_Title: $("#NewsFeedItem_Title").val(), NewsFeedItem_Body: $("#NewsFeedItem_Body").val() }
        };

        $http(request).then(function (response) {
            $("#NewsFeedItem_Title").val("");
            $("#NewsFeedItem_Body").val("");
            $("#imgUpload").val("");
            delete $scope.NewsFeedItemList;
            $scope.NewsFeedItemList = response.data;
            document.getElementById('files').value = "";
        });
    }

    $scope.CreateNewsFeedItemComment = function (keyEvent, nfi, cmt) {
        if (keyEvent.which === 13) {

            var cmt_val = $(cmt).val();
            
            //nfi.NewsFeedItemComments
            var request = {
                method: 'POST',
                url: '/NewsFeedItems/CreateNewsFeedItemComment/',
                data: formdata,
                headers: {
                    'Content-Type': undefined
                },
                params: { NewsFeedItem_ID: nfi.ID, NewsFeedItem_Comment: cmt_val }
            }

            $http(request).then(function (response) {
                var myObj = response.data;
                if (myObj != null && myObj != "") {

                    $(cmt).val("");

                    var tmp = $scope.NewsFeedItemList;
                    for (var i in tmp) {
                        if (tmp[i].ID == nfi.ID) {
                            tmp[i].NewsFeedItemComments.push(myObj);
                        }
                    }
                } else {
                    alert('There was a problem posting comment, please try again later');
                }
            });
        }
    }
});


app.controller('UserProfileBoxController', function ($scope, $http) {
    
    $scope.updateBoxInfo = function () {
        var request = {
            method: 'POST',
            url: '/UserProfileBox/GetInfo/',
            data: formdata,
            headers: {
                'Content-Type': undefined
            },
            params: {  }
        };

        $http(request).then(function (response) {
            $scope.UserProfileBoxInfo = response.data;
        });

    }
    var formdata = new FormData();

    $scope.$on('GetProfileInfoByID', function (event, args) {
        
        var request = {
            method: 'POST',
            url: '/UserProfileBox/GetInfoByID/',
            data: formdata,
            headers: {
                'Content-Type': undefined
            },
            params: { ID: args.ID }
        };

        $http(request).then(function (response) {
            $scope.UserProfileBoxInfo = response.data;
        });
    });

    $scope.$on('GetProfileInfoCurrentUser', function (event, args) {
        var request = {
            method: 'POST',
            url: '/UserProfileBox/GetInfo/',
            data: formdata,
            headers: {
                'Content-Type': undefined
            },
            params: {}
        };

        $http(request).then(function (response) {
            $scope.UserProfileBoxInfo = response.data;
            $scope.currentUser = response.data;
        });
    });

});

app.controller('SuggestionsController', function ($scope, $http) {
    //$http.get('/Suggestions/GetList').then(function (response) {
    //    delete $scope.WhotoFollow;
    //    $scope.WhotoFollow = response.data;
    //});

    var formdata = new FormData();
    $scope.getSuggestionList = function () {
        var request = {
            method: 'POST',
            url: '/Suggestions/GetList/',
            data: formdata,
            headers: {
                'Content-Type': undefined
            },
            params: {}
        };

        $http(request).then(function (response) {
            $scope.WhotoFollow = response.data;
        });
    }

    $scope.Subscribe = function (uid, btn) {
        $(btn).hide();
        
        //alert(uid);
        var request = {
            method: 'POST',
            url: '/Suggestions/Subscribe/',
            data: formdata,
            headers: {
                'Content-Type': undefined
            },
            params: { Id: uid }
        };

        $http(request).then(function (response) {
            //alert('returned');
            
            //$scope.NewsFeedItemList = response.data;
        });
    }
});

app.controller('MySubscriptionController', function ($scope, $http) {
    

    var formdata = new FormData();
    $scope.getSubList = function () {
        var request = {
            method: 'POST',
            url: '/MySubscription/GetList/',
            data: formdata,
            headers: {
                'Content-Type': undefined
            },
            params: {  }
        };

        $http(request).then(function (response) {            
            $scope.SubscriptionList = response.data;
        });
    }
    $scope.UnSubscribe = function (uid, btn) {
        $(btn).hide();

        alert(uid);
        var request = {
            method: 'POST',
            url: '/MySubscription/UnSubscribe/',
            data: formdata,
            headers: {
                'Content-Type': undefined
            },
            params: { Id: uid }
        };

        $http(request).then(function (response) {
            $scope.SubscriptionList = response.data;
        });
    }
});