﻿var table;
var totalItems = 0;
var categoryId = 0;
var _URL = baseUrl + "CategoryMapping";
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
        onEditorPreparing(e) {
            //if (e.parentType == 'dataRow' && (e.dataField == 'CategoryId')) {
            //    e.editorOptions.onValueChanged = function (args) {
            //        categoryId = args.value;
            //        e.component.cellValue(e.row.rowIndex, 2, args.value);
            //        e.component.cellValue(e.row.rowIndex, 3, args.value);
            //    }
            //}

            if (e.parentType === 'dataRow' && e.dataField === 'ProfitCenterId') {
                e.editorOptions.disabled = true;//(typeof e.row.data.CategoryId !== 'number');
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
                title: "Category Mapping",
                showTitle: true,
                width: 700,
                height: 'auto',
            },
            form: {
                items: [
                    {
                        dataField: "CategoryWeb",
                        validationRules: [{ type: "required" }]
                    },
                    {
                        dataField: "CategoryId",
                        validationRules: [{ type: "required" }]
                    },
                    {
                        dataField: "ProfitCenterId",
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
            { caption: "Category Web", dataField: "CategoryWeb", alignment: "left", sortIndex: 0, sortOrder: "asc" },
            {
                caption: "Category",
                dataField: "CategoryId",
                setCellValue(rowData, value) {
                    categoryId = value;
                    rowData.CategoryId = value;
                    rowData.ProfitCenterId = value;
                },
                lookup: {
                    dataSource: customStoreCategory,
                    valueExpr: "Id",
                    displayExpr: "Category",
                    searchEnabled: true,
                },
            },
            {
                caption: "Profit Center",
                dataField: "ProfitCenterId",
                lookup: {
                    dataSource: customStoreProfitCenter,
                    valueExpr: "Id",
                    displayExpr: "ProfitCenter",
                    searchEnabled: true,
                },
            },
        ],
    }).dxDataGrid("instance");

})

var customStoreCategory = new DevExpress.data.CustomStore({
    key: "Id",
    load: function (loadOptions) {
        search = loadOptions.searchValue == null ? "" : loadOptions.searchValue;
        return loadData(_URL + "/GetCategoryOption?search=" + search, loadOptions);
    },
    byKey: function (key) {
        var d = new $.Deferred();
        $.get(_URL + "/GetCategoryOptionById?search=" + key)
            .done(function (dataItem) {
                d.resolve(dataItem.result);
            });
        return d.promise();
    }
})

var customStoreProfitCenter = new DevExpress.data.CustomStore({
    key: "Id",
    load: function (loadOptions) {
        search = loadOptions.searchValue == null ? "" : loadOptions.searchValue;
        return loadData(_URL + "/GetProfitCenterOptionByCategoryId?search=" + search + "&categoryId=" + categoryId, loadOptions);
    },
    byKey: function (key) {
        var d = new $.Deferred();
        $.get(_URL + "/GetProfitCenterOptionByCategoryId?categoryId=" + key)
            .done(function (dataItem) {
                d.resolve(dataItem.result);
            });
        return d.promise();
    }
})