var dataGrid;
var dataDate;
var currentMonth;
var currentYear;
var totalItems = 0;
var locked = null;
var _URL = baseUrl + "Update";

var customStore = new DevExpress.data.CustomStore({
    key: "Id",
    load: function (loadOptions) {
        var loadUrl = _URL + "/GetDataSpendingPhasing?year=" + currentYear + "&month=" + currentMonth;
        return loadData(loadUrl, loadOptions);
    },
    remove: function (key) {
        return sendRequest(_URL + "/Delete", "POST", { Id: key });
    },
    byKey: function (key) {
        var d = new $.Deferred();
        $.get(baseUrl + "/GetDataSpendingPhasing?Id=" + key)
            .done(function (dataItem) {
                d.resolve(dataItem);
            });
        return d.promise();
    }
})

var optionYearsStore = new DevExpress.data.CustomStore({
    key: "Years",
    load: function (loadOptions) {
        var search = (loadOptions.searchValue !== null && loadOptions.searchValue !== "" ? loadOptions.searchValue : "");
        var loadUrl = baseUrl + "Parameter/GetYearsSpendingPhasingOption?transaction=Update";
        return loadData(loadUrl, loadOptions);
    },
    byKey: function (key) {
        var d = new $.Deferred();
        $.get(baseUrl + "Parameter/GetYearsSpendingPhasingOption?key=" + key + "&transaction=Update")
            .done(function (dataItem) {
                var data = dataItem.result;
                var obj = { "Years": data.Years }
                d.resolve(obj);
            });
        return d.promise();
    }
})

var optionMonthStore = new DevExpress.data.CustomStore({
    key: "Month",
    load: function (loadOptions) {
        var search = (loadOptions.searchValue !== null && loadOptions.searchValue !== "" ? loadOptions.searchValue : "");
        var loadUrl = baseUrl + "Parameter/GetMonthOption?isBudget=0";
        return loadData(loadUrl, loadOptions);
    },
    byKey: function (key) {
        var d = new $.Deferred();
        $.get(baseUrl + "Parameter/GetMonthOption?isBudget=0&key=" + key)
            .done(function (dataItem) {
                var data = dataItem.result;
                var obj = { "Month": data.Month }
                d.resolve(obj);
            });
        return d.promise();
    }
})

var customStoreCategory = new DevExpress.data.CustomStore({
    key: "Category",
    load: function (loadOptions) {
        search = loadOptions.searchValue == null ? "" : loadOptions.searchValue;
        return loadData(baseUrl + "Users/GetCategoryOption?search=" + search, loadOptions);
    },
    byKey: function (key) {
        var d = new $.Deferred();
        $.get(baseUrl + "Users/GetCategoryOptionById?search=" + key)
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
        return loadData(baseUrl + "Users/GetProfitCenterOption?search=" + search, loadOptions);
    },
    byKey: function (key) {
        var d = new $.Deferred();
        $.get(baseUrl + "Users/GetProfitCenterOptionById?search=" + key)
            .done(function (dataItem) {
                d.resolve(dataItem.result);
            });
        return d.promise();
    }
})

var Categorylist = $("#Categorylist").dxTagBox({
    displayExpr: "Category",
    valueExpr: "Category",
    readOnly: true,
    //disabled: true,
    dataSource: customStoreCategory,
    placeholder: "",
    elementAttr: {
        class: "dx-dropdowneditor-input-overflow"
    }
}).dxTagBox("instance");

var ProfitCenterlist = $("#ProfitCenterlist").dxTagBox({
    displayExpr: "ProfitCenter",
    valueExpr: "ProfitCenter",
    readOnly: true,
    //disabled: true,
    dataSource: customStoreProfitCenter,
    placeholder: "",
    elementAttr: {
        class: "dx-dropdowneditor-input-overflow"
    }
}).dxTagBox("instance");

function GetSelectedCategorylist() {
    var item = $(".document-checklist-textbox");
    var selected = [];
    if (item.length > 0) {
        jQuery.each(item, function (index, elm) {
            var val = $(elm).val();
            selected.push(val);
        })
    }

    $("#Categorylist").dxTagBox("instance").option("value", selected);
    console.log(selected);
}

function GetSelectedProfitCenterlist() {
    var item = $(".document-checklist-textbox1");
    var selected = [];
    if (item.length > 0) {
        jQuery.each(item, function (index, elm) {
            var val = $(elm).val();
            selected.push(val);
        })
    }

    if (selected != null)
        $("#ProfitCenterlist").dxTagBox("instance").option("value", selected);
    console.log(selected);
}

