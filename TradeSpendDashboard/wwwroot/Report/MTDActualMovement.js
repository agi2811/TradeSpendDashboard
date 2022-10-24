var dataGrid;
var currentMonth;
var currentYear;
var _URL = baseUrl + "ReportMovement";

const dataSources = [
    { ID: 1, Name: 'Current' },
    { ID: 2, Name: 'Snapshot' }
]

const dataSnapshot = [
    { ID: 2, Name: 'Snapshot 2 [2022-02-10 11:15]' },
    { ID: 1, Name: 'Snapshot 1 [2022-02-10 12:20]' }

]

var optionYearsStore = new DevExpress.data.CustomStore({
    key: "Years",
    load: function (loadOptions) {
        var search = (loadOptions.searchValue !== null && loadOptions.searchValue !== "" ? loadOptions.searchValue : "");
        var loadUrl = baseUrl + "Parameter/GetYearsReportOption";
        return loadData(loadUrl, loadOptions);
    },

    byKey: function (key) {
        var d = new $.Deferred();
        $.get(baseUrl + "Parameter/GetYearsReportOption?key=" + key)
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
    }
    ,
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

 

var optionSnapshotStore = new DevExpress.data.CustomStore({
    key: "ID",
    load: function (loadOptions) {
        //var search = (loadOptions.searchValue !== null && loadOptions.searchValue !== "" ? loadOptions.searchValue : "");
        var loadUrl = baseUrl + "ReportMovement/GetSnapshotHistory";
        return loadData(loadUrl, loadOptions);
    }
    //,
    //byKey: function (key) {
    //    var d = new $.Deferred();
    //    $.get(baseUrl + "Parameter/GetMonthOption?isBudget=0&key=" + key)
    //        .done(function (dataItem) {
    //            var data = dataItem.result;
    //            var obj = { "Month": data.Month }
    //            d.resolve(obj);
    //        });
    //    return d.promise();
    //}
})

function ShowModalSnapshot() {
    $('#SnapshotName').val('');
    $('#divModalSnapshot').modal('show');
}




function InitRadionEvent() {
    $('input[type=radio][name=inlineRadioOptions]').change(function () {
        if (this.value == 'Snapshot') {
            //alert(this.value);
            $('#divSnapshotName').css('visibility', 'visible');
            $('#divButtons').css('visibility', 'hidden');
            InitSelectSnapshot();
            ReadOnlySelectBox(true);
        }
        else {
            $('#divSnapshotName').css('visibility', 'hidden');
            $('#divButtons').css('visibility', 'visible'); 
            $('#dxSelectSnapshot').dxSelectBox('instance').option('value', null); 


            
            $('#dxSelectYear').dxSelectBox('instance').option('value', currentYear);
            $('#dxSelectYearOutlook').dxSelectBox('instance').option('value', currentYear);
            $('#dxSelectYearBudget').dxSelectBox('instance').option('value', currentYear);



            $('#dxSelectMonth').dxSelectBox('instance').option('value', currentMonth);
            $('#dxSelectMonthOutlook').dxSelectBox('instance').option('value', currentMonth);


            ReadOnlySelectBox(false);
        }
    });
}

function InitDataSource() {
    $("#dxSelectSource").dxSelectBox({
        dataSource: new DevExpress.data.ArrayStore({
            data: dataSources,
            key: 'ID',
        }),
        displayExpr: 'Name',
        valueExpr: 'ID',
        value: dataSources[0].ID,
        onValueChanged: function (data) {
            const value = data.value;
            if (value == 1) {
                $("#divYear").css("display", "block");
                $("#divMonth").css("display", "block");
                $("#divList").css("display", "none");
            } else {
                $("#divYear").css("display", "none");
                $("#divMonth").css("display", "none");
                $("#divList").css("display", "block");
            }
        }
    }).dxSelectBox('instance');
}

function InitYearActual() {
 
    $("#dxSelectYear").dxSelectBox({
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
            //$('#CurrentYear').val(e.value);
            //currentYear = e.value;
            //dataGrid.getDataSource().reload();
        }
    }).dxSelectBox('instance');

}

function InitYearOutlook() {
     

    $("#dxSelectYearOutlook").dxSelectBox({
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
            //$('#CurrentYear').val(e.value);
            //currentYear = e.value;
            //dataGrid.getDataSource().reload();
        }
    }).dxSelectBox('instance');


}

