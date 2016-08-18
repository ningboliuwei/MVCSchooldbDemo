var AlertType = { Error: "error", Question: "question", Info: "info", Warning: "warning" }; //警告框的 ICON 类型（仿枚举）

//添加 Tab 的函数，若已存在则选中已有的 Tab
function AddTab(tabsName, title, url) {
    if ($(tabsName).tabs("exists", title)) {
        $(tabsName).tabs("select", title);
    } else {
        $(tabsName)
            .tabs("add",
            {
                title: title,
                href: url,
                closable: true
            });
    }
}

//在 easyui datagrid 中显示数据
function BindGrid(gridName, toolbarName, url, columns, title, sortName, sortOrder, queryData) {
    $(gridName)
        .datagrid({
            url: url,
            columns: columns,
            title: title,
            fitColumns: true,
            iconCls: "icon-view",
            width: "100%",
            nowrap: false,
            autoRowHeight: true,
            striped: true,
            collapsible: true,
            pagination: true,
            pageSize: 20,
            pageList: [20, 40, 80],
            rownumbers: true,
            ctrlSelect: true,
            toolbar: toolbarName,
            queryParams: queryData,
            remoteSort: true,
            method: "post",
            sortName: sortName,
            sortOrder: sortOrder
        });
}

//显示警告框
function Alert(title, msg, icon) {
    $.messager.alert(title, msg, icon);
}

//显示确认框
function Confirm(title, msg, callback) {
    $.messager.confirm(title,
        msg,
        function(result) {
            if (result) {
                callback();
            }
        });
}

//显示编辑对话框
function ShowEditor(editorName, url, title) {
    $(editorName)
        .dialog({
            closed: true,
            title: title,
            href: url
            //            cache: false,
            //            doSize: false
        });

    $(editorName).dialog("open");
}

//关闭编辑对话框
function CloseEditor(editorName) {
    $(editorName).dialog("close");
}

//刷新 datagrid
function RefreshGrid(gridName) {
    $(gridName).datagrid("reload");
}

//向指定 url 提交
function FormSubmit(formName, gridName, editorName, url, errorMsg) {
    $(formName)
        .form("submit",
        {
            url: url,
            onSubmit: function() {
                return $(this).form("enableValidation").form("validate");
            },
            success: function() {
                $(function() {
                    RefreshGrid(gridName);
                    CloseEditor(editorName);
                });
            },
            error: function() {
                Alert("错误", errorMsg, AlertType.Error);
            }
        });
}

function Post(gridName, editorName, url, data, errorMsg) {
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        success: function() {
            $(function() {
                RefreshGrid(gridName);
                CloseEditor(editorName);
            });
        },
        error: function() {
            Alert("错误", errorMsg, AlertType.Error);
        },
        dataType: "html"
    });
}

function DisableControls(controlNames) {
    $(controlNames).disabled = true;
}

function HideControls(controlNames) {
    $(controlNames).hide();

}

function Delete(gridName, url) {
    var rows = $(gridName).datagrid("getSelections");

    if (rows.length === 0) {
        Alert("错误", "请至少选中一条记录。", window.AlertType.Error);
    } else {
        Confirm("确认",
            "确认删除选中的记录吗？",
            function() {
                const ids = [];

                for (let i = 0; i < rows.length; i++) {
                    ids.push(rows[i].Id);
                }

                $.ajax({
                    url: url,
                    type: "POST",
                    data: JSON.stringify(ids),
                    contentType: "application/json;charset=utf-8",
                    success: function() {
                        RefreshGrid(gridName);
                    }
                });
            });
    }
}

//打开编辑对话框
function Edit(gridName, editorName, url, editorTitle) {
    const rows = $(gridName).datagrid("getSelections");

    if (rows.length === 0) {
        Alert("错误", "请先选择一条记录。", AlertType.Error);
    } else if (rows.length > 1) {
        Alert("错误", "请只选择一条记录。", AlertType.Error);
        return;
    } else {
        const id = rows[0].Id;

        ShowEditor(editorName, url + "/" + id, editorTitle);
    }
}

//打开添加对话框
function Add(editorName, url, editorTitle) {
    ShowEditor(editorName,
        url,
        editorTitle);
}

//打开明细对话框
function Details(gridName, editorName, url, editorTitle) {
    const rows = $(gridName).datagrid("getSelections");

    if (rows.length === 0) {
        Alert("错误", "请先选择一条记录。", AlertType.Error);
    } else if (rows.length > 1) {
        Alert("错误", "请只选择一条记录。", AlertType.Error);
        return;
    } else {
        const id = rows[0].Id;

        ShowEditor(editorName, url + "/" + id, editorTitle);
    }
}

function ValidateForm(formName) {
    return $(formName).form("enableValidation").form("validate");
}


function InitUploadify(uploadifyName,
    url,
    ifAuto,
    ifMulti,
    queueLimit,
    fileTypeExts,
    fileTypeDesc,
    uploadSuccessCallback,
    uploadErrorCallback,
    uploadStartCallback,
    data) {
    $(uploadifyName)
        .uploadify({
            uploader: url,
            swf: "../Content/Uploadify/uploadify.swf",
            width: 60,
            height: 23,
            buttonText: "上传",
            buttonCursor: "hand",
            fileObjName: "fileData",
            fileTypeExts: fileTypeExts,
            fileTypeDesc: fileTypeDesc,
            auto: ifAuto,
            multi: ifMulti,
            queueSizeLimit: queueLimit,
            onUploadSuccess: uploadSuccessCallback,
            onUploadError: uploadErrorCallback,
            onUploadStart: uploadStartCallback,
            formData: data
        });
}

function EditorSubmit(formName, gridName, editorName, url, data, confirmMsg, errorMsg) {
    if (ValidateForm(formName)) {
        Confirm("确认",
            confirmMsg,
            function() {
                Post(gridName, editorName, url, data, errorMsg);
            });
    }
}

function InitTree(treeName, tabsName, data) {
    $(treeName)
        .tree({
            data: data,
            onClick: function(node) {
                AddTab(tabsName, node.text, node.attributes.url);
            }
        });
}

function BindCombobox(comboboxName, url, valueField, textField, initalText, callbackOnHidePanel) {
    $(comboboxName)
        .combobox({
            url: url,
            panelHeight: "auto",
            valueField: valueField,
            textField: textField,
            dataType: "json",
            onShowPanel: function() {
                $(comboboxName).combobox("reload");

            },
            onLoadSuccess: function() {
                const data = $(comboboxName).combobox("getData");
                if (initalText !== null && data[0][[textField]] !== initalText) {
                    data.unshift({ [valueField]: 0, [textField]: initalText });
                    $(comboboxName).combobox("loadData", data);
                }
            }
        });
}

function RefreshCombobox(comboboxName) {
    $(comboboxName).combobox("reload");
};