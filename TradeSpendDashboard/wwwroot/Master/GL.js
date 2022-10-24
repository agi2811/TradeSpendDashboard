var table;
var totalItems = 0;
var _URL = baseUrl + "GL";
var _URL_PARAM = baseUrl + "Parameter";

var customStore = new DevExpress.data.CustomStore({
    key: "id",
    load: function (loadOptions) {
        var loadUrl = _URL + "/GetData";
        return loadData(loadUrl, loadOptions);
    },
    insert: function (values) {
        return sendRequest(_URL + "/Insert", "POST", values);
    },
    update: function (key, values) {
        return sendRequest(_URL + "/Update", "POST", values);
    },
    remove: function (key) {
        return sendRequest(_URL + "/Delete", "POST", { Id: key });
    },
    byKey: function (key) {
        var d = new $.Deferred();
        $.get(baseUrl + "/GetById?Id=" + key)
            .done(function (dataItem) {
                d.resolve(dataItem);
            });
        return d.promise();
    }
})

$(function () {
    var now = new Date();
    var date = now.toISOString();
    dataDate = moment(now).format("YYYY-MM-DD");

    table = $("#gridContainer").dxDataGrid({
        dataSource: customStore,
        //dataSource: customStore, //Data,
        remoteOperations: false,
        onRowUpdating: function (e) {
            $.extend(e.newData, $.extend({}, e.oldData, e.newData));
        },
        onToolbarPreparing: function (e) {
            var dataGrid = e.component;
            e.toolbarOptions.items.unshift(
                {
                    location: 'before',
                    template() {
                        return $('<div>')
                            .addClass('informers')
                            .append(
                                $('<h2>')
                                    .addClass('count')
                                    .text(totalItems),
                                $('<span>')
                                    .addClass('name')
                                    .text('Total Items'),
                            );
                    },
                },
                {
                    location: "after",
                    onClick: function (e) {
                        $("#gridContainer").dxDataGrid("addRow");
                    },
                    template: function () {
                        var htm = "<button class='btn btn-outline-primary' title='Add Data'><i class='fa fa-plus'></i></button>";
                        return $("<div/>").addClass("informer").append(htm);
                    }
                },
                {
                    location: "after",
                    onClick: function (e) {
                        dataGrid.getDataSource().reload();
                        $('.informers .count').text(totalItems);
                    },
                    template: function () {
                        var htm = "<button class='btn btn-outline-primary' title='Refresh All'><i class='fa fa-sync'></i></button>";
                        return $("<div/>").addClass("informer").append(htm);
                    }
                }
            )
        },
        onContentReady: function (e) {
            let total = e.component.getDataSource().totalCount();
            totalItems = parseFloat(total).toLocaleString(window.document.documentElement.lang);
            $('.informers .count').text(totalItems);
        },
        headerFilter: {
            visible: true
        },
        filterRow: {
            visible: true,
            applyFilter: "auto"
        },
        searchPanel: {
            visible: false
        },
        showBorders: true,
        showColumnLines: true,
        showRowLines: true,
        paging: {
            pageSize: 10
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [10, 20, 50, 100],
            showInfo: true
        },
        loadPanel: {
            enabled: true
        },
        sorting: {
            mode: "multiple"
        },
        editing: {
            mode: "popup",
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: false,
            confirmDelete: true,
            useIcons: true,
            popup: {
                title: "GL",
                showTitle: true,
                width: 700,
                height: 'auto',
            },
            form: {
                items: [
                    {
                        dataField: "glCode",
                        validationRules: [{ type: "required" }]
                    },
                    //{
                    //    dataField: "glName",
                    //    validationRules: [{ type: "required" }]
                    //},
                    {
                        dataField: "glDescription",
                        validationRules: [{ type: "required" }]
                    },
                    {
                        dataField: "typeId",
                        validationRules: [{ type: "required" }]
                    }
                ]
            }
        },
        columns: [
            {
                caption: "Action",
                type: "buttons",
                buttons: [
                    "edit",
                    "delete"
                ],
                fixed: true,
                fixedPosition: "left"
            },
            { caption: "GL Code", dataField: "glCode", alignment: "left", sortIndex: 0, sortOrder: "asc" },
            //{ caption: "GL Name", dataField: "glName", alignment: "left" },
            { caption: "GL Description", dataField: "glDescription", alignment: "left" },
            {
                caption: "GL Type", dataField: "typeId",
                lookup: {
                    dataSource: customStoreType,
                    valueExpr: 'Id',
                    searchEnabled: true,
                    displayExpr: function (item) {
                        return item.Type;
                    },
                }
            },
        ],
    }).dxDataGrid("instance");

})

var customStoreType = new DevExpress.data.CustomStore({
    key: "Id",
    load: function (loadOptions) {
        search = loadOptions.searchValue == null ? "" : loadOptions.searchValue;
        return loadData(_URL + "/GetGLTypeOption?search=" + search, loadOptions);
    },
    byKey: function (key) {
        var d = new $.Deferred();
        $.get(_URL + "/GetGLTypeOptionById?search=" + key)
            .done(function (dataItem) {
                d.resolve(dataItem.result);
            });
        return d.promise();
    }
})