function InitTableSpendingPhasing() {
    var now = new Date();
    var date = now.toISOString();
    dataDate = moment(now).format("YYYY-MM-DD");
    currentMonth = $('#CurrentMonth').val();
    currentYear = $('#CurrentYear').val();
    dataGrid = $("#gridContainer").dxDataGrid({
        dataSource: customStore,
        //allowColumnReordering: true,
        //columnChooser: {
        //    enabled: true,
        //},
        columnAutoWidth: true,
        selection: {
            mode: "multiple",
            showCheckBoxesMode: "always"    // or "onClick" | "onLongTap" | "always" 
        },
        remoteOperations: false,
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
                    location: 'before',
                    widget: 'dxSelectBox',
                    options: {
                        width: 150,
                        dataSource: optionYearsStore,
                        displayExpr: function (data) {
                            if (data) {
                                return data.Years;
                            }
                        },
                        valueExpr: 'Years',
                        deferRendering: false,
                        onContentReady: function (e) {
                            e.component.option('value', currentYear);
                        },
                        onValueChanged(e) {
                            $('#CurrentYear').val(e.value);
                            currentYear = e.value;
                            dataGrid.getDataSource().reload();
                            //dataGrid.refresh();
                            //dataGrid.clearGrouping();
                            //dataGrid.columnOption(e.value, 'groupIndex', 0);
                        },
                    },
                },
                {
                    location: 'before',
                    widget: 'dxSelectBox',
                    options: {
                        width: 250,
                        dataSource: optionMonthStore,
                        displayExpr: function (data) {
                            if (data) {
                                return data.Month;
                            }
                        },
                        valueExpr: 'Month',
                        deferRendering: false,
                        onContentReady: function (e) {
                            e.component.option('value', currentMonth);
                        },
                        onValueChanged(e) {
                            $('#CurrentMonth').val(e.value);
                            currentMonth = e.value;
                            dataGrid.getDataSource().reload();
                            //dataGrid.refresh();
                            //dataGrid.clearGrouping();
                            //dataGrid.columnOption(e.value, 'groupIndex', 0);
                        },
                    },
                },
                //{
                //    location: "before",
                //    visible: true,
                //    onClick: function (e) {
                //        lockData("SpendingPhasing");
                //    },
                //    template: function () {
                //        var htm = "";
                //        htm = "<button class='btn btn-danger' title='Lock Periode'><i class='fa fa-lock'></i> Lock Periode</button>";
                //        return $("<div/>")
                //            .addClass("informer button")
                //            .append(htm);
                //    }
                //},
                //{
                //    location: "before",
                //    visible: true,
                //    template: function () {
                //        var htm = "";
                //        htm = "<div class='informersLock'></div>";
                //        return $("<div/>")
                //            .addClass("")
                //            .append(htm);
                //    }
                //},
                {
                    location: "after",
                    visible: true,
                    onClick: function (e) {
                        if (locked == false) {
                            if (dataGrid.getDataSource().totalCount() > 0) {
                                var inpBudgetOwner = $('#inpBudgetOwner').val();
                                var dataCategorylist = $("#Categorylist").dxTagBox("instance").option("value");
                                var dataProfitCenterlist = $("#ProfitCenterlist").dxTagBox("instance").option("value");

                                if (inpBudgetOwner == null || inpBudgetOwner == "") { Swal.fire("Warning", "Cannot Upload Data, Budget Owner cannot be empty", "warning"); return false; }

                                $("#year").val(currentYear);
                                $("#month").val(currentMonth);
                                $("#budgetOwner").val(inpBudgetOwner);
                                $("#categoryList").val(dataCategorylist);
                                $("#profitCenterList").val(dataProfitCenterlist);
                                $("#formDownload").attr("action", "DownloadTemplateSpendingPhasing");
                                $("#btnDownload").trigger("click");
                            } else {
                                Swal.fire('Warning', 'Unable to Download, because data not found!', 'warning');
                            }
                        }
                    },
                    template: function () {
                        var htm = "";
                        htm = "<button class='btn btn-success btn-download' title='Download Template'><i class='fa fa-download'></i> Download Template</button>";
                        return $("<div/>")
                            .addClass("informer")
                            .append(htm);
                    }
                },
                {
                    location: "after",
                    visible: true,
                    onClick: function (e) {
                        if (locked == false) {
                            if (dataGrid.getDataSource().totalCount() > 0) {
                                openModalUpload("SpendingPhasing");
                            } else {
                                Swal.fire('Warning', 'Unable to Update, because data not found!', 'warning');
                            }
                        } 
                    },
                    template: function () {
                        var htm = "";
                        htm = "<button class='btn btn-primary btn-upload' title='Upload Template'><i class='fa fa-upload'></i> Upload Template</button>";
                        return $("<div/>")
                            .addClass("informer")
                            .append(htm);
                    }
                },
                {
                    location: "after",
                    locateInMenu: "auto",
                    widget: "dxButton",
                    options: {
                        icon: "download",
                        hint: "Export to Excel",
                        elementAttr: {
                            "class": "dx-button-success"
                        },
                        height: 28,
                        onClick: function () {
                            e.component.exportToExcel(false);
                        }
                    }
                },
                {
                    location: "after",
                    onClick: function (e) {
                        dataGrid.getDataSource().reload();
                        $('.informers .count').text(totalItems);
                    },
                    template: function () {
                        var htm = "<button class='btn btn-outline-primary' title='Refresh All'><i class='fa fa-sync'></i></button>&nbsp;";
                        return $("<div/>").addClass("informer").append(htm);
                    }
                }
            )
        },
        onContentReady: function (e) {
            let total = e.component.getDataSource().totalCount();
            totalItems = parseFloat(total).toLocaleString(window.document.documentElement.lang);
            $('.informers .count').text(totalItems);
            locked = false;

            //jQuery.ajax({
            //    method: 'POST',
            //    url: _URL + '/GetDataLockSpendingPhasing',
            //    data: {
            //        year: currentYear,
            //        month: currentMonth
            //    },
            //    success: function (dataItem) {
            //        if (dataItem) {
            //            var data = dataItem.result;
            //            var disabled = totalItems == 0 ? false : data.IsLocked;
            //            var visibility = disabled ? "visible" : "hidden";
            //            var visibilitys = disabled ? "hidden" : "visible";
            //            $(".informersLock").css("visibility", visibility);
            //            $(".btn-download").attr("disabled", disabled);
            //            $(".btn-upload").attr("disabled", disabled);
            //            locked = totalItems == 0 ? false : data.IsLocked;
            //        }
            //    }
            //})
        },
        onExporting: function (e) {
            var workbook = new ExcelJS.Workbook();
            var worksheet = workbook.addWorksheet('Spending Phasing Update');
            DevExpress.excelExporter.exportDataGrid({
                worksheet: worksheet,
                component: e.component,
                customizeCell: function (options) {
                    var excelCell = options;
                    excelCell.font = { name: 'Arial', size: 12 };
                    excelCell.alignment = { horizontal: 'left' };
                }
            }).then(function () {
                workbook.xlsx.writeBuffer().then(function (buffer) {
                    saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Export Spending Phasing Update.xlsx');
                });
            });
            e.cancel = true;
        },
        onCellPrepared: function (e) {
            if (e.rowType === 'totalFooter') {
                if (e.summaryItems.length > 0) {
                    e.cellElement[0].querySelector(".dx-datagrid-summary-item").style.fontWeight = 'bold';
                    e.cellElement[0].querySelector(".dx-datagrid-summary-item").style.color = '#808080';
                }
            }
        },
        //onEditorPreparing: function (e) {
        //    e.component.columnOption("SO").formItem.visible = false;
        //},
        onSelectionChanged: function (e) {
            e.component.repaint();
        },
        selection: {
            mode: "row"
        },
        headerFilter: {
            visible: true
        },
        filterRow: {
            visible: true,
            applyFilter: "auto"
        },
        //onCellPrepared: function (e) {
        //    e.cellElement.find(".dx-header-filter").hide();
        //},
        searchPanel: {
            visible: false
        },
        showBorders: true,
        sorting: {
            mode: "multiple"
        },
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
        editing: {
            mode: "row",
        },
        wordWrapEnabled: true,
        columnFixing: {
            enabled: true,
        },
        columns: [
            //{
            //    caption: "Status",
            //    type: "buttons",
            //    buttons: [
            //        {
            //            hint: "",
            //            icon: "",
            //            visible: function (e) {
            //                return !e.row.data.IsLocked;
            //            }
            //        },
            //        {
            //            hint: "Locked",
            //            icon: "key",
            //            visible: function (e) {
            //                return e.row.data.IsLocked;
            //            }
            //        }
            //    ],
            //    fixed: true,
            //    fixedPosition: "left"
            //},
            { caption: "Year", dataField: "Year", alignment: "left", width: 120, fixed: true },
            { caption: "Budget Owner", dataField: "BudgetOwner", alignment: "left", width: 200, fixed: true },
            { caption: "Profit Center", dataField: "ProfitCenter", alignment: "left", width: 200, fixed: true },
            { caption: "Category", dataField: "Category", alignment: "left", width: 200, fixed: true },
            { caption: "GL", dataField: "GLCode", alignment: "left", width: 200 },
            { caption: "GL Desc", dataField: "GLDesc", alignment: "left", width: 300 },
            { caption: "GL Type", dataField: "GLType", alignment: "left", width: 120 },
            { caption: "New Channel", dataField: "NewChannel", alignment: "left", width: 300 },           
            { caption: "Customer", dataField: "Customer", alignment: "left", width: 200 },
            { caption: "Old Channel", dataField: "OldChannel", alignment: "left", width: 200 },
            { caption: "MG3*", dataField: "MG3", alignment: "left", width: 200 },
            { caption: "MG4*", dataField: "MG4", alignment: "left", width: 200 },
            { caption: "Activity*", dataField: "Activity", alignment: "left", width: 200 },
            {
                caption: "Jan", dataField: "Jan",
                alignment: "right", width: 120,
                dataType: "number",
                format: {
                    type: 'fixedPoint',
                    precision: 2
                },
                editorOptions: {
                    min: 0,
                    format: "#,##0.##",
                    disabled: true
                },
            },
            {
                caption: "Feb", dataField: "Feb",
                alignment: "right", width: 120,
                dataType: "number",
                format: {
                    type: 'fixedPoint',
                    precision: 2
                },
                editorOptions: {
                    min: 0,
                    format: "#,##0.##",
                    disabled: true
                },
            },
            {
                caption: "Mar", dataField: "Mar",
                alignment: "right", width: 120,
                dataType: "number",
                format: {
                    type: 'fixedPoint',
                    precision: 2
                },
                editorOptions: {
                    min: 0,
                    format: "#,##0.##",
                    disabled: true
                },
            },
            {
                caption: "Apr", dataField: "Apr",
                alignment: "right", width: 120,
                dataType: "number",
                format: {
                    type: 'fixedPoint',
                    precision: 2
                },
                editorOptions: {
                    min: 0,
                    format: "#,##0.##",
                    disabled: true
                },
            },
            {
                caption: "May", dataField: "May",
                alignment: "right", width: 120,
                dataType: "number",
                format: {
                    type: 'fixedPoint',
                    precision: 2
                },
                editorOptions: {
                    min: 0,
                    format: "#,##0.##",
                    disabled: true
                },
            },
            {
                caption: "Jun", dataField: "Jun",
                alignment: "right", width: 120,
                dataType: "number",
                format: {
                    type: 'fixedPoint',
                    precision: 2
                },
                editorOptions: {
                    min: 0,
                    format: "#,##0.##",
                    disabled: true
                },
            },
            {
                caption: "Jul", dataField: "Jul",
                alignment: "right", width: 120,
                dataType: "number",
                format: {
                    type: 'fixedPoint',
                    precision: 2
                },
                editorOptions: {
                    min: 0,
                    format: "#,##0.##",
                    disabled: true
                },
            },
            {
                caption: "Aug", dataField: "Aug",
                alignment: "right", width: 120,
                dataType: "number",
                format: {
                    type: 'fixedPoint',
                    precision: 2
                },
                editorOptions: {
                    min: 0,
                    format: "#,##0.##",
                    disabled: true
                },
            },
            {
                caption: "Sep", dataField: "Sep",
                alignment: "right", width: 120,
                dataType: "number",
                format: {
                    type: 'fixedPoint',
                    precision: 2
                },
                editorOptions: {
                    min: 0,
                    format: "#,##0.##",
                    disabled: true
                },
            },
            {
                caption: "Oct", dataField: "Oct",
                alignment: "right", width: 120,
                dataType: "number",
                format: {
                    type: 'fixedPoint',
                    precision: 2
                },
                editorOptions: {
                    min: 0,
                    format: "#,##0.##",
                    disabled: true
                },
            },
            {
                caption: "Nov", dataField: "Nov",
                alignment: "right", width: 120,
                dataType: "number",
                format: {
                    type: 'fixedPoint',
                    precision: 2
                },
                editorOptions: {
                    min: 0,
                    format: "#,##0.##",
                    disabled: true
                },
            },
            {
                caption: "Dec", dataField: "Dec",
                alignment: "right", width: 120,
                dataType: "number",
                format: {
                    type: 'fixedPoint',
                    precision: 2
                },
                editorOptions: {
                    min: 0,
                    format: "#,##0.##",
                    disabled: true
                },
            },
            {
                caption: "Total", dataField: "Total",
                alignment: "right", width: 120,
                dataType: "number",
                format: {
                    type: 'fixedPoint',
                    precision: 2
                },
                editorOptions: {
                    min: 0,
                    format: "#,##0.##",
                    disabled: true
                },
            },
        ],
        summary: {
            recalculateWhileEditing: true,
            totalItems: [
                {
                    column: 'Activity',
                    displayFormat: 'Total',
                    minWidth: 150
                },
                {
                    column: 'Jan',
                    displayFormat: '{0}',
                    summaryType: 'sum',
                    valueFormat: '#,##0.00',
                    minWidth: 150
                },
                {
                    column: 'Feb',
                    displayFormat: '{0}',
                    summaryType: 'sum',
                    valueFormat: '#,##0.00',
                    minWidth: 150
                },
                {
                    column: 'Mar',
                    displayFormat: '{0}',
                    summaryType: 'sum',
                    valueFormat: '#,##0.00',
                    minWidth: 150
                },
                {
                    column: 'Apr',
                    displayFormat: '{0}',
                    summaryType: 'sum',
                    valueFormat: '#,##0.00',
                    minWidth: 150
                },
                {
                    column: 'May',
                    displayFormat: '{0}',
                    summaryType: 'sum',
                    valueFormat: '#,##0.00',
                    minWidth: 150
                },
                {
                    column: 'Jun',
                    displayFormat: '{0}',
                    summaryType: 'sum',
                    valueFormat: '#,##0.00',
                    minWidth: 150
                },
                {
                    column: 'Jul',
                    displayFormat: '{0}',
                    summaryType: 'sum',
                    valueFormat: '#,##0.00',
                    minWidth: 150
                },
                {
                    column: 'Aug',
                    displayFormat: '{0}',
                    summaryType: 'sum',
                    valueFormat: '#,##0.00',
                    minWidth: 150
                },
                {
                    column: 'Sep',
                    displayFormat: '{0}',
                    summaryType: 'sum',
                    valueFormat: '#,##0.00',
                    minWidth: 150
                },
                {
                    column: 'Oct',
                    displayFormat: '{0}',
                    summaryType: 'sum',
                    valueFormat: '#,##0.00',
                    minWidth: 150
                },
                {
                    column: 'Nov',
                    displayFormat: '{0}',
                    summaryType: 'sum',
                    valueFormat: '#,##0.00',
                    minWidth: 150
                },
                {
                    column: 'Dec',
                    displayFormat: '{0}',
                    summaryType: 'sum',
                    valueFormat: '#,##0.00',
                    minWidth: 150
                },
                {
                    column: 'Total',
                    displayFormat: '{0}',
                    summaryType: 'sum',
                    valueFormat: '#,##0.00',
                    minWidth: 150
                }
            ],
        }
    }).dxDataGrid("instance");
}

