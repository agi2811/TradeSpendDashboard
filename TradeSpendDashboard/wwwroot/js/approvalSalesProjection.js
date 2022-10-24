var urlApproval = baseUrl + "SalesProjection/SaveAllData";

function validasiSalesProjection() {
    var result = {};
    var dataFrom = $("#DateFrom").dxDateBox("instance").option("value");
    var strDateFrom = moment(dataFrom).format("yyyy-MM-DD");

    jQuery.ajax({
        url: baseUrl + "SalesProjection/SalesProjectionValidate?period=" + strDateFrom,
        type: "GET",
        async: false,
        success: function (resp) {
            if (resp.status === "success") {
                var data = resp.data;
                result.status = data.result;
                result.msg = data.message;
            } else {
                result.status = false;
                result.msg = data.message;
            }
        },
        error: function (err) {
            result.status = false;
            result.msg = data.message;
        }
    })
    if (!result.status) {
        Swal.fire({
            title: "Warning",
            text: result.msg,
            icon: "warning"
        });
    }
    return result;
}

function ClickSubmit(RequestID, FlowProcessStatusID, OldFlowProcessStatusID, mandatory) {
    debugger;
    if (FlowProcessStatusID !== "41") {
        var resultValidation = validasiSalesProjection();
        if (!resultValidation.status) {
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
                    var comment = inputValue.value;
                    if (comment === "" && FlowProcessStatusID !== "10") {
                        Swal.fire(
                            'Warning!',
                            'Plese Input comment for this request!',
                            'warning'
                        )
                        return false;
                    }

                    var from = $("#DateFrom").dxDateBox("instance").option('value');
                    var to = $("#DateTo").dxDateBox("instance").option('value');
                    var fromGeneral = moment(from).format("YYYY-MMM-DD");
                    var toGeneral = moment(to).format("YYYY-MMM-DD");
                    var BASname = document.getElementById("basdata").value;
                    var zone = document.getElementById("zone").value;
                    var subzone = document.getElementById("subzone").value;

                    var comment = inputValue.value;
                    var reqId = document.getElementById("reqID").value;
                    if (reqId === "") {
                        reqId = 0;
                    }

                    jQuery.ajax({
                        async: false,
                        url: urlApproval,
                        type: "POST",
                        data: {
                            param: {
                                PeriodFrom: fromGeneral,
                                PeriodTo: toGeneral,
                                RequestId: reqId,
                                Id: reqId,
                                Name: BASname,
                                Zone: zone,
                                SubZone: subzone
                            },
                            request: {
                                Id: reqId,
                                FlowProcessStatusId: FlowProcessStatusID,
                                Comment: comment
                            }
                        },
                        beforeSend: function () {
                            //loadPanelSaveData.show();
                        },
                        success: function (resp) {
                            var respond = resp.data;
                            $("#reqID").val(respond.requestId);
                            ReqId = respond.requestId;
                            Swal.fire("Success", "Success Save Data", "success");
                            location.href = baseUrl + "Request/SalesProjectionList";
                        },
                        error: function (err) {
                            var respond = err.responseJSON.result;
                            debugger;
                            Swal.fire("Error", respond, "error");
                        }
                    })
                }
            })
        }
    })
}