function InitYearBudget() {
 



    $("#dxSelectYearBudget").dxSelectBox({
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
            //$('#CurrentYear').val(e.value);
            //currentYear = e.value;
            //dataGrid.getDataSource().reload();
        }
    }).dxSelectBox('instance');
}

function InitSelectSnapshot() {
    $("#dxSelectSnapshot").dxSelectBox({
        dataSource: optionSnapshotStore,
        displayExpr: 'DisplayName',
        valueExpr: 'ID',
        //value: dataSources[0].ID,
        //onContentReady: function (e) {
        //    e.component.option('value', currentYear);
        //},
        onValueChanged: function (data) {


            var item = data.component.option('selectedItem');

            if (!item) {
                return;
            }

             

            $('#dxSelectYear').dxSelectBox('instance').option('value', item.YearActual);
            $('#dxSelectYearOutlook').dxSelectBox('instance').option('value', item.YearOutlook);
            $('#dxSelectYearBudget').dxSelectBox('instance').option('value', item.YearBudget);


            $('#dxSelectMonth').dxSelectBox('instance').option('value', item.MonthActual);
            $('#dxSelectMonthOutlook').dxSelectBox('instance').option('value', item.MonthOutlook);

            ApplyFilter(); 
            //InitYearActual(dataYearActual);
            //alert(item.MonthActual); 
        }
    }).dxSelectBox('instance');
}

function ReadOnlySelectBox(readOnly) {

    $('#dxSelectYear').dxSelectBox('instance').option('readOnly', readOnly);
    $('#dxSelectMonth').dxSelectBox('instance').option('readOnly', readOnly);

    $('#dxSelectYearOutlook').dxSelectBox('instance').option('readOnly', readOnly);
    $('#dxSelectMonthOutlook').dxSelectBox('instance').option('readOnly', readOnly);

    $('#dxSelectYearBudget').dxSelectBox('instance').option('readOnly', readOnly); 
}

function InitMonthActual() {



    $("#dxSelectMonth").dxSelectBox({
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
            //$('#CurrentMonth').val(e.value);
            //currentYear = e.value;
            //dataGrid.getDataSource().reload();
        }
    }).dxSelectBox('instance');
}

function InitMonthOutlook() {
 


    $("#dxSelectMonthOutlook").dxSelectBox({
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
            //$('#CurrentMonth').val(e.value);
            //currentYear = e.value;
            //dataGrid.getDataSource().reload();
        }
    }).dxSelectBox('instance');
}

function GetSnapshotID() {

    if ($('input[type=radio][name=inlineRadioOptions]:checked').val() == 'DataSource') return 0;
 
    return $("#dxSelectSnapshot").dxSelectBox('instance').option('value');;
}


