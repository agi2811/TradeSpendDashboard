function DateTimeFormat(value, row, index) {
    if (value != null) return moment(value).format("DD MMM YYYY  h:mm:ss a");
    return "-";
}

$(".required").append("&nbsp;<span class='text-danger'>*</span>");

var loadingMessage = "Please wait, Processing your data ...";

function sendRequest(url, method, data) {
    var d = $.Deferred();
    method = method || "GET";
    logRequest(method, url, data);

    $.ajax(url, {
        method: method || "GET",
        data: data,
        cache: false,
        xhrFields: { withCredentials: true }
    }).done(function (result) {
        var type = "";
        type = !result.status ? "error" : result.status;
        type = (type.toLowerCase() !== "success") ? "error" : "success"

        if (result.result) {

            Swal.fire(
                "Success",
                result.result,
                type
            )
        }
        d.resolve(method === "GET" ? result.data : data);
    }).fail(function (xhr) {

        Swal.fire(
            "Error",
            xhr.responseJSON.result,
            "error"
        )
        d.reject();
        //d.reject(xhr.responseJSON ? xhr.responseJSON.Message : xhr.statusText);
    });

    return d.promise();
}

function logRequest(method, url, data) {
    var args = Object.keys(data || {}).map(function (key) {
        return key + "=" + data[key];
    }).join(" ");

    var logList = $("#requests ul"),
        time = DevExpress.localization.formatDate(new Date(), "HH:mm:ss"),
        newItem = $("<li>").text([time, method, url.slice(URL.length), args].join(" "));

    logList.prepend(newItem);
}

function getCustomStore(loadUrl, insertUrl, updateUrl, deleteUrl) {
    var customStore = new DevExpress.data.CustomStore({
        key: "Id",
        load: function (loadOptions) {
            return loadData(loadUrl, loadOptions);
        },
        insert: function (values) {
            return sendRequest(insertUrl, "POST", values);
        },
        update: function (key, values) {
            return sendRequest(updateUrl + "/" + key, "POST", values);
        },
        remove: function (key) {
            return sendRequest(deleteUrl + parseInt(key), "POST", "");
        }
    });
    return customStore;
}

function getCustomStoreFdManager(loadUrl, insertUrl, updateUrl, deleteUrl) {
    var customStore = new DevExpress.data.CustomStore({
        key: "Id",
        load: function (loadOptions) {
            return loadData(loadUrl, loadOptions);
        },
        insert: function (values) {
            var data = [];
            data.push(values);
            var items = {};
            items.item = JSON.stringify(data);
            return sendRequest(insertUrl, "POST", items);
        },
        update: function (key, values) {
            values.Id = key;
            var data = [];
            data.push(values);
            var items = {};
            items.item = JSON.stringify(data);
            return sendRequest(updateUrl, "POST", items);
        },
        remove: function (key) {
            return sendRequest(deleteUrl, "POST", { Id: key });
        }
    })
    return customStore;
}

function getCustomStoreReport(loadUrl) {

    var customStore = new DevExpress.data.CustomStore({
        load: function (loadOptions) {

            return loadData(loadUrl, loadOptions);
        }
    });
    return customStore;
}

function loadData(_url, loadOptions) {
    var deferred = $.Deferred(),
        args = {};

    if (loadOptions) {
        var page = (loadOptions.take) ? (loadOptions.skip / loadOptions.take) + 1 : 1;

        if (typeof (loadOptions.sort) === "string") {
            args.sort = JSON.stringify([{ selector: loadOptions.sort, desc: false }]);
        } else {
            args.sort = JSON.stringify(loadOptions.sort);
        }

        if (loadOptions.filter) {
            args.filter = JSON.stringify(filterParse(loadOptions.filter));
        }


        if (loadOptions.searchExpr) {
            var searchFilter = [];
            searchFilter.push({
                filetrField: loadOptions.searchExpr,
                filterOperator: loadOptions.searchOperation,
                filterValue: loadOptions.searchValue
            });

            args.filter = JSON.stringify(searchFilter);
        }

        args.pageSize = loadOptions.take || 50;
        args.pageNumber = page;


    }

    $.ajax({
        url: _url,
        dataType: "json",
        data: args,
        cache: false,
        success: function (result) {
            var data = result.result;
            if (!result.isError) {
                result.totalCount = (!result.totalCount) ? 0 : result.totalCount;
                deferred.resolve(data, {
                    totalCount: result.totalCount,
                    //summary: result.summary,
                    //groupCount: result.groupCount
                });
            } else
                deferred.reject(result.message);
        },
        error: function () {
            deferred.reject("Data Loading Error");
        }
    });

    return deferred.promise();
}

