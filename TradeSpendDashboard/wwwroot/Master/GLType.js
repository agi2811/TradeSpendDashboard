var table;
var totalItems = 0;
var _URL = baseUrl + "GLType";
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
                title: "GL Type",
                showTitle: true,
                width: 500,
                height: 'auto',
            },
            form: {
                items: [{
                    itemType: 'group',
                    colCount: 1,
                    colSpan: 2,
                    items: ['type'],
                }]
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
            { caption: "Type", dataField: "type", alignment: "left", sortIndex: 0, sortOrder: "asc" },
        ],
    }).dxDataGrid("instance");

})