var table;
var treeList;
var totalItems = 0;
var RoleId = 0;
var flagCategory = null;
var itemsCategory = [];
var itemsProfitCenter = [];
var _URL = baseUrl + "Users";
var _URL_BOMAP = baseUrl + "BudgetOwnerMapping";
var _URL_PARAM = baseUrl + "Parameter";

var customStore = new DevExpress.data.CustomStore({
    key: "Id",
    load: function (loadOptions) {
        var loadUrl = _URL + "/GetData";
        return loadData(loadUrl, loadOptions);
    },
    insert: function (values) {
        var sendReq = sendRequest(_URL + "/SaveData", "POST", { param: values, budgetOwnerId: values.BudgetOwnerId, categoryIdList: itemsCategory, profitCenterIdList: itemsProfitCenter });
        itemsCategory = [];
        itemsProfitCenter = [];
        return sendReq;
    },
    update: function (key, values) {
        var sendReq = sendRequest(_URL + "/SaveData", "POST", { param: values, budgetOwnerId: values.BudgetOwnerId, categoryIdList: itemsCategory, profitCenterIdList: itemsProfitCenter });
        itemsCategory = [];
        itemsProfitCenter = [];
        return sendReq;
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

var customStoreUsername = new DevExpress.data.CustomStore({
    key: "Id",
    load: function (loadOptions) {
        var search = "";
        search = loadOptions.searchValue == null ? "" : loadOptions.searchValue;
        return loadData(_URL_PARAM + "/GetUserOption?search=" + search, loadOptions);
    },
    byKey: function (key) {
        var d = new $.Deferred();
        $.get(_URL_PARAM + "/GetUserOption?key=" + key)
            .done(function (dataItem) {
                d.resolve(dataItem);
            });
        return d.promise();
    }
});

var customStoreGroup = new DevExpress.data.CustomStore({
    key: "Id",
    load: function (loadOptions) {
        search = loadOptions.searchValue == null ? "" : loadOptions.searchValue;
        return loadData(_URL_PARAM + "/GetRoleOption?search=" + search, loadOptions);
    },
    byKey: function (key) {
        var d = new $.Deferred();
        $.get(_URL_PARAM + "/GetRoleOptionById?search=" + key)
            .done(function (dataItem) {
                d.resolve(dataItem.result);
            });
        return d.promise();
    }
});

$(function () {
    var now = new Date();
    var date = now.toISOString();
    dataDate = moment(now).format("YYYY-MM-DD");

    table = $("#gridContainer").dxDataGrid({
        dataSource: customStore, //Data,
        remoteOperations: false,
        onRowUpdating: function (e) {
            $.extend(e.newData, $.extend({}, e.oldData, e.newData));
        },
        //errorRowEnabled: false,
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
                        swal.fire({
                            title: "Are you sure?",
                            text: "Syncronize All User Data!",
                            icon: "question",
                            showCancelButton: true,
                            confirmButtonText: 'Yes',
                            cancelButtonText: 'No',
                        }).then((result) => {
                            if (result.isConfirmed) {
                                loadPanel.show();
                                $.ajax({
                                    url: baseUrl + "Users/SyncronizeAllUserData",
                                    success: function (resp) {
                                        loadPanel.hide();
                                        swal.fire({
                                            title: resp.title,
                                            text: resp.result,
                                            icon: resp.status
                                        }).then(function () {
                                            dataGrid.getDataSource().reload();
                                            $('.informers .count').text(totalItems);
                                        })
                                    },
                                    error: function (err) {
                                        loadPanel.hide();
                                        swal.fire({
                                            title: "Error",
                                            text: "Cannot Update Mapping User",
                                            icon: "error"
                                        })
                                    },
                                    fail: function (err) {
                                        loadPanel.hide();
                                        swal.fire({
                                            title: "Error",
                                            text: "Cannot Update Mapping User",
                                            icon: "error"
                                        })
                                    }
                                })
                            }
                        });
                    },
                    template: function () {
                        var htm = "<button class='btn btn-outline-primary' title='Syncronize All User'><i class='fa fa-sync'></i> Syncronize All User</button>";
                        return $("<div/>").addClass("informer").append(htm);
                    }
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
                title: "User Info",
                showTitle: true,
                width: 900,
                height: "auto",
                position: {
                    my: "top",
                    at: "top",
                    of: window
                }
            },
            form: {
                items: [
                    {
                        dataField: "UserCode",
                        //editorType: "dxSelectBox",
                        validationRules: [{ type: "required" }],
                    },
                    {
                        dataField: "RoleId",
                        caption: "Role",
                        validationRules: [{ type: "required" }],
                    },
                    {
                        dataField: "BudgetOwnerId",
                        caption: "Budget Owner",
                        //validationRules: [{ type: "required" }],
                    },
                    {
                        dataField: "CategoryId",
                        caption: "Category",
                        //validationRules: [{ type: "required" }],
                    },
                    {
                        dataField: "ProfitCenterId",
                        caption: "Profit Center",
                        //validationRules: [{ type: "required" }],
                    }
                ]
            }
        },
        columns: [
            {
                caption: "Action",
                type: "buttons",
                buttons: [
                    "edit", "delete"
                ],
                fixed: true,
                fixedPosition: "left"
            },
            {
                caption: "User Code",
                dataField: "UserCode",
                alignment: "left",
                sortIndex: 0, sortOrder: "asc",
                editorType: "dxSelectBox",
                cellTemplate: function (element, info) {
                    element.append("<div>" + info.value + "</div > ").css("color", "black");
                },
                lookup: {
                    dataSource: customStoreUsername,
                    valueExpr: 'userCode',
                    displayExpr: 'userCode',
                    searchEnabled: true,           
                },
                editorOptions: {
                    itemTemplate: function (itemData, itemIndex, itemElement) {
                        if (itemData != null) {
                            //$("<span>").addClass("middle").text(itemData.userCode).appendTo(itemElement);
                            $("<span>").addClass("middle").text(itemData.userName).appendTo(itemElement);
                            //$("<span>").addClass("middle").text(" (" + itemData.userName + ") ").appendTo(itemElement);
                            $("<span>").addClass("middle").text(" - ").appendTo(itemElement);
                            $("<span>").addClass("middle").text(itemData.email).appendTo(itemElement);
                        } else {
                            $("<span>").text("(All)").appendTo(itemElement);
                        }
                    }
                }
                //editorOptions: {
                //    dataSource: customStoreUsername,
                //    valueExpr: "userCode",
                //    displayExpr: "userCode",
                //    placeholder: "Choose User",
                //    searchEnabled: true,                
                //    itemTemplate: function (itemData, itemIndex, itemElement) {
                //        if (itemData != null) {
                //            //$("<span>").addClass("middle").text(itemData.userCode).appendTo(itemElement);
                //            $("<span>").addClass("middle").text(itemData.userName).appendTo(itemElement);
                //            //$("<span>").addClass("middle").text(" (" + itemData.userName + ") ").appendTo(itemElement);
                //            $("<span>").addClass("middle").text(" - ").appendTo(itemElement);
                //            $("<span>").addClass("middle").text(itemData.email).appendTo(itemElement);
                //        } else {
                //            $("<span>").text("(All)").appendTo(itemElement);
                //        }
                //    }
                //}
            },
            { caption: "User Name", dataField: "UserName", alignment: "left" },
            { caption: "Full Name", dataField: "Name", alignment: "left" },
            { caption: "Email", dataField: "Email", alignment: "left" },
            {
                caption: "Role",
                visible: false,
                dataField: "RoleId",
                cellTemplate: function (element, info) {
                    element.append("<div>" + info.value + "</div>").css("color", "black");
                },
                setCellValue(rowData, value) {
                    RoleId = value;
                    rowData.RoleId = value;
                    rowData.BudgetOwnerId = null;
                    rowData.CategoryId = null;
                    rowData.ProfitCenterId = null;
                    flagCategory = null;
                },
                lookup: {
                    dataSource: customStoreGroup,
                    valueExpr: 'RoleId',
                    displayExpr: 'RoleName',
                },
                editorOptions: {
                    //placeholder: 'Choose Role',
                }
            },
            { caption: "Role", dataField: "RoleName", alignment: "left" },
            {
                caption: "Budget Owner",
                visible: false,
                dataField: "BudgetOwnerId",
                setCellValue(rowData, value) {
                    rowData.BudgetOwnerId = value;
                    rowData.CategoryId = null;
                    rowData.ProfitCenterId = null;
                },
                lookup: {
                    dataSource: customStoreBudgetOwner,
                    valueExpr: 'Id',
                    displayExpr: 'BudgetOwner',
                },
                editorOptions: {
                    //placeholder: 'Choose Budget Owner',
                }
            },
            {              
                caption: 'Category',
                dataField: 'CategoryId',
                cellTemplate: 'typeTemplate',
                editCellTemplate: function (container, data) {
                    itemsCategory = [];
                    var string = data.value;
                    var itemValues = (typeof string == 'undefined') || string == null || string == '0' ? [] : JSON.parse("[" + string + "]");
                    container.dxTagBox({
                        dataSource: customStoreCategory,
                        valueExpr: 'Id',
                        displayExpr: 'Category',
                        placeholder: "Select....",
                        value: itemValues,
                        searchEnabled: true,
                        showSelectionControls: true,
                        showClearButton: true,
                        onValueChanged: function (e) {
                            data.setValue(e.values);
                            itemsCategory = e.value;
                        },
                        searchEnabled: true,
                        disabled: (typeof flagCategory != 'undefined' || flagCategory != null) && (flagCategory == true) ? false : true,
                        elementAttr: {
                            class: "dx-dropdowneditor-input-overflow"
                        }
                    });
                }
            },
            {
                caption: 'Profit Center',
                dataField: 'ProfitCenterId',
                cellTemplate: 'typeTemplate',
                editCellTemplate: function (container, data) {
                    itemsProfitCenter = [];
                    var string = data.value;
                    var itemValues = (typeof string == 'undefined') || string == null || string == '0' ? [] : JSON.parse("[" + string + "]");
                    container.dxTagBox({
                        dataSource: customStoreProfitCenter,
                        valueExpr: 'Id',
                        displayExpr: 'ProfitCenter',
                        placeholder: "Select....",
                        value: itemValues,
                        searchEnabled: true,
                        showSelectionControls: true,
                        showClearButton: true,
                        onValueChanged: function (e) {
                            data.setValue(e.values);
                            itemsProfitCenter = e.value;
                        },
                        searchEnabled: true,
                        disabled: (typeof flagCategory == 'undefined' || flagCategory == null) ? true : (flagCategory == true) ? true : false,
                        elementAttr: {
                            class: "dx-dropdowneditor-input-overflow"
                        }
                    });
                }
            }
            //{
            //    caption: "Category",
            //    dataField: "CategoryId",
            //    alignment: "left",
            //    visible: false,
            //    //editorType: "dxTagBox",
            //    //editorOptions: {
            //    //    dataSource: customStoreCategory,
            //    //    valueExpr: "Id",
            //    //    displayExpr: "Category",
            //    //    placeholder: "Select....",
            //    //    showSelectionControls: true,
            //    //    searchEnabled: true,
            //    //    showClearButton: true,
            //    //    elementAttr: {
            //    //        class: "dx-dropdowneditor-input-overflow"
            //    //    }
            //    //}
            //},
            //{
            //    caption: "Profit Center",
            //    dataField: "ProfitCenterId",
            //    alignment: "left",
            //    visible: false,
            //    editorType: "dxTagBox",
            //    editorOptions: {
            //        dataSource: customStoreProfitCenter,
            //        valueExpr: "Id",
            //        displayExpr: "ProfitCenter",
            //        placeholder: "Select....",
            //        showSelectionControls: true,
            //        searchEnabled: true,
            //        showClearButton: true,
            //        elementAttr: {
            //            class: "dx-dropdowneditor-input-overflow"
            //        }
            //    }
            //}
        ],
        onInitNewRow() {
            flagCategory = null;
        },
        onEditingStart: function (e) {
            RoleId = e.data.RoleId;
            flagCategory = e.data.IsCategory;
        },
        onEditorPreparing: function (e) {
            if (e.parentType === "dataRow" && e.dataField === "UserCode") {
                if (e.row) {
                    if (!e.row.isNewRow) {
                        e.editorType = "dxTextBox";
                        e.editorOptions.disabled = true;
                    }
                }
            }

            if (e.parentType === "dataRow" && e.dataField === "RoleId") {
                if (e.row) {
                    if (!e.row.isNewRow) {
                        e.editorOptions.text = e.row.data.RoleName;
                        e.editorOptions.value = e.row.data.RoleId;
                    }
                }
            }

            if (e.parentType === 'dataRow' && (e.dataField === 'BudgetOwnerId')) {
                var standardHandler = e.editorOptions.onValueChanged;
                e.editorOptions.onValueChanged = function (e) {
                    ajaxSetData(RoleId, e.value);
                    standardHandler(e);
                }
            }          

            if (e.parentType === 'dataRow' && (e.dataField === 'CategoryId' || e.dataField === 'ProfitCenterId')) {
                if (e.dataField === 'CategoryId') {
                    e.editorOptions.disabled = (typeof flagCategory != 'undefined' || flagCategory != null) && (flagCategory == true) ? false : true;
                } else {
                    e.editorOptions.disabled = (typeof flagCategory == 'undefined' || flagCategory == null) ? true : (flagCategory == true) ? true : false;
                }
            }
        },
        masterDetail: {
            enabled: true,
            template: function (container, options) {
                var detailSpending = new DevExpress.data.CustomStore({
                    key: "Id",
                    load: function (loadOptions) {
                        var url = baseUrl + "Users/GetUsersSpending";
                        return loadData(`${url}?userCode=${options.data.UserCode}`);
                    }
                });

                $("<div>")
                    .dxDataGrid({
                        columnAutoWidth: true,
                        dataSource: detailSpending,
                        showBorders: true,
                        loadPanel: {
                            enabled: true
                        },
                        filterRow: {
                            visible: true,
                            applyFilter: "auto"
                        },
                        columns: [
                            { caption: "Budget Owner", dataField: "BudgetOwner", alignment: "left", width: "300" },
                            { caption: "Category", dataField: "Category", alignment: "left", width: "300", sortIndex: 0, sortOrder: "asc" },
                            { caption: "Profit Center", dataField: "ProfitCenter", alignment: "left", width: "300" },
                        ]
                    }).appendTo(container);
            }
        },
    }).dxDataGrid("instance");
    //InitTreeListSpandingRole();
    //InitPopupObject();
})

