var pageType = $("#pageType").val();
var roleName = $("#roleName").val();
var visibleAdd = (pageType === "form") ? true : false;
debugger;
var visibleCcs = (roleName !== "HOATC" || roleName !== "HOCF") ? false : true;
if (pageType === "")
    pageType = "form";
var urlApproval = baseUrl + "RequestContract/SaveAllData";

function ClickSubmit(RequestID, FlowProcessStatusID, OldFlowProcessStatusID, mandatory) {
    debugger;
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
        //if (!status) {
        //    Swal.fire({
        //        title: "Cannot Submit Data",
        //        text: "Please Add the Item data before Submit",
        //        icon: "warning",
        //        showCancelButton: true,
        //        cancelButtonText: 'No'
        //    })
        //    return false;
        //}
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
                confirmButtonText: 'Commit Comment!',
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
                                SaveDataContract(RequestID, FlowProcessStatusID, OldFlowProcessStatusID, mandatory, inputValue);
                            }, 2000);
                        }
                    } else {
                        setTimeout(function () {
                            loadPanel.show();
                            SaveDataContract(RequestID, FlowProcessStatusID, OldFlowProcessStatusID, mandatory, inputValue);
                        }, 2000);
                    }
                }
            })
        }
    });

    function SaveDataContract(RequestID, FlowProcessStatusID, OldFlowProcessStatusID, mandatory, inputValue) {
        debugger;
        var reqId = $("#reqID").val();
        var refId = $("#referenceId").dxSelectBox("instance").option("value");
        var from = $("#DateFrom").dxDateBox("instance").option("value");
        var to = $("#DateTo").dxDateBox("instance").option('value');
        var fromGeneral = moment(from).format("YYYY-MMM-DD");
        var toGeneral = moment(to).format("YYYY-MMM-DD");
        var total = $("#totalOutlet").val();
        var pengelola = $("input[name=pengelola]:checked").val();
        var note = document.getElementById("notes").value;
        var groupName = $("#namaGroupOutlet").val();
        var reqId = document.getElementById("reqID").value;
        var comment = inputValue.value;
        if (reqId === "") {
            reqId = 0;
        }

        var data = {
            request: {
                Id: reqId,
                FlowProcessStatusId: FlowProcessStatusID,
                Comment: comment
            },
            param: {
                PeriodFrom: fromGeneral,
                PeriodTo: toGeneral,
                ReferenceRequestId: refId,
                RequestId: reqId,
                Id: reqId,
                GroupOutletName: groupName,
                TotalOutlet: total,
                PengelolaOutlet: pengelola,
                Notes: note
            }
        }

        jQuery.ajax({
            async: false,
            url: urlApproval,
            type: "POST",
            data: data,
            beforeSend: function () {
                loadPanel.show();
            },
            success: function (resp) {
                var respond = resp.data;
                $("#reqID").val(respond.requestId);
                ReqId = respond.requestId;
                Swal.fire("Success", "Success Save Data", "success");
                loadPanel.hide();
                if (FlowProcessStatusID !== "17" && FlowProcessStatusID !== "35") {
                    location.href = baseUrl + "Request/MyTask";
                } else {
                    if (FlowProcessStatusID === "35") {
                        location.href = baseUrl + "RequestContract";
                    } else {
                        location.href = baseUrl + "RequestRenewal/Form/" + respond.requestId;
                    }
                }
            },
            error: function (err) {
                Swal.fire("Error", "Failed Save Data", "error");
            }
        })
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

}