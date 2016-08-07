
var AlertType = { Error: "error", Question: "question", Info: "info", Warning: "warning" }; //警告框的 ICON 类型（仿枚举）

//添加 Tab 的函数，若已存在则选中已有的 Tab
function AddTab(title, url) {
    if ($("#tabs").tabs("exists", title)) {
        $("#tabs").tabs("select", title);
    } else {
        $("#tabs")
            .tabs("add",
            {
                title: title,
                href: url,
                closable: true
            });
    }
}

//在 easyui datagrid 中显示数据
function BindGrid(url, columns, title, sortName, sortOrder, queryData) {
    $("#grid")
        .datagrid({
            url: url,
            columns: columns,
            title: title,
            fitColumns: true,
            iconCls: "icon-view",
            width: "100%",
            nowrap: true,
            autoRowHeight: false,
            striped: true,
            collapsible: true,
            pagination: true,
            pageSize: 20,
            pageList: [20, 40, 80],
            rownumbers: true,
            ctrlSelect: true,
            toolbar: "#tb",
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
function ShowEditor(url, title) {
    $("#editor")
        .dialog({
            closed: true,
            title: title,
            href: url,
            fit: false,
            onOpen: function() { $("#editor").dialog("hcenter"); }
        });


    $("#editor").dialog("open");
}

//关闭编辑对话框
function CloseEditor() {
    $("#editor").dialog("close");
}

//刷新 datagrid
function RefreshGrid() {
    $("#grid").datagrid("reload");
}

//向指定 url 提交
function FormSubmit(url, errorMsg) {
    $("#ff")
        .form("submit",
        {
            url: url,
            onSubmit: function() {
                return $(this).form("enableValidation").form("validate");
            },
            success: function() {
                $(function() {
                    RefreshGrid();
                    CloseEditor();
                });
            },
            error: function() {
                Alert("错误", errorMsg, AlertType.Error);
            }
        });
}

function Post(url, data, errorMsg) {
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        success: function() {
            $(function() {
                RefreshGrid();
                CloseEditor();
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

function Delete(url) {
    var rows = $("#grid").datagrid("getSelections");

    if (rows.length === 0) {
        Alert("错误", "请至少选中一条记录。", AlertType.Error);
    } else {
        Confirm("确认",
            "确认删除选中的记录吗？",
            function() {
                var ids = [];

                for (var i = 0; i < rows.length; i++) {
                    ids.push(rows[i].Id);
                }

                $.ajax({
                    url: url,
                    type: "POST",
                    data: JSON.stringify(ids),
                    contentType: "application/json;charset=utf-8",
                    success: function() {
                        RefreshGrid();
                    }
                });
            });
    }
}

//打开编辑对话框
function Edit(url, editorTitle) {
    var rows = $("#grid").datagrid("getSelections");

    if (rows.length === 0) {
        Alert("错误", "请先选择一条记录。", AlertType.Error);
    } else if (rows.length > 1) {
        Alert("错误", "请只选择一条记录。", AlertType.Error);
        return;
    } else {
        var id = rows[0].Id;

        ShowEditor(url + "/" + id, editorTitle);
    }
}

//打开添加对话框
function Add(url, editorTitle) {
    ShowEditor(url, editorTitle);
}

//打开明细对话框
function Details(url, editorTitle) {
    var rows = $("#grid").datagrid("getSelections");

    if (rows.length === 0) {
        Alert("错误", "请先选择一条记录。", AlertType.Error);
    } else if (rows.length > 1) {
        Alert("错误", "请只选择一条记录。", AlertType.Error);
        return;
    } else {
        var id = rows[0].Id;

        ShowEditor(url + "/" + id, editorTitle);
    }
}

function ValidateForm() {
    return $("#ff").form("enableValidation").form("validate");
}


function InitUploadify(url,
    ifAuto,
    ifMulti,
    queueLimit,
    fileTypeExts,
    fileTypeDesc,
    uploadSuccessCallback,
    uploadErrorCallback,
    uploadStartCallback,
    data) {
    $("#uploadify")
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

function EditorSubmit(url, data, confirmMsg, errorMsg) {
    if (ValidateForm()) {
        Confirm("确认",
            confirmMsg,
            function() { Post(url, data, errorMsg) });
    }
}