function ajaxSetData(parRoleId, parBudgetOwnerId) {
    $.ajax({
        type: 'GET',
        url: _URL_PARAM + '/GetDataRoleBudgetOwnerByRoleBOId',
        cache: true,
        async: false,
        dataType: "json",
        processData: true,
        contentType: false,
        data: {
            roleId: parRoleId,
            budgetOwnerId: parBudgetOwnerId
        },
        success: function (dataItem) {
            if (dataItem) {
                var data = dataItem.result;
                if (data != null) {
                    flagCategory = data.isCategory;
                }
            }
        }
    })
}

var customStoreBudgetOwner = new DevExpress.data.CustomStore({
    key: "Id",
    load: function (loadOptions) {
        search = loadOptions.searchValue == null ? "" : loadOptions.searchValue;
        return loadData(_URL_BOMAP + "/GetBudgetOwnerOptionByRoleId?search=" + RoleId, loadOptions);
    },
    byKey: function (key) {
        var d = new $.Deferred();
        $.get(_URL_BOMAP + "/GetBudgetOwnerOptionById?search=" + key)
            .done(function (dataItem) {
                d.resolve(dataItem.result);
            });
        return d.promise();
    }
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
        return loadData(_URL + "/GetProfitCenterOption?search=" + search, loadOptions);
    },
    byKey: function (key) {
        var d = new $.Deferred();
        $.get(_URL + "/GetProfitCenterOptionById?search=" + key)
            .done(function (dataItem) {
                d.resolve(dataItem.result);
            });
        return d.promise();
    }
})

