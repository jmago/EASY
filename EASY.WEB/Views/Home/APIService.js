formApp.factory("conectionAPI", function ($http, $location) {
    _getUrl = function (action) {
        _url = $location.absUrl().split('/');
        url = '';
        for (var i = 0; i < _url.length - 1; i++) {
            url += _url[i] + '/';
        }
        return url + action;
    }

    var _open = function (action, id, param) {
        if (id)
            window.location = _getUrl(action) + '?' + action + 'ID=' + id;
        else if (param) {
            window.location = _getUrl(action) + '?' + param.Name + '=' + param.Value;
        }
        else
            window.location = _getUrl(action);
    };

    var _delete = function (param) {
        if (param) {
            $http.post(_getUrl('Delete'), param).then((res) => { _open('Index', null, { Name: 'message', Value: res.data.type + '|' + res.data.message }); });
        }
    };

    var _getData = function (action, data) {
        var thisURL = _getUrl(action);
        if (data !== undefined && data !== null) {
            return $http.get(thisURL, { params: data });
        } else
            return $http.get(thisURL);
    };

    var _sendData = function (action, data) {
        return $http.post(_getUrl(action), data);
    };

    return {
        getData: _getData,
        sendData: _sendData,
        delete: _delete,
        open: _open
    };
});