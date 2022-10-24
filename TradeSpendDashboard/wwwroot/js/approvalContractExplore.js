var urlApproval = baseUrl + "RequestContract/SaveAllData";

function ClickSubmit(RequestID, FlowProcessStatusID, OldFlowProcessStatusID, mandatory) {
    debugger;
    if (!validateForm()) {
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
                            debugger;
                            setTimeout(function () {
                                loadPanel.show();
                                saveDataApproval(RequestID, FlowProcessStatusID, OldFlowProcessStatusID, mandatory, inputValue);
                            }, 1000);
                        }
                    } else {
                        setTimeout(function () {
                            loadPanel.show();
                            saveDataApproval(RequestID, FlowProcessStatusID, OldFlowProcessStatusID, mandatory, inputValue);
                        }, 1000);
                    }
                }
            })
        }
    })
}

function saveDataApproval(RequestID, FlowProcessStatusID, OldFlowProcessStatusID, mandatory, inputValue) {
    var reqId = contract.reqId;
    if (showRef) {
        if (!contract.referenceRequestId) {
            Swal.fire({
                title: "Warning",
                text: "Please Complete the required field!",
                icon: "warning"
            });
            return false;
        }
    }

    var resultValidasi = validasiContract();
    if (!resultValidasi.status) {
        return false;
    }

    var dataForm = {
        param: {
            ReferenceRequestId: contract.referenceRequestId,
            PeriodFrom: moment(contract.from).format("YYYY-MM-DD"),
            PeriodTo: moment(contract.to).format("YYYY-MM-DD"),
            RequestId: contract.reqId,
            Id: contract.reqId,
            GroupOutletId: contract.namaGroupOutlet,
            GroupOutletName: contract.namaGroupOutlet,
            DistributorCode: contract.distributor,
            OutletCode: contract.outlet,
            TotalOutlet: contract.totalOutlet,
            TermOfPayment: contract.terms,
            PengelolaOutlet: contract.pengelola,
            Notes: contract.note,
            SubChannelId: contract.subChannel
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
        },
        success: function (resp) {
            var respond = resp.data;
            form.updateData("reqId", respond.requestId);
            ReqId = respond.requestId;
            Swal.fire("Success", "Success Save Data", "success");
            loadPanel.hide();
            if (FlowProcessStatusID === "17" || FlowProcessStatusID === "35") {
                if (FlowProcessStatusID === "35") {
                    location.href = baseUrl + "RequestContract";
                }
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
    var ID = contract.reqId;
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
    var ID = contract.reqId;
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
    var validate = form.validate();
    if (validate.isValid) {
        return true;
    }
    return false;
}