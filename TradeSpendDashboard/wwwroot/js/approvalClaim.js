var urlApproval = baseUrl + "RequestClaim/SaveAllData";
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
                confirmButtonText: 'Commit Comment!'
            }).then((inputValue) => {
                var from = moment($("#Period").dxDateBox("instance").option('value')).format("YYYY-MM-DD");
               

                var comment = inputValue.value;
                var reqId = document.getElementById("reqID").value;
                if (reqId === "") {
                    reqId = 0;
                }

                 
                //if (inputValue === false) {
                //    return false;
                //}
                //if (mandatory) {
                //    if (inputValue === "" || inputValue == null) {
                //        Swal.fire("You need to write something!", "", "error");
                //        return false;
                //    }
                //}

                jQuery.ajax({
                    async: false,
                    url: baseUrl + "Claim/SaveAllData",
                    type: "POST",
                    data: {
                        param: {
                            Period: from,
                            RequestId: reqId
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
                    },
                    error: function (err) {
                        Swal.fire("Error", "Failed Save Data", "error");
                    }
                })
            })
        }
    })
}