$(function () {
    InitTableSpendingPhasing();
    GetSelectedCategorylist();
    GetSelectedProfitCenterlist();
})

function openModalUpload(flag) {
    $('#inpFlag').val(flag);
    $('#titleModalUpload').text('Spending Phasing Update');
    $('#divModalUpload').modal('show');
    $('#Inputfile').val(null);
    $('#InputfileMapping').val(null);
    $('#Inputfile').attr("accept", ".pdf,.xls,.xlsx");

    document.getElementById("divFileMappingRDNDO").style.display = "none";
    document.getElementById("btnUploadExcel").style.display = "block";
}

$('#btnUploadExcel').on('click', function (e) {
    uploadData();
})

$('#InputfileMapping').on('change', function (e) {
    checkSizeFileUpload(this, "InputfileMapping");
})

$('#Inputfile').on('change', function (e) {
    checkSizeFileUpload(this, "Inputfile");
})

function checkSizeFileUpload(e, nameval) {
    var fp = $('#' + nameval + '');
    var lg = fp[0].files.length;
    var items = fp[0].files;
    var fileSize = 0;

    if (lg > 0) {
        for (var i = 0; i < lg; i++) {
            fileSize = fileSize + items[i].size;
        }

        if (fileSize > 10097152) {
            Swal.fire("Warning", "File size can't be more than 10 Mb", "warning");
            $('#' + nameval + '').val('');
        }
    }
}