//function InitTreeListSpandingRole() {
//    treeList = $('#dxTreeListSpandingRole').dxTreeList({
//        dataSource: employees,
//        keyExpr: 'ID',
//        parentIdExpr: 'Head_ID',
//        showRowLines: true,
//        showBorders: true,
//        columnAutoWidth: true,
//        height: 400,
//        selection: {
//            mode: 'multiple',
//            recursive: false,
//        },
//        columns: [{
//            caption: "Budget Owner",
//            dataField: 'FieldData',
//        }],
//        expandedRowKeys: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20],
//        onSelectionChanged() {
//            //const selectedData = treeList.getSelectedRowsData(selectionMode);
//            //$('#selected-items-container').text(getEmployeeNames(selectedData));
//        },
//    }).dxTreeList('instance');

//    treeList.clearSelection();
//    treeList.option('selection.recursive', true);
//}

//function InitPopupObject() {
//    $('#dxSelectUser').dxSelectBox({
//        dataSource: customStoreUsername,
//        displayExpr: 'userCode',
//        valueExpr: 'userCode',
//        placeholder: "Choose User",
//        searchEnabled: true,
//        //value: products[0].userCode,
//    });

//    $('#dxSelectRole').dxSelectBox({
//        dataSource: customStoreGroup,
//        displayExpr: 'RoleId',
//        valueExpr: 'RoleName',
//        placeholder: "Choose Role",
//        searchEnabled: true,
//        //value: products[0].userCode,
//    });

