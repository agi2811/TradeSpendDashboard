var table;
var totalItems = 0;
var customerId = 0;
var _URL = baseUrl + "CustomerLevel";
var _URL_PARAM = baseUrl + "Parameter";

var customStore = new DevExpress.data.CustomStore({
    key: "Id",
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
        return sendRequest(_URL + "/Delete", "POST", { id: key });
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
    table = $("#gridContainer").dxDataGrid({
        dataSource: customStore,
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
        onEditorPreparing(e) {
            if (e.parentType === 'dataRow' && e.dataField === 'OldChannelId') {
                e.editorOptions.disabled = true;//(typeof e.row.data.CustomerId !== 'number');
            }

            if (e.parentType === 'dataRow' && e.dataField === 'NewChannelId') {
                e.editorOptions.disabled = true;//(typeof e.row.data.CustomerId !== 'number');
            }
        },
        editing: {
            mode: "popup",
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: false,
            confirmDelete: true,
            useIcons: true,
            popup: {
                title: "Customer Level",
                showTitle: true,
                width: 700,
                height: 'auto',
            },
            form: {
                items: [
                    {
                        dataField: "CustomerLevel1",
                        validationRules: [{ type: "required" }]
                    },
                    {
                        dataField: "CustomerId",
                        validationRules: [{ type: "required" }]
                    },
                    {
                        dataField: "NewChannelId",
                        validationRules: [{ type: "required" }]
                    },
                    {
                        dataField: "OldChannelId",
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
            { caption: "Customer Level", dataField: "CustomerLevel1", alignment: "left", sortIndex: 0, sortOrder: "asc" },
            {
                caption: "Customer",
                dataField: "CustomerId",
                setCellValue(rowData, value) {
                    customerId = value;
                    rowData.CustomerId = value;
                    rowData.OldChannelId = value;
                    rowData.NewChannelId = value;
                },
                lookup: {
                    dataSource: customStoreCustomer,
                    valueExpr: "Id",
                    displayExpr: "Customer",
                    searchEnabled: true,
                },
            },
            {
                caption: "New Channel",
                dataField: "NewChannelId",
                lookup: {
                    dataSource: customStoreNewChannel,
                    valueExpr: "Id",
                    displayExpr: "Channel",
                    searchEnabled: true,
                },
            },
            {
                caption: "Old Channel",
                dataField: "OldChannelId",
                lookup: {
                    dataSource: customStoreOldChannel,
                    valueExpr: "Id",
                    displayExpr: "Channel",
                    searchEnabled: true,
                },
            },
        ]
    }).dxDataGrid("instance");
})

var customStoreCustomer = new DevExpress.data.CustomStore({
    key: "Id",
    load: function (loadOptions) {
        search = loadOptions.searchValue == null ? "" : loadOptions.searchValue;
        return loadData(_URL + "/GetCustomerOption?search=" + search, loadOptions);
    },
    byKey: function (key) {
        var d = new $.Deferred();
        $.get(_URL + "/GetCustomerOptionById?search=" + key)
            .done(function (dataItem) {
                d.resolve(dataItem.result);
            });
        return d.promise();
    }
})

var customStoreOldChannel = new DevExpress.data.CustomStore({
    key: "Id",
    load: function (loadOptions) {
        search = loadOptions.searchValue == null ? "" : loadOptions.searchValue;
        return loadData(_URL + "/GetOldChannelOptionByCustomerId?search=" + search + "&customerId=" + customerId, loadOptions);
    },
    byKey: function (key) {
        var d = new $.Deferred();
        $.get(_URL + "/GetOldChannelOptionByCustomerId?customerId=" + key)
            .done(function (dataItem) {
                d.resolve(dataItem.result);
            });
        return d.promise();
    }
})

var customStoreNewChannel = new DevExpress.data.CustomStore({
    key: "Id",
    load: function (loadOptions) {
        search = loadOptions.searchValue == null ? "" : loadOptions.searchValue;
        return loadData(_URL + "/GetNewChannelOptionByCustomerId?search=" + search + "&customerId=" + customerId, loadOptions);
    },
    byKey: function (key) {
        var d = new $.Deferred();
        $.get(_URL + "/GetNewChannelOptionByCustomerId?customerId=" + key)
            .done(function (dataItem) {
                d.resolve(dataItem.result);
            });
        return d.promise();
    }
})