function loadDataPagination(_url, loadOptions) {
    var deferred = $.Deferred(),
        args = {};

    if (loadOptions) {
        var page = (loadOptions.take) ? (loadOptions.skip / loadOptions.take) + 1 : 1;

        if (typeof (loadOptions.sort) === "string") {
            args.sort = JSON.stringify([{ selector: loadOptions.sort, desc: false }]);
        } else {
            args.sort = JSON.stringify(loadOptions.sort);
        }

        if (loadOptions.filter) {
            args.filter = JSON.stringify(filterParse(loadOptions.filter));
        }


        if (loadOptions.searchExpr) {
            var searchFilter = [];
            searchFilter.push({
                filetrField: loadOptions.searchExpr,
                filterOperator: loadOptions.searchOperation,
                filterValue: loadOptions.searchValue
            });

            args.filter = JSON.stringify(searchFilter);
        }

        args.pageSize = loadOptions.take || 50;
        args.pageNumber = page;
    }

    $.ajax({
        url: _url,
        dataType: "json",
        data: args,
        cache: false,
        success: function (result) {
            var data = result.result.result;
            if (!result.isError) {
                result.totalCount = (!result.totalCount) ? 0 : result.totalCount;
                deferred.resolve(data, {
                    totalCount: result.totalCount,
                });
            } else
                deferred.reject(result.message);
        },
        error: function () {
            deferred.reject("Data Loading Error");
        }
    });

    return deferred.promise();
}

function validateNumber(e) {
    if (e.value < 0) {
        return false;
    }
    else {
        return true;
    }
}

function requestAjax(url, method, data) {
    var d = $.Deferred();
    method = method || "GET";
    logRequest(method, url, data);

    $.ajax(url, {
        method: method || "GET",
        data: data,
        cache: false,
        xhrFields: { withCredentials: true }
    }).done(function (result) {
        var type = "";
        type = !result.status ? "error" : result.status;
        type = (type.toLowerCase() !== "success") ? "error" : "success"
        d.resolve(method === "GET" ? result.data : data);
    }).fail(function (xhr) {
        d.reject(xhr.responseJSON ? xhr.responseJSON.Message : xhr.statusText);
    });

    return d.promise();
}

var loadPanel = $(".loadpanel").dxLoadPanel({
    shadingColor: "rgba(0,0,0,0.4)",
    //position: { of: "#employee" },
    visible: false,
    showIndicator: true,
    showPane: true,
    shading: true,
    width: 400,
    message: loadingMessage,
    closeOnOutsideClick: false,
    onShown: function () {
        setTimeout(function () {
            loadPanel.hide();
        }, 50000);
    },
    onHidden: function () {
        //showEmployeeInfo(employee);
    }
}).dxLoadPanel("instance");

function filterParse(_filters) {
    var filter = [];
    if (typeof (_filters[0]) === "object") {
        $.each(_filters, function (i, dataFilter) {
            if (typeof (dataFilter) === "object") {
                if (typeof (dataFilter[0]) !== "object") {
                    filter.push({
                        filterField: dataFilter[0],
                        filterOperator: dataFilter[1],
                        filterValue: dataFilter[2]
                    });
                }
            }
        });
    } else {
        filter.push({
            filterField: _filters[0],
            filterOperator: _filters[1],
            filterValue: _filters[2]
        });
    }
    //debugger;
    return filter;
}

var $loadIndicator = $("<div>").dxLoadIndicator({ visible: false });