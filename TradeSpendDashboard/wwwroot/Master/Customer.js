var table;
var totalItems = 0;
var _URL = baseUrl + "Customer";
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
        return sendRequest(_URL + "/ActivateData", "POST", { id: key });
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
        editing: {
            mode: "popup",
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: false,
            confirmDelete: true,
            useIcons: true,
            popup: {
                title: "Customer",
                showTitle: true,
                width: 700,
                height: 'auto',
            },
            form: {
                items: [
                    {
                        dataField: "customer",
                        validationRules: [{ type: "required" }]
                    },
                    //{
                    //    dataField: "customerMap",
                    //    validationRules: [{ type: "required" }]
                    //},
                    {
                        dataField: "newChannelId",
                        validationRules: [{ type: "required" }]
                    },
                    {
                        dataField: "oldChannelId",
                        validationRules: [{ type: "required" }]
                    }
                ]
            }
        },
        onCellPrepared: function (e) {
            if (e.rowType === "data" && e.column.caption == "Action") {
                if (e.row.data.isActive == false) {
                    e.cellElement.find(".dx-link").filter(".dx-link-delete").remove();
                }
            }
        },
        columns: [
            {
                caption: "Action",
                type: "buttons",
                buttons: [
                    "edit", "delete",
                    {
                        hint: "Activate",
                        icon: "tips",
                        visible: function (e) {
                            return !e.row.data.isActive;
                        },
                        onClick: function (e) {
                            var result = DevExpress.ui.dialog.confirm("Are you sure you want to activate this record?", "", false);
                            result.done(function (dialogResult) {
                                if (dialogResult) {
                                    //var index = e.row.rowIndex;
                                    //table.deleteRow(index);
                                    sendRequest(_URL + "/ActivateData", "POST", { id: e.row.data.id });
                                    var grid = $("#gridContainer").dxDataGrid('instance');
                                    DevExpress.ui.notify(grid.refresh(), "refresh", 100);
                                }
                            });
                        }
                    },
                ],
                fixed: true,
                fixedPosition: "left"
            },
            { caption: "Customer", dataField: "customer", alignment: "left", sortIndex: 0, sortOrder: "asc" },
            //{ caption: "Customer Map", dataField: "customerMap", alignment: "left" },
            {
                caption: "New Channel", dataField: "newChannelId",
                lookup: {
                    dataSource: customStoreChannel,
                    valueExpr: 'Id',
                    searchEnabled: true,
                    displayExpr: function (item) {
                        return item.Channel;
                    },
                }
            },
            {
                caption: "Old Channel", dataField: "oldChannelId",
                lookup: {
                    dataSource: customStoreChannel,
                    valueExpr: 'Id',
                    searchEnabled: true,
                    displayExpr: function (item) {
                        return item.Channel;
                    },
                }
            },
        ]
    }).dxDataGrid("instance");
})

var customStoreChannel = new DevExpress.data.CustomStore({
    key: "Id",
    load: function (loadOptions) {
        search = loadOptions.searchValue == null ? "" : loadOptions.searchValue;
        return loadData(_URL + "/GetChannelOption?search=" + search, loadOptions);
    },
    byKey: function (key) {
        var d = new $.Deferred();
        $.get(_URL + "/GetChannelOptionById?search=" + key)
            .done(function (dataItem) {
                d.resolve(dataItem.result);
            });
        return d.promise();
    }
})