var app = {
    alertSuccess: function (message) {
        DevExpress.ui.notify({
            message: message,
            width: 300,
            position: {
                my: "bottom right",
                at: "bottom right",
                offsite: "-10 -10"
            }
        }, "success", 3000);
    },
    alertError: function (message) {
        DevExpress.ui.notify({
            message: message,
            width: 300,
            position: {
                my: "bottom right",
                at: "bottom right",
                offsite: "-10 -10"
            }
        }, "error", 3000);
    },
    dialogConfirm: function (options) {
        var customDialog = DevExpress.ui.dialog.custom({
            title: options.title || "Dialog Title",
            messageHtml: options.html || "<div></div>",
            buttons: [{
                text: "Confirm",
                icon: "fas fa-check-circle",
                type: "danger",
                onClick: function (e) {
                    return true
                }
            }, {
                text: "Cancel",
                icon: "fas fa-times-circle",
                onClick: function (e) {
                    return false
                }
            }]
        });

        customDialog.show().done(function (dialogResult) {
            if (dialogResult) {
                if (typeof (options.onConfirm) === "function") {
                    options.onConfirm();
                }
            } else {
                if (typeof (options.onCancel) === "function") {
                    options.onCancel();
                }
            }

        });
    },
    logoutConfirm: function () {
        this.dialogConfirm({
            title: "Logout",
            html: "<p>Would you like to logout?</p>",
            onConfirm: function () {
                window.location.href = baseUrl + "Account/Logout"
            }
        });
    },
    renderEditorWithCustomHelpText: function (data, itemElement, options) {
        var elemenItemLabel = itemElement.parent().children().first();
        var elemenConentLabel = elemenItemLabel.children().first();
        var spanLast = elemenConentLabel.children().last();

        elemenConentLabel.attr("id", "label-content-help-" + data.component.getItemID() + "-" + + data.dataField);

        var btnId = "label-help-" + data.component.getItemID() + "-" + + data.dataField;

        var btnHelp = $("<span>")
            .dxButton({
                elementAttr: {
                    id: btnId,
                    class: "label-icon-help"
                },
                tabIndex: -1,
                icon: "fas fa-question-circle",
                text: ""
            });

        spanLast.append(btnHelp);

        var tooltip = $("<div>" + options.helpText + "</div>")
            .dxTooltip({
                target: "#" + btnId,
                showEvent: "mouseenter",
                hideEvent: "mouseleave",
                closeOnOutsideClick: false,
                maxWidth: function () {
                    return window.innerWidth / 2.5;
                },
                elementAttr: {
                    class: "label-tooltip-help"
                }
            });

        btnHelp.append(tooltip);

        var editorOptions = Object.assign({}, data.editorOptions);
        var _validationRules = Object.assign({}, options.validationRules);

        editorOptions.value = data.component.option('formData')[data.dataField];
        editorOptions.onValueChanged = function (e) {
            var oldValue = data.component.option('formData')[data.dataField];
            if (oldValue !== e.value) {
                data.component.updateData(data.dataField, e.value);
            }
        }

        var _customEditor = null;

        if (!data.component.customEditor) {
            data.component.customEditor = [];
        }

        switch (data.editorType) {
            case "dxSelectBox":
                _customEditor = $("<div>").dxSelectBox(editorOptions)
                    .dxValidator({ validationRules: _validationRules, validationGroup: options.validationGroup });
                data.component.customEditor[data.dataField] = _customEditor.dxSelectBox("instance");
                break;
            case "dxTagBox":
                _customEditor = $("<div>").dxTagBox(editorOptions)
                    .dxValidator({ validationRules: _validationRules, validationGroup: options.validationGroup });
                data.component.customEditor[data.dataField] = _customEditor.dxTagBox("instance");
                break;
            case "dxSwitch":
                _customEditor = $("<div>").dxSwitch(editorOptions)
                    .dxValidator({ validationRules: _validationRules, validationGroup: options.validationGroup });
                data.component.customEditor[data.dataField] = _customEditor.dxSwitch("instance");
                break;
            case "dxNumberBox":
                _customEditor = $("<div>").dxNumberBox(editorOptions)
                    .dxValidator({ validationRules: _validationRules, validationGroup: options.validationGroup });
                data.component.customEditor[data.dataField] = _customEditor.dxNumberBox("instance");
                break;
            case "dxDateBox":
                _customEditor = $("<div>").dxDateBox(editorOptions)
                    .dxValidator({ validationRules: _validationRules, validationGroup: options.validationGroup });
                data.component.customEditor[data.dataField] = _customEditor.dxDateBox("instance");
                break;
            case "dxTextArea":
                _customEditor = $("<div>").dxTextArea(editorOptions)
                    .dxValidator({ validationRules: _validationRules, validationGroup: options.validationGroup });
                data.component.customEditor[data.dataField] = _customEditor.dxTextArea("instance");
                break;
            default:
                _customEditor = $("<div>").dxTextBox(editorOptions)
                    .dxValidator({ validationRules: _validationRules, validationGroup: options.validationGroup });
                data.component.customEditor[data.dataField] = _customEditor.dxTextBox("instance");
                break;
        }

        itemElement.append(_customEditor);
    },
    sendRequest: function (url, method, data) {
        var that = this;
        var d = $.Deferred();
        var model = data;

        $.ajax(url, {
            type: method,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(model),
            cache: false,
        }).done(function (result, response) {
            if (!result.isError && result.data) {

                d.resolve();

                that.alertSuccess(response);

                return result.data;
            }

            that.alertError(result.message);
            d.reject(result.message);
        }).fail(function (xhr, response) {
            d.reject(xhr.responseText);
            that.alertError("Error status : " + xhr.status + xhr.responseText);
        });

        return d.promise();
    },
    customStore: function (options) {
        var that = this;
        var store = new DevExpress.data.CustomStore({
            key: options.key,
            load: function (loadOptions) {
                return that.loadData(options.loadUrl, loadOptions);
            },
            insert: function (values) {
                return that.sendRequest(options.insertUrl, "POST", values);
            },
            update: function (key, values) {
                return that.sendRequest(options.updateUrl + "/" + key, "PUT", values);
            },
            remove: function (key) {
                return that.sendRequest(options.deleteUrl + "/" + key, "DELETE");
            }
        });

        return store;
    },
    customStoreRoleDetail: function (options) {
        var that = this;
        var store = new DevExpress.data.CustomStore({
            key: options.key,
            load: function (loadOptions) {
                return that.loadData(options.loadUrl, loadOptions);
            },
            dataType: "json",
            data: {
                parentId: options.parentIds ? options.parentIds[0] : 0,
                LoadChildren: false
            }
        });

        return store;
    },
    loadData: function (_url, loadOptions) {
        var deferred = $.Deferred(),
            args = {};

        if (loadOptions) {
            var page = (loadOptions.take) ? (loadOptions.skip / loadOptions.take) + 1 : 1;

            if (typeof (loadOptions.sort) === "string") {
                args.sort = JSON.stringify([{ selector: loadOptions.sort, desc: false }]);
            } else {
                //args.sort = JSON.stringify(loadOptions.sort);
            }

            if (loadOptions.filter) {
                args.filter = JSON.stringify(filterParse(loadOptions.filter));
            }

            if (loadOptions.searchExpr) {
                var searchFilter = [];
                searchFilter.push({
                    filterField: loadOptions.searchExpr,
                    filterOperator: loadOptions.searchOperation,
                    filterValue: loadOptions.searchValue
                });

                args.filter = JSON.stringify(searchFilter);
            }

            args.pageSize = loadOptions.take || 50000;
            args.pageNumber = page;
        }

        $.ajax({
            url: _url,
            dataType: "json",
            data: args,
            cache: false,
            success: function (result) {
                var data = result.data;

                if (!result.isError) {
                    deferred.resolve(data.result, {
                        totalCount: data.totalCount,
                        //summary: result.summary,
                        //groupCount: result.groupCount
                    });
                } else
                    deferred.reject(result.message);
            },
            error: function () {
                deferred.reject("Data Loading Error");
            }
        });

        return deferred.promise();
    },
    lookupDataSource: function (options) {
        var that = this;

        return {
            store: new DevExpress.data.CustomStore({
                key: options.key,
                load: function (loadOptions) {
                    return that.loadData(options.url, loadOptions);
                },
                byKey: function (key) {
                    var d = new $.Deferred();

                    $.get(options.byKeyUrl + key)
                        .done(function (result) {
                            d.resolve(result.data);
                        });

                    return d.promise();
                }
            }),
            sort: options.sort,
            paginate: true,
            pageSize: options.pageSize || 10
        }
    },
    apiLoadData: function (url, method, data = null) {
        var json;
        if (data !== null) {
            data = data.values;
        }
        $.ajax(url, {
            type: method,
            data: JSON.stringify(data),
            cache: false,
            async: false
        }).done(function (result, response) {
            var message = result.message;
            if (!result.isError) {
                var data = result.data.result;
                if (data.length != 0) {
                    DevExpress.ui.notify({
                        message: "Load data successfully!",
                        width: 300,
                        position: { at: 'bottom right', my: "bottom right", offset: '-10 -10' }
                    }, "success", 1500);
                } else {
                    DevExpress.ui.notify({
                        message: "Data is empty!",
                        width: 300,
                        position: { at: 'bottom right', my: "bottom right", offset: '-10 -10' }
                    }, "warning", 1500);
                }
                return json = method === "GET" ? data : true;
            }
            DevExpress.ui.notify(message, "error", 1000);
            return false;
        }).fail(function (xhr, response) {
            DevExpress.ui.notify("Error status : " + xhr.status + xhr.responseText, "error", 2000);
            return json = false;
        });
        return json;
    },
    apiLoadDataNonResult: function (url, method, data = null) {
        var json;
        if (data !== null) {
            data = data.values;
        }
        $.ajax(url, {
            type: method,
            data: JSON.stringify(data),
            cache: false,
            async: false
        }).done(function (result, response) {
            var message = result.message;
            if (!result.isError) {
                var data = result.data;
                return json = method === "GET" ? data : true;
            }
            DevExpress.ui.notify(message, "error", 1000);
            return false;
        }).fail(function (xhr, response) {
            DevExpress.ui.notify("Error status : " + xhr.status + xhr.responseText, "error", 2000);
            return json = false;
        });
        return json;
    },
    apiLoadDataSelected: function (url, method, data = null) {
        var json;
        if (data !== null) {
            data = data.values;
        }
         
        $.ajax(url, {
            type: method,
            data: JSON.stringify(data),
            cache: false,
            async: false
        }).done(function (result, response) {
            var message = result.message;
            if (!result.isError) {
                var data = result;
                 
                if (data.length != 0) {
                    DevExpress.ui.notify({
                        message: "Load data successfully!",
                        width: 300,
                        position: { at: 'bottom right', my: "bottom right", offset: '-10 -10' }
                    }, "success", 1500);
                } else {
                    DevExpress.ui.notify({
                        message: "Data is empty!",
                        width: 300,
                        position: { at: 'bottom right', my: "bottom right", offset: '-10 -10' }
                    }, "warning", 1500);
                }
                return json = method === "GET" ? data : true;
            }
            DevExpress.ui.notify(message, "error", 1000);
            return false;
        }).fail(function (xhr, response) {
            DevExpress.ui.notify("Error status : " + xhr.status + xhr.responseText, "error", 2000);
            return json = false;
        });
        return json;
    },
    loadPanel: function () {
        dxLoadPanel({
            shadingColor: "rgba(0,0,0,0.4)",
            position: { of: "#body" },
            visible: false,
            showIndicator: true,
            showPane: true,
            shading: true,
            closeOnOutsideClick: false,
            onShown: function () {
                //setTimeout(function () {
                //    loadPanel.hide();
                //}, 60000);
            },
            onHidden: function () { }
        }).dxLoadPanel("instance");
    }

}

function filterParse(_filters) {
    var filter = [];
    if (typeof (_filters[0]) === "object") {
        $.each(_filters, function (i, dataFilter) {
            if (typeof (dataFilter) === "object") {
                if (typeof (dataFilter[0]) !== "object") {
                    filter.push({
                        filterField: dataFilter[0],
                        filterOperator: dataFilter[1],
                        filterValue: dataFilter[2]
                    });
                }
            }
        });
    } else {
        filter.push({
            filterField: _filters[0],
            filterOperator: _filters[1],
            filterValue: _filters[2]
        });
    }
    // 
    return filter;
}