function InitTableMTDActual(tblName) {

    var year = $("#dxSelectYear").dxSelectBox('instance').option('value');
    var month = $("#dxSelectMonth").dxSelectBox('instance').option('value');


    var dataSource = new DevExpress.data.CustomStore({
        key: "Actual",

        load: function (loadOptions) {
            var loadUrl = baseUrl + "ReportMovement/Get_MTD_Actual_ProfitCenter?year=" + year + "&month=" + month + "&snapshotID=" + GetSnapshotID();
            return loadData(loadUrl, loadOptions);
        },
    });

    //var now = new Date();
    //var date = now.toISOString();
    //dataDate = moment(now).format("YYYY-MM-DD");
    //currentMonth = $('#CurrentMonth').val();
    //currentYear = $('#CurrentYear').val();
    dataGrid = $('#' + tblName).dxDataGrid({
        dataSource: dataSource,
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
        onSelectionChanged: function (e) {
            e.component.repaint();
        },
        selection: {
            mode: "row"
        },

        searchPanel: {
            visible: false
        },
        showBorders: true,
        sorting: {
            mode: "multiple"
        },
        showColumnLines: true,
        showRowLines: true,

        loadPanel: {
            enabled: true
        },
        wordWrapEnabled: true,
        onToolbarPreparing: function (e) {
            var dataGrid = e.component;
            e.toolbarOptions.items.unshift(
                {
                    location: 'before',
                    template() {
                        return $('<div>')
                            .addClass('informers')
                            .append(
                                $('<h4>')
                                    .addClass('count')
                                    .text('Actual'),
                                //$('<span>')
                                //    .addClass('name')
                                //    .text('Total Items'),
                            );
                    },
                },
            )
        },
        columns: [


            { caption: "Source", dataField: "Source", alignment: "left", width: 200 },
            { caption: "Actual", dataField: "Actual", alignment: "left", width: 200 },
            {
                caption: "SN", dataField: "SN",
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
                caption: "CD", dataField: "CD",
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


        ]
    }).dxDataGrid("instance");
}

function InitTableMTDOutlook(tblName) {



    var year = $("#dxSelectYearOutlook").dxSelectBox('instance').option('value');
    var month = $("#dxSelectMonthOutlook").dxSelectBox('instance').option('value');


    var dataSource = new DevExpress.data.CustomStore({
        key: "Outlook",

        load: function (loadOptions) {
            var loadUrl = baseUrl + "ReportMovement/Get_MTD_Outlook_ProfitCenter?year=" + year + "&month=" + month + "&snapshotID=" + GetSnapshotID();
            return loadData(loadUrl, loadOptions);
        },
    });

    //var now = new Date();
    //var date = now.toISOString();
    //dataDate = moment(now).format("YYYY-MM-DD");
    //currentMonth = $('#CurrentMonth').val();
    //currentYear = $('#CurrentYear').val();
    dataGrid = $('#' + tblName).dxDataGrid({
        dataSource: dataSource,
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
        onSelectionChanged: function (e) {
            e.component.repaint();
        },
        selection: {
            mode: "row"
        },

        searchPanel: {
            visible: false
        },
        showBorders: true,
        sorting: {
            mode: "multiple"
        },
        showColumnLines: true,
        showRowLines: true,

        loadPanel: {
            enabled: true
        },
        wordWrapEnabled: true,
        onToolbarPreparing: function (e) {
            var dataGrid = e.component;
            e.toolbarOptions.items.unshift(
                {
                    location: 'before',
                    template() {
                        return $('<div>')
                            .addClass('informers')
                            .append(
                                $('<h4>')
                                    .addClass('count')
                                    .text('Outlook'),
                                //$('<span>')
                                //    .addClass('name')
                                //    .text('Total Items'),
                            );
                    },
                },
            )
        },
        columns: [


            { caption: "Source", dataField: "Source", alignment: "left", width: 200 },
            { caption: "Outlook", dataField: "Outlook", alignment: "left", width: 200 },
            {
                caption: "SN", dataField: "SN",
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
                caption: "CD", dataField: "CD",
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


        ]
    }).dxDataGrid("instance");
}

function InitTableMTDBudget(tblName) {


    var year = $("#dxSelectYearBudget").dxSelectBox('instance').option('value');
    var dataSource = new DevExpress.data.CustomStore({
        key: "Outlook",

        load: function (loadOptions) {
            var loadUrl = baseUrl + "ReportMovement/Get_MTD_Budget_ProfitCenter?year=" + year + "&snapshotID=" + GetSnapshotID();
            return loadData(loadUrl, loadOptions);
        },
    });

    //var now = new Date();
    //var date = now.toISOString();
    //dataDate = moment(now).format("YYYY-MM-DD");
    //currentMonth = $('#CurrentMonth').val();
    //currentYear = $('#CurrentYear').val();
    dataGrid = $('#' + tblName).dxDataGrid({
        dataSource: dataSource,
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
        onSelectionChanged: function (e) {
            e.component.repaint();
        },
        selection: {
            mode: "row"
        },

        searchPanel: {
            visible: false
        },
        showBorders: true,
        sorting: {
            mode: "multiple"
        },
        showColumnLines: true,
        showRowLines: true,

        loadPanel: {
            enabled: true
        },
        wordWrapEnabled: true,
        onToolbarPreparing: function (e) {
            var dataGrid = e.component;
            e.toolbarOptions.items.unshift(
                {
                    location: 'before',
                    template() {
                        return $('<div>')
                            .addClass('informers')
                            .append(
                                $('<h4>')
                                    .addClass('count')
                                    .text('Budget'),
                                //$('<span>')
                                //    .addClass('name')
                                //    .text('Total Items'),
                            );
                    },
                },
            )
        },
        columns: [


            { caption: "Source", dataField: "Source", alignment: "left", width: 200 },
            { caption: "Budget", dataField: "Outlook", alignment: "left", width: 200 },
            {
                caption: "SN", dataField: "SN",
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
                caption: "CD", dataField: "CD",
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


        ]
    }).dxDataGrid("instance");
}

function InitTableMTDActualOutlook(tblName) {



    var year = $("#dxSelectYear").dxSelectBox('instance').option('value');
    var month = $("#dxSelectMonth").dxSelectBox('instance').option('value');

    var yearOutlook = $("#dxSelectYearOutlook").dxSelectBox('instance').option('value');
    var monthOutlook = $("#dxSelectMonthOutlook").dxSelectBox('instance').option('value');

    var dataSource = new DevExpress.data.CustomStore({
        key: "Variance",

        load: function (loadOptions) {
            var loadUrl = baseUrl + "ReportMovement/Get_MTD_Variance_OutlookActual?year=" + year + "&month=" + month + "&yearOutlook=" + yearOutlook + "&monthOutlook=" + monthOutlook + "&snapshotID=" + GetSnapshotID();
            return loadData(loadUrl, loadOptions);
        },
    });

    //var now = new Date();
    //var date = now.toISOString();
    //dataDate = moment(now).format("YYYY-MM-DD");
    //currentMonth = $('#CurrentMonth').val();
    //currentYear = $('#CurrentYear').val();
    dataGrid = $('#' + tblName).dxDataGrid({
        dataSource: dataSource,
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
        onSelectionChanged: function (e) {
            e.component.repaint();
        },
        selection: {
            mode: "row"
        },

        searchPanel: {
            visible: false
        },
        showBorders: true,
        sorting: {
            mode: "multiple"
        },
        showColumnLines: true,
        showRowLines: true,

        loadPanel: {
            enabled: true
        },
        //editing: {
        //    mode: "row",
        //},
        wordWrapEnabled: true,
        onToolbarPreparing: function (e) {
            var dataGrid = e.component;
            e.toolbarOptions.items.unshift(
                {
                    location: 'before',
                    template() {
                        return $('<div>')
                            .addClass('informers')
                            .append(
                                $('<h4>')
                                    .addClass('count')
                                    .text('Variance'),
                                //$('<span>')
                                //    .addClass('name')
                                //    .text('Total Items'),
                            );
                    },
                },
            )
        },
        columns: [


            {
                caption: "Source", alignment: "left"
                , width: 200
                , calculateCellValue: function (data) {


                    return "Primary";
                },
            },
            { caption: "Variance", dataField: "Variance", alignment: "left", width: 200 },
            {
                caption: "SN", dataField: "SN",
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
                caption: "CD", dataField: "CD",
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


        ]
    }).dxDataGrid("instance");
}

function InitTableMTDActualBudget(tblName) {



    var year = $("#dxSelectYear").dxSelectBox('instance').option('value');
    var month = $("#dxSelectMonth").dxSelectBox('instance').option('value');

    var yearBudget = $("#dxSelectYearBudget").dxSelectBox('instance').option('value');

    var dataSource = new DevExpress.data.CustomStore({
        key: "Variance", 
        load: function (loadOptions) {
            var loadUrl = baseUrl + "ReportMovement/Get_MTD_Variance_BudgetActual?year=" + year + "&month=" + month + "&yearBudget=" + yearBudget + "&snapshotID=" + GetSnapshotID();
            return loadData(loadUrl, loadOptions);
        },
    });

    //var now = new Date();
    //var date = now.toISOString();
    //dataDate = moment(now).format("YYYY-MM-DD");
    //currentMonth = $('#CurrentMonth').val();
    //currentYear = $('#CurrentYear').val();
    dataGrid = $('#' + tblName).dxDataGrid({
        dataSource: dataSource,
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
        onSelectionChanged: function (e) {
            e.component.repaint();
        },
        selection: {
            mode: "row"
        },
        //headerFilter: {
        //    visible: true
        //},
        //filterRow: {
        //    visible: true,
        //    applyFilter: "auto"
        //},
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
        //paging: {
        //    pageSize: 10
        //},
        //pager: {
        //    showPageSizeSelector: true,
        //    allowedPageSizes: [10, 20, 50, 100],
        //    showInfo: true
        //},
        loadPanel: {
            enabled: true
        },
        //editing: {
        //    mode: "row",
        //},
        wordWrapEnabled: true,
        onToolbarPreparing: function (e) {
            var dataGrid = e.component;
            e.toolbarOptions.items.unshift(
                {
                    location: 'before',
                    template() {
                        return $('<div>')
                            .addClass('informers')
                            .append(
                                $('<h4>')
                                    .addClass('count')
                                    .text('Variance'),
                                //$('<span>')
                                //    .addClass('name')
                                //    .text('Total Items'),
                            );
                    },
                },
            )
        },
        columns: [


            {
                caption: "Source", alignment: "left"
                , width: 200
                , calculateCellValue: function (data) {


                    return "Primary";
                },
            },
            { caption: "Variance", dataField: "Variance", alignment: "left", width: 200 },
            {
                caption: "SN", dataField: "SN",
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
                caption: "CD", dataField: "CD",
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


        ]
    }).dxDataGrid("instance");
}

function InitTableMTDActualSummaryOutlook(tblName) {

    var year = $("#dxSelectYear").dxSelectBox('instance').option('value');;
    var month = $("#dxSelectMonth").dxSelectBox('instance').option('value');

    var yearOutlook = $("#dxSelectYearOutlook").dxSelectBox('instance').option('value');
    var monthOutlook = $("#dxSelectMonthOutlook").dxSelectBox('instance').option('value');
    var dataSource = new DevExpress.data.CustomStore({
        key: "BudgetOwner", 
        load: function (loadOptions) {
            var loadUrl = baseUrl + "ReportMovement/GetMTDActualSummaryOutlook?year=" + currentYear + "&month=" + month + "&yearOutlook=" + yearOutlook + "&monthOutlook=" + monthOutlook + "&snapshotID=" + GetSnapshotID();
            return loadData(loadUrl, loadOptions);
        },
    });

    //var now = new Date();
    //var date = now.toISOString();
    //dataDate = moment(now).format("YYYY-MM-DD");
    //currentMonth = $('#CurrentMonth').val();
    //currentYear = $('#CurrentYear').val();
    dataGrid = $('#' + tblName).dxDataGrid({
        dataSource: dataSource,
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
        onSelectionChanged: function (e) {
            e.component.repaint();
        },
        selection: {
            mode: "row"
        },
        //headerFilter: {
        //    visible: true
        //},
        //filterRow: {
        //    visible: true,
        //    applyFilter: "auto"
        //},
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
        //paging: {
        //    pageSize: 10
        //},
        //pager: {
        //    showPageSizeSelector: true,
        //    allowedPageSizes: [10, 20, 50, 100],
        //    showInfo: true
        //},
        loadPanel: {
            enabled: true
        },
        //editing: {
        //    mode: "row",
        //},
        wordWrapEnabled: true,
        onToolbarPreparing: function (e) {
            var dataGrid = e.component;
            e.toolbarOptions.items.unshift(
                {
                    location: 'before',
                    template() {
                        return $('<div>')
                            .addClass('informers')
                            .append(
                                $('<h4>')
                                    .addClass('count')
                                    .text('Actual Vs Outlook'),
                                //$('<span>')
                                //    .addClass('name')
                                //    .text('Total Items'),
                            );
                    },
                },
            )
        },
        columns: [


            //{
            //    caption: "Source", alignment: "left"
            //    , width: 200
            //    , calculateCellValue: function (data) {


            //        return "Primary";
            //    },
            //},
            { caption: "Budget Owner", dataField: "BudgetOwner", alignment: "left", width: 200 },
            {
                caption: "SN (%)", dataField: "SN",
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
                caption: "CD (%)", dataField: "CD",
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
                caption: "Actual SN", dataField: "ActualSN",
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
                caption: "Actual CD", dataField: "ActualCD",
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

        ]
    }).dxDataGrid("instance");
}

function InitTableMTDActualSummaryBudget(tblName) {

    var year = $("#dxSelectYear").dxSelectBox('instance').option('value');
    var month = $("#dxSelectMonth").dxSelectBox('instance').option('value');

    var YearBudget = $("#dxSelectYearBudget").dxSelectBox('instance').option('value');
    var dataSource = new DevExpress.data.CustomStore({
        key: "BudgetOwner", 
        load: function (loadOptions) {
            var loadUrl = baseUrl + "ReportMovement/GetMTDActualSummaryBudget?year=" + currentYear + "&month=" + month + "&yearBudget=" + YearBudget + "&snapshotID=" + GetSnapshotID();
            return loadData(loadUrl, loadOptions);
        },
    });

    //var now = new Date();
    //var date = now.toISOString();
    //dataDate = moment(now).format("YYYY-MM-DD");
    //currentMonth = $('#CurrentMonth').val();
    //currentYear = $('#CurrentYear').val();
    dataGrid = $('#' + tblName).dxDataGrid({
        dataSource: dataSource,
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
        onSelectionChanged: function (e) {
            e.component.repaint();
        },
        selection: {
            mode: "row"
        },
        //headerFilter: {
        //    visible: true
        //},
        //filterRow: {
        //    visible: true,
        //    applyFilter: "auto"
        //},
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
        //paging: {
        //    pageSize: 10
        //},
        //pager: {
        //    showPageSizeSelector: true,
        //    allowedPageSizes: [10, 20, 50, 100],
        //    showInfo: true
        //},
        loadPanel: {
            enabled: true
        },
        //editing: {
        //    mode: "row",
        //},
        wordWrapEnabled: true,
        onToolbarPreparing: function (e) {
            var dataGrid = e.component;
            e.toolbarOptions.items.unshift(
                {
                    location: 'before',
                    template() {
                        return $('<div>')
                            .addClass('informers')
                            .append(
                                $('<h4>')
                                    .addClass('count')
                                    .text('Actual Vs Budget'),
                                //$('<span>')
                                //    .addClass('name')
                                //    .text('Total Items'),
                            );
                    },
                },
            )
        },
        columns: [


            //{
            //    caption: "Source", alignment: "left"
            //    , width: 200
            //    , calculateCellValue: function (data) {


            //        return "Primary";
            //    },
            //},
            { caption: "Budget Owner", dataField: "BudgetOwner", alignment: "left", width: 200 },
            {
                caption: "SN (%)", dataField: "SN",
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
                caption: "CD (%)", dataField: "CD",
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
                caption: "Actual SN", dataField: "ActualSN",
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
                caption: "Actual CD", dataField: "ActualCD",
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

        ]
    }).dxDataGrid("instance");
}

function SnapshotMTDActual() {


   

    var dataParams = {
        Name: $('#SnapshotName').val(),
        YearActual: $("#dxSelectYear").dxSelectBox('instance').option('value'),
        MonthActual: $("#dxSelectMonth").dxSelectBox('instance').option('value'),
        YearOutlook: $("#dxSelectYearOutlook").dxSelectBox('instance').option('value'),
        MonthOutlook: $("#dxSelectMonthOutlook").dxSelectBox('instance').option('value'),
        YearBudget: $("#dxSelectYearBudget").dxSelectBox('instance').option('value')
    }







    $.ajax({
        type: 'POST',
        url: baseUrl + 'ReportMovement/SnapshotMTDActual',
        data: dataParams,
        success: function (data) {


            if (data.status == 'success') {
                Swal.fire(
                    "Success",
                    data.result,
                    "success"
                )
            } else {
                Swal.fire(
                    "Error",
                    data.result,
                    "error"
                )
            }
            $('#divModalSnapshot').modal('hide');
        },
        error: function (e) {
            swal("Error", e.statusText, "error");
        },
    });

}


$(function () {
    currentYear = $('#CurrentYear').val();
    currentMonth = $('#CurrentMonth').val();
    InitDataSource();
    InitYearActual();
    InitYearOutlook();
    InitYearBudget();
    InitMonthActual();
    InitMonthOutlook();
    ApplyFilter(); 
    InitRadionEvent();
})



function ApplyFilter() {
    InitTableMTDActual('gridContainerActual');
    InitTableMTDActual('gridContainer2');
    InitTableMTDOutlook('gridContainer3');
    InitTableMTDBudget('gridContainer4');
    InitTableMTDActualOutlook('gridContainer5');
    InitTableMTDActualBudget('gridContainer6');
    InitTableMTDActualSummaryOutlook('gridContainer7');
    InitTableMTDActualSummaryBudget('gridContainer8');
}

function RefreshDataGrid() {
    $("#gridContainerActual").dxDataGrid("instance").refresh();
    $("#gridContainer2").dxDataGrid("instance").refresh();
    $("#gridContainer3").dxDataGrid("instance").refresh();
    $("#gridContainer4").dxDataGrid("instance").refresh();
    $("#gridContainer5").dxDataGrid("instance").refresh();
    $("#gridContainer6").dxDataGrid("instance").refresh();
    $("#gridContainer7").dxDataGrid("instance").refresh();
    $("#gridContainer8").dxDataGrid("instance").refresh();
}
