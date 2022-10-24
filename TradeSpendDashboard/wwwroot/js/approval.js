var urlApproval = baseUrl + "RequestGuideLine/SaveAllData";
function ClickSubmit(RequestID, FlowProcessStatusID, OldFlowProcessStatusID, mandatory) {
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
                    if (comment === "" && FlowProcessStatusID !== "1") {
                        Swal.fire(
                            'Warning!',
                            'Plese Input comment for this request!',
                            'warning'
                        )
                        return false;
                    }

                    var from = $("#DateFrom").dxDateBox("instance").option('value');
                    var to = $("#DateTo").dxDateBox("instance").option('value');
                    var effectiveDate = $("#EffectiveDate").dxDateBox("instance").option('value');
                    var fromGeneral = moment(from).format("YYYY-MMM-DD");
                    var toGeneral = moment(to).format("YYYY-MMM-DD");
                    var effectiveDateGeneral = moment(effectiveDate).format("YYYY-MMM-DD");
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
                                FlowProcessStatusID: FlowProcessStatusID,
                                EffectiveDate: effectiveDateGeneral,
                                PeriodTo: toGeneral,
                                RequestId: reqId,
                                Comment: inputValue.value,
                                Id: reqId
                            },
                            request: {
                                Id: reqId,
                                FlowProcessStatusId: FlowProcessStatusID,
                                Comment: comment
                            },
                            oldFlowId: OldFlowProcessStatusID
                        },
                        beforeSend: function () {
                            //loadPanelSaveData.show();
                        },
                        success: function (resp) {
                            var respond = resp.data;
                            $("#reqID").val(respond.requestId);
                            ReqId = respond.requestId;
                            Swal.fire("Success", "Success Save Data", "success");
                            location.href = baseUrl + "RequestGuideLine";
                        },
                        error: function (err) {
                            Swal.fire("Error", "Failed Save Data", "error");
                        }
                    })
                }
            })
        }
    })
}