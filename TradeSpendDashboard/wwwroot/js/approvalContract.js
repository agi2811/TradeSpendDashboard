var pageType = $("#pageType").val();
var roleName = $("#roleName").val();
var visibleAdd = (pageType === "form") ? true : false;
var visibleCcs = (roleName !== "HOATC" || roleName !== "HOCF") ? false : true;
if (pageType === "")
    pageType = "form";
var urlApproval = baseUrl + "RequestContract/SaveAllData";

function ClickSubmit(RequestID, FlowProcessStatusID, OldFlowProcessStatusID, mandatory) {
    debugger;
    var hasError = validateForm();
    if (hasError !== 0) {
        Swal.fire({
            title: "Warning",
            text: "Please Complete the required field!",
            icon: "warning"
        });
        return false;
    }

    if (FlowProcessStatusID === "35" || FlowProcessStatusID === "21") {
        var statusContract = cekStatusContract();
        if (statusContract === true) {
            Swal.fire(
                'information!',
                'This Contract is as Above Guideline!',
                'info'
            )
        }

        var status = cekDetailContract();
        if (!status) {
            Swal.fire({
                title: "Cannot Submit Data",
                text: "Please Add the Item data before Submit",
                icon: "warning",
                showCancelButton: true,
                cancelButtonText: 'No'
            })
            return false;
        }
    }

    Swal.fire({
        title: "Are you sure?",
        text: "Save This Data!",
        icon: "question",
        showCancelButton: true,
        confirmButtonText: 'Yes',
        cancelButtonText: 'No',
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                title: 'Enter your Comment!',
                input: 'textarea',
                confirmButtonText: 'Send Comment!',
                showCancelButton: true,
                cancelButtonText: 'Cancel'
            }).then((inputValue) => {
                if (inputValue.isConfirmed) {
                    loadPanel.show();
                    if (FlowProcessStatusID !== "17") {
                        var comment = inputValue.value;
                        if (comment === "" && FlowProcessStatusID !== "17") {
                            loadPanel.hide();
                            Swal.fire(
                                'Warning!',
                                'Plese Input comment for this request!',
                                'warning'
                            )
                            return false;
                        } else {
                            setTimeout(function () {
                                loadPanel.show();
                                saveDataApproval(RequestID, FlowProcessStatusID, OldFlowProcessStatusID, mandatory, inputValue);
                            }, 2000);
                        }
                    } else {
                        setTimeout(function () {
                            loadPanel.show();
                            saveDataApproval(RequestID, FlowProcessStatusID, OldFlowProcessStatusID, mandatory, inputValue);
                        }, 2000);
                    }
                }
            })
        }
    })
}

function saveDataApproval(RequestID, FlowProcessStatusID, OldFlowProcessStatusID, mandatory, inputValue) {
    var from = $("#DateFrom").dxDateBox("instance").option("value");
    var to = $("#DateTo").dxDateBox("instance").option('value');
    var fromGeneral = moment(from).format("YYYY-MMM-DD");
    var toGeneral = moment(to).format("YYYY-MMM-DD");
    var distributor = $("#distributor").dxSelectBox("instance").option("value");
    var subChannel = $("#subChannel").dxSelectBox("instance").option("value");
    var outlet = $("#outlet").dxSelectBox("instance").option("value");
    var total = $("#totalOutlet").dxNumberBox("instance").option("value");
    var termOfPayment = $("#terms").dxNumberBox("instance").option("value");
    var pengelola = $("input[name=pengelola]:checked").val();
    var groupName = $("#namaGroupOutlet").val();
    if (pageType === "form") {
        groupName = $("#namaGroupOutlet").dxSelectBox("instance").option("value");
    }

    var note = $("#notes").val();
    var reqId = $("#reqID").val();

    if (reqId === "") {
        reqId = 0;
    }

    var dataForm = {
        param: {
            PeriodFrom: fromGeneral,
            PeriodTo: toGeneral,
            RequestId: reqId,
            Id: reqId,
            GroupOutletId: groupName,
            GroupOutletName: groupName,
            DistributorCode: distributor,
            OutletCode: outlet,
            TotalOutlet: total,
            TermOfPayment: termOfPayment,
            PengelolaOutlet: pengelola,
            Notes: note,
            SubChannelId: subChannel
        },
        request: {
            Id: reqId,
            FlowProcessStatusId: FlowProcessStatusID,
            Comment: inputValue.value
        }
    }

    jQuery.ajax({
        async: false,
        url: urlApproval,
        type: "POST",
        data: dataForm,
        beforeSend: function () {
            loadPanel.show();
            //loadPanelSaveData.show();
        },
        success: function (resp) {
            var respond = resp.data;
            $("#reqID").val(respond.requestId);
            ReqId = respond.requestId;
            Swal.fire("Success", "Success Save Data", "success");
            loadPanel.hide();
            if (FlowProcessStatusID === "17" || FlowProcessStatusID === "35") {
                location.href = baseUrl + "RequestContract";
            } else {
                location.href = baseUrl + "Request/MyTask";
            }
        },
        error: function (err) {
            Swal.fire("Error", "Failed Save Data", "error");
            loadPanel.hide();
        }
    })
    loadPanel.hide();
}

function cekDetailContract() {
    var ID = $("#reqID").val();
    var hasItem = false;

    jQuery.ajax({
        async: false,
        url: baseUrl + "RequestContract/GetDataDetail/?Id=" + ID,
        type: "GET",
        success: function (resp) {
            if (resp.status === "success") {
                if (resp.result.length > 0) {
                    hasItem = true;
                } else {
                    hasItem = false;
                }
            } else {
                hasItem = false;
            }
        },
        error: function (err) {
            hasItem = false;
        }
    })
    return hasItem;
}

function cekStatusContract() {
    var ID = $("#reqID").val();
    var hasItem = false;
    jQuery.ajax({
        async: false,
        url: baseUrl + "RequestContract/GetTypeContractAsync/?ReqId=" + ID,
        type: "GET",
        success: function (resp) {
            if (resp.status === "success") {
                hasItem = resp.result;
            } else {
                hasItem = false;
            }
        },
        error: function (err) {
            hasItem = false;
        }
    })
    return hasItem;
}

function validateForm() {
    isValid1 = $("#distributor").dxSelectBox("instance").option("isValid");
    isValid2 = $("#outlet").dxSelectBox("instance").option("isValid");
    isValid3 = $("#subChannel").dxSelectBox("instance").option("isValid");
    //isValid5 = $("#namaGroupOutlet").dxSelectBox("instance").option("isValid");
    isValid6 = $("#totalOutlet").dxNumberBox("instance").option("isValid");
    isValid4 = $("#terms").dxNumberBox("instance").option("isValid");
    isValid7 = $("#DateFrom").dxDateBox("instance").option("isValid");
    isValid8 = $("#DateTo").dxDateBox("instance").option("isValid");
    var arr = [isValid1, isValid2, isValid3, isValid4, /*isValid5,*/ isValid6, isValid7, isValid8];
    var result = jQuery.inArray(false, arr);
    return result + 1;
}