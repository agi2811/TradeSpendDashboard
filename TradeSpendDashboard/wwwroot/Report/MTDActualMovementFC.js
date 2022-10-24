var dataGrid;
var currentMonth;
var currentYear;
var _URL = baseUrl + "ReportMovement";

var treeList;

var expanded = true;

var getNodeKeys = function (node) {
    var keys = [];
    keys.push(node.key);
    node.children.forEach(function (item) {
        keys = keys.concat(getNodeKeys(item));
    });
    return keys;
}


const dataSnapshot = [
    { ID: 2, Name: 'Snapshot 2 [2022-02-10 11:15]' },
    { ID: 1, Name: 'Snapshot 1 [2022-02-10 12:20]' }

]

const dataSelectSource = [
    { ID: 'Actual', Name: 'Actual' },
    { ID: 'Outlook', Name: 'Outlook' },
    { ID: 'Budget', Name: 'Budget' }

]

const dataSelectProfitCenter = [
    { ID: 'CD', Name: 'CD' },
    { ID: 'SN', Name: 'SN' }

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

 

function ExpandAll() {
    treeList.forEachNode(function (node) {
        treeList.expandRow(node.key);
    });
}

function collapseAll() {
    $treeList.forEachNode(function (node) {
        $treeList.collapseRow(node.key);
    });
}



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

$(function () {
    currentYear = $('#CurrentYear').val();
    currentMonth = $('#CurrentMonth').val();
    //InitDataSource();
    InitSelectSource();
    InitSelectSource2();
    InitSelectProfitCenter();

    InitSelectYear1();
    InitYearOutlook();
    //InitYearBudget();
    InitMonthActual();
    InitMonthOutlook();
    //initTable();
    //ApplyFilter();
    //InitRadionEvent();


})

function initTable() {

    GetMTDColNameList(function (dataColumn) {

        var profitCenter = $('#dxSelectProfitCenter').dxSelectBox('instance').option('value');
        var source1 = $('#dxSelectSource').dxSelectBox('instance').option('value');
        var source2 = $('#dxSelectSource2').dxSelectBox('instance').option('value');
        var source1Year = $('#dxSelectYear1').dxSelectBox('instance').option('value');
        var source1Month = $('#dxSelectMonth1').dxSelectBox('instance').option('value');
        var source2Year = $('#dxSelectYear2').dxSelectBox('instance').option('value');
        var source2month = $('#dxSelectMonth2').dxSelectBox('instance').option('value');


        var Columns = [];
        Columns.push(
            {
                dataField: "Description",
                caption: "Description",
                width: 300,
                fixed: true,
                allowEditing: false
            },
        )

        debugger;

        var CategoryLs = [...new Set(dataColumn.map((d) => { return d.Category }))];

        $.each(CategoryLs, function (i, Category) {

            debugger;
            var filteredCategory = dataColumn.filter((d) => { return d.ColName.includes(Category+ '_Source') && !d.ColName.includes('Total_Source') });

            if (filteredCategory.length > 1) {


                var colHeader = {
                    caption: Category,
                    alignment: "center",
                    cssClass: "dx_treeList_header",
                    columns: []
                };


                $.each(filteredCategory, function (i, dataColumFil) {

                    var colName = dataColumFil.ColName;
                    var caption = '-';
                    debugger;

                    if (colName) {
                        caption = colName.replaceAll('Source1', source1);
                        caption = caption.replaceAll('Source2', source2);
                        caption = caption.replaceAll('_', ' ');
                        caption = caption.replaceAll(Category, '');
                    }

                    colHeader.columns.push(
                        {
                            dataField: colName,
                            caption: caption,
                            width: 150,
                            dataType: "number",
                            fixed: false,
                            cssClass: "dx_treeList_header",
                            format: {
                                type: "fixedPoint",
                                precision: 10
                            },
                        }
                    );

                });



                Columns.push(colHeader);
            }
             



        });

        var DataColTotal = dataColumn.filter((d) => { return d.ColName.includes('Total_Source') });

        $.each(DataColTotal, function (i, ColTotal) {

            var colName = ColTotal.ColName;
            var caption = '-';
          

            if (colName) {
                caption = colName.replaceAll('Source1', source1);
                caption = caption.replaceAll('Source2', source2);
                caption = caption.replaceAll('_', ' ');
            }

            Columns.push(
                {
                    dataField: colName,
                    caption: caption,
                    width: 150,
                    dataType: "number",
                    fixed: false,
                    format: {
                        type: "fixedPoint",
                        precision: 2
                    },
                }
            );

        });

        //$.each(dataColumn, function (i, dataCol) {

        //    var colName = dataCol.ColName;



        //    if (colName != 'ID' || colName != 'SourceID' || colName != 'Source' || colName != 'ParentID' || colName != 'Year' || colName != 'Level') {

        //        var caption = '-';
        //        if (colName) {
        //            caption = colName.replaceAll('Source1', source1);
        //            caption = caption.replaceAll('Source2', source2);
        //            caption = caption.replaceAll('_', ' ');
        //        }

        //            Columns.push(
        //                {
        //                    dataField: colName,
        //                    caption: caption,
        //                    width: 150,
        //                    dataType: "number",
        //                    fixed: false,
        //                    format: {
        //                        type: "fixedPoint",
        //                        precision: 2
        //                    },
        //                }
        //            );




        //    }



        //});




        var dataSource = new DevExpress.data.CustomStore({
            key: "ID",
            load: function (loadOptions) {
                var loadUrl = baseUrl + "ReportMovement/GetFC_vs_Budget?profitCenter=" + profitCenter + "&source1=" + source1 + "&source2=" + source2 + "&source1Year=" + source1Year + "&source1Month=" + source1Month + "&source2Year=" + source2Year + "&source2month=" + source2month;
                return loadData(loadUrl, loadOptions);
            },
        });


        $("#gridContainer").dxTreeList({
            dataSource: dataSource,
            keyExpr: "ID",
            //height: 'calc(100vh - 125px)',
            parentIdExpr: "ParentID",
            columnAutoWidth: true,
            wordWrapEnabled: true,
            showBorders: true,
            //expandedRowKeys: [1, 5,41,45], 
            scrolling: {
                useNative: 'auto',
                //scrollByContent: true,
                scrollByThumb: true,
                showScrollbar: "always" // or "onScroll" | "always" | "never"
            },
            export: {
                enabled: true
            },
            loadPanel: {
                enabled: true
            },
            headerFilter: {
                visible: true
            },
            //editing: {
            //    refreshMode: "reshape",
            //    mode: "row",
            //    allowUpdating: true,
            //    useIcons: true
            //},
            columnFixing: {
                enabled: true
            },
            onInitialized: function (e) {
                treeList = e.component;

            },
            onNodesInitialized: function (e) {
                ExpandAll();

            },
            //onEditingStart: function (e) {

            //    SetAllowEditingMonth(e);


            //},
            //onRowUpdated: function (e) {
            //    SetAllowEditingAllMonth(e, false);


            //},
            //onEditorPreparing: function (e) {


            //},
            onCellPrepared: function (e) {

                //var remove = true;
                //if (e.rowType === "data" && e.column.command == "edit") {


                //    if (!e.data.IsAllowEdit || !(UserIsAllowUpdateBudgetMonitoring)) {
                //        e.cellElement.find(".dx-link-icon").remove();
                //    } else {
                //        var test = 1;
                //        if (e.data.LevelID == 3) {
                //            if (e.data.SubID == 3 && (UserIsFinanceController || UserIsRMM)) {

                //                remove = false;

                //            }
                //            else if ((e.data.SubID == 5 || e.data.SubID == 6) && UserIsFinanceController) {

                //                remove = false;
                //            }
                //        }

                //    }




                //}

                //if (remove) {
                //    e.cellElement.find(".dx-link-icon").remove();
                //}

                if (e.rowType === "data") {

                    if (e.data.Level == 1) {
                        e.cellElement.css("background-color", "#B0C4DE");
                    } else if (e.data.Level == 2) {
                        e.cellElement.css("background-color", "#ADD8E6");
                    } else if (e.data.Level == 3) {
                        e.cellElement.css("background-color", "#B0E0E6");
                    }

                    //if (e.data.LevelID == 1 && e.data.RowType == 'data') {
                    //    e.cellElement.css("background-color", "#8db4e2");

                    //} else if (e.data.LevelID == 2 && e.data.RowType == 'data') {
                    //    e.cellElement.css("background-color", "#daeef3");

                    //} else if (e.data.LevelID == 3 && e.data.RowType == 'data') {
                    //    e.cellElement.css("background-color", "#f4f9ff");

                    //}

                    //if (e.data.RowType == 'footer') {
                    //    e.cellElement.css("font-weight", "bold");
                    //}
                }

            },

            columns: Columns
        });

    });





}



function GetMTDColNameList(callback) {




    var profitCenter = $('#dxSelectProfitCenter').dxSelectBox('instance').option('value');
    var source1 = $('#dxSelectSource').dxSelectBox('instance').option('value');
    var source2 = $('#dxSelectSource2').dxSelectBox('instance').option('value');







    $.ajax({
        type: 'POST',
        url: baseUrl + 'ReportMovement/GetMTDColNameList',
        data: {
            'profitCenter': profitCenter,
            'source1': source1,
            'source2': source2,
        },
        success: function (data) {

            if (typeof (callback) === "function") {
                callback(data.result);
            }
        },
        error: function (e) {
            console.log(e.statusText);
        },
    });

}


function collapseAll() {
    $treeList.forEachNode(function (node) {
        $treeList.collapseRow(node.key);
    });
}

function sendRequest2(url, method, data) {

    var d = $.Deferred();


    method = method || "GET";

    //logRequest(method, url, data);

    $.ajax(url, {
        method: method || "GET",
        data: data,
        cache: false,
        xhrFields: { withCredentials: true }
    }).done(function (result) {


        d.resolve(result);

        if (method === "POST") {

            $("#gridContainer").dxTreeList("instance").refresh();
        }

    }).fail(function (xhr) {
        d.reject(xhr.responseJSON ? xhr.responseJSON.Message : xhr.statusText);
    });

    return d.promise();
}

function ShowModalSnapshot() {
    $('#SnapshotName').val('');
    $('#divModalSnapshot').modal('show');
}



function InitSelectSource() {
    $("#dxSelectSource").dxSelectBox({
        dataSource: new DevExpress.data.ArrayStore({
            data: dataSelectSource,
            key: 'ID',
        }),
        displayExpr: 'Name',
        valueExpr: 'ID',
        value: dataSelectSource[0].ID,
        //onValueChanged: function (data) {
        //    const value = data.value;
        //    if (value == 1) {
        //        $("#divYear").css("display", "block");
        //        $("#divMonth").css("display", "block");
        //        $("#divList").css("display", "none");
        //    } else {
        //        $("#divYear").css("display", "none");
        //        $("#divMonth").css("display", "none");
        //        $("#divList").css("display", "block");
        //    }
        //}
    }).dxSelectBox('instance');
}

function InitSelectProfitCenter() {
    $("#dxSelectProfitCenter").dxSelectBox({
        dataSource: new DevExpress.data.ArrayStore({
            data: dataSelectProfitCenter,
            key: 'ID',
        }),
        displayExpr: 'Name',
        valueExpr: 'ID',
        value: dataSelectProfitCenter[0].ID,
        //onValueChanged: function (data) {
        //    const value = data.value;
        //    if (value == 1) {
        //        $("#divYear").css("display", "block");
        //        $("#divMonth").css("display", "block");
        //        $("#divList").css("display", "none");
        //    } else {
        //        $("#divYear").css("display", "none");
        //        $("#divMonth").css("display", "none");
        //        $("#divList").css("display", "block");
        //    }
        //}
    }).dxSelectBox('instance');
}

function InitSelectSource2() {
    $("#dxSelectSource2").dxSelectBox({
        dataSource: new DevExpress.data.ArrayStore({
            data: dataSelectSource,
            key: 'ID',
        }),
        displayExpr: 'Name',
        valueExpr: 'ID',
        value: dataSelectSource[1].ID,
        //onValueChanged: function (data) {
        //    const value = data.value;
        //    if (value == 1) {
        //        $("#divYear").css("display", "block");
        //        $("#divMonth").css("display", "block");
        //        $("#divList").css("display", "none");
        //    } else {
        //        $("#divYear").css("display", "none");
        //        $("#divMonth").css("display", "none");
        //        $("#divList").css("display", "block");
        //    }
        //}
    }).dxSelectBox('instance');
}

function InitSelectYear1() {

    $('#dxSelectYear1').dxSelectBox({
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


    $("#dxSelectYear2").dxSelectBox({
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



            $('#dxSelectYear1').dxSelectBox('instance').option('value', item.YearActual);
            $('#dxSelectYear2').dxSelectBox('instance').option('value', item.YearOutlook);
            $('#dxSelectYearBudget').dxSelectBox('instance').option('value', item.YearBudget);


            $('#dxSelectMonth1').dxSelectBox('instance').option('value', item.MonthActual);
            $('#dxSelectMonth2').dxSelectBox('instance').option('value', item.MonthOutlook);

            ApplyFilter();
            //InitSelectYear1(dataYearActual);
            //alert(item.MonthActual); 
        }
    }).dxSelectBox('instance');
}

function ReadOnlySelectBox(readOnly) {

    $('#dxSelectYear1').dxSelectBox('instance').option('readOnly', readOnly);
    $('#dxSelectMonth1').dxSelectBox('instance').option('readOnly', readOnly);

    $('#dxSelectYear2').dxSelectBox('instance').option('readOnly', readOnly);
    $('#dxSelectMonth2').dxSelectBox('instance').option('readOnly', readOnly);

    $('#dxSelectYearBudget').dxSelectBox('instance').option('readOnly', readOnly);
}

function InitMonthActual() {



    $('#dxSelectMonth1').dxSelectBox({
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



    $("#dxSelectMonth2").dxSelectBox({
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







function SnapshotMTDActual() {




    var dataParams = {
        Name: $('#SnapshotName').val(),
        YearActual: $('#dxSelectYear1').dxSelectBox('instance').option('value'),
        MonthActual: $('#dxSelectMonth1').dxSelectBox('instance').option('value'),
        YearOutlook: $("#dxSelectYear2").dxSelectBox('instance').option('value'),
        MonthOutlook: $("#dxSelectMonth2").dxSelectBox('instance').option('value'),
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