//    $('#btnSave').dxButton({
//        stylingMode: 'contained',
//        text: 'Save',
//        type: 'normal',
//        width: 100,
//        onClick() {
//            //$('#ModalMaster').modal('hide');
//        },
//    });

//    $('#btnCancel').dxButton({
//        stylingMode: 'contained',
//        text: 'Cancel',
//        type: 'normal',
//        width: 100,
//        onClick() {
//            $('#ModalMaster').modal('hide');
//        },
//    });
//}

//const employees = [{
//    ID: 1,
//    Head_ID: 0,
//    FieldData: 'CCD',
//}, {
//    ID: 2,
//    Head_ID: 0,
//    FieldData: 'MT',
//}, {
//    ID: 3,
//    Head_ID: 0,
//    FieldData: 'BMPA',
//}, {
//    ID: 4,
//    Head_ID: 1,
//    FieldData: 'Category',
//}, {
//    ID: 5,
//    Head_ID: 2,
//    FieldData: 'Category',
//}, {
//    ID: 6,
//    Head_ID: 3,
//    FieldData: 'Profit Center',
//}, {
//    ID: 7,
//    Head_ID: 4,
//    FieldData: 'All',
//}, {
//    ID: 8,
//    Head_ID: 5,
//    FieldData: 'All',
//}, {
//    ID: 9,
//    Head_ID: 6,
//    FieldData: 'CD',
//}, {
//    ID: 10,
//    Head_ID: 6,
//    FieldData: 'SN',
//}];