function checkHasFailedUpload() {
    var status = false;
    $.ajax({
        type: 'GET',
        url: _URL + '/HasFailed' + $('#inpFlag').val(),
        async: false,
        processData: true,
        contentType: false,
        data: {
            year: currentYear,
            month: currentMonth
        },
        beforeSend: function () {
            //$('#overlay-screen').css('display', 'block');
        },
        success: function (resp) {
            if (resp.status === "true") {
                status = true;
            } else {
                status = false;
            }
        },
        error: function (result) {
            //$('#overlay-screen').hide();
            Swal.fire("Error", result.msg, 'error');
            $('#divModalUpload').modal('hide');
        }
    })
    return status;
}

function uploadData() {
    var form = new FormData();
    var element = document.getElementById("Inputfile");
    var flag = $('#inpFlag').val();
    var inpBudgetOwner = $('#inpBudgetOwner').val();
    var dataCategorylist = $("#Categorylist").dxTagBox("instance").option("value");
    var dataProfitCenterlist = $("#ProfitCenterlist").dxTagBox("instance").option("value");

    if (inpBudgetOwner == null || inpBudgetOwner == "") { Swal.fire("Warning", "Cannot Upload Data, Budget Owner cannot be empty", "warning"); return false; }

    if (element.files.length > 0) {
        var name = "file";
        debugger;
        var files = element.files[0];
        form.append(name, files);
    } else {
        Swal.fire('Warning', 'Please choose file', 'warning');
        return;
    }

    loadPanel.show();
    form.append("flag", $('#inpFlag').val());
    form.append("isBulk", false);
    form.append("year", currentYear);
    form.append("month", currentMonth);
    form.append("budgetOwner", inpBudgetOwner);
    form.append("categoryList", dataCategorylist);
    form.append("profitCenterList", dataProfitCenterlist);

    loadingMessage = "Please wait, Proccessing your Document ...";
    loadPanel.option("message", loadingMessage);
    loadPanel.show();

    $.ajax({
        type: 'POST',
        url: _URL + '/Import' + $('#inpFlag').val(),
        processData: false,
        contentType: false,
        data: form,
        beforeSend: function () {
            //$('#overlay-screen').css('display', 'block');
        },
        success: function (result) {
            debugger;
            //$('#overlay-screen').hide();
            Swal.fire(result.status, result.msg, result.status);
            $('#divModalUpload').modal('hide');
            $("#gridContainer").dxDataGrid("instance").refresh();

            if (flag === "SpendingPhasing") {
                var hasFailed = checkHasFailedUpload();
                if (hasFailed) {
                    location.href = _URL + `/DownloadTemplate${flag}Failed?year=${currentYear}&month=${currentMonth}`;
                }
            }
            loadPanel.hide();
        },
        error: function (result) {
            debugger;
            //$('#overlay-screen').hide();
            var msg = result.responseJSON.msg;
            Swal.fire("Error", msg, 'error');
            $('#divModalUpload').modal('hide');
            loadPanel.hide();
        }
    })
}

//function lockData(flag) {
//    var form = new FormData();

//    loadPanel.show();
//    form.append("isBulk", false);
//    form.append("year", currentYear);
//    form.append("month", currentMonth);
//    form.append("isLock", true);

//    loadingMessage = "Please wait, Proccessing your Document ...";
//    loadPanel.option("message", loadingMessage);
//    loadPanel.show();

//    $.ajax({
//        type: 'POST',
//        url: _URL + '/Lock' + flag,
//        processData: false,
//        contentType: false,
//        data: form,
//        beforeSend: function () {
//            //$('#overlay-screen').css('display', 'block');
//        },
//        success: function (result) {
//            debugger;
//            Swal.fire(result.status, result.msg, result.status);
//            $('#divModalUpload').modal('hide');
//            $("#gridContainer").dxDataGrid("instance").refresh();
//            loadPanel.hide();
//        },
//        error: function (result) {
//            debugger;
//            //$('#overlay-screen').hide();
//            var msg = result.responseJSON.msg;
//            Swal.fire("Error", msg, 'error');
//            $('#divModalUpload').modal('hide');
//            loadPanel.hide();
//        }
//    })
//}
