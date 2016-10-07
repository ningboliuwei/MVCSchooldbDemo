const DATA_DICT_ITEM_URL = "/DataDictItem/GetDataDictItemValues";

var AlertType = { Error: "error", Question: "question", Info: "info", Warning: "warning" }; //警告框的 ICON 类型（仿枚举）
var InputType = { Radio: "radio", Checkbox: "checkbox" }; //批量生成的控件类型



//EasyUI用DataGrid用日期格式化
var FormatHelper = {
    DateFormatter: function(value, row, index) {
        const date = new Date(value);
        const year = date.getFullYear().toString();
        var month = (date.getMonth() + 1);
        var day = date.getDate().toString();

        if (month < 10) {
            month = `0${month}`;
        }

        if (day < 10) {
            day = `0${day}`;
        }

        return year + "-" + month + "-" + day;
    },

    //EasyUI用DataGrid用日期格式化
    DateTimeFormatter: function(value, row, index) {
        const date = new Date(value);
        const year = date.getFullYear().toString();
        var month = (date.getMonth() + 1);
        var day = date.getDate().toString();
        var hour = date.getHours().toString();
        var minutes = date.getMinutes().toString();
        var seconds = date.getSeconds().toString();

        if (month < 10) {
            month = `0${month}`;
        }

        if (day < 10) {
            day = `0${day}`;
        }

        if (hour < 10) {
            hour = `0${hour}`;
        }

        if (minutes < 10) {
            minutes = `0${minutes}`;
        }

        if (seconds < 10) {
            seconds = `0${seconds}`;
        }

        return year + "-" + month + "-" + day + " " + hour + ":" + minutes + ":" + seconds;
    },
    DateBoxFormatter: function(value, rec, index) {
        if (value == undefined) {
            return "";
        }
        /*json格式时间转js时间格式*/
        value = value.substr(1, value.length - 2);
        var obj = eval('(' + "{Date: new " + value + "}" + ')');
        var dateValue = obj["Date"];
        if (dateValue.getFullYear() < 1900) {
            return "";
        }
        return dateValue.Format("yyyy-mm-dd");
    }
};

// 对Date的扩展，将 Date 转化为指定格式的String
// 月(M)、日(d)、小时(H)、分(M)、秒(S)、季度(q) 可以用 1-2 个占位符， 
// 年(y)可以用 1-4 个占位符，毫秒(s)只能用 1 个占位符(是 1-3 位的数字) 
// 例子： 
// (new Date()).Format("yyyy-mm-dd HH:MM:SS.s") ==> 2015-07-02 08:09:04.423 
// (new Date()).Format("yyyy-m-d H:M:S.s")      ==> 2015-7-2 8:9:4.18 
Date.prototype.Format = function(fmt) { //author: meizz 
    var o = {
        "m+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "H+": this.getHours(), //小时 
        "M+": this.getMinutes(), //分 
        "S+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "s": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}
//调用： 
//var time1 = new Date().Format("yyyy-MM-dd");
//var time2 = new Date().Format("yyyy-MM-dd HH:mm:ss")


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
            href: url,
            modal: true
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
        console.log(id);
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
                if (node.attributes.url !== "#") {
                    AddTab(tabsName, node.text, node.attributes.url);
                }
            }
        });
}

function BindCombobox(comboboxName, url, params, valueField, textField, initialText) {
    $(comboboxName)
        .combobox({
            url: url,
            queryParams: params,
            panelHeight: "auto",
            valueField: valueField,
            textField: textField,
            dataType: "json",
            editable: false,
            onShowPanel: function() {
                $(comboboxName).combobox("reload");
            },
            onLoadSuccess: function() {
                const previousValue = $(comboboxName).combobox("getValue");
                const data = $(comboboxName).combobox("getData");
                if (initialText !== null && data[0][[textField]] !== initialText) {
                    data.unshift({ [valueField]: 0, [textField]: initialText });
                    $(comboboxName).combobox("loadData", data);
                }

                if ($(comboboxName).combobox("getText") === "") {
                    $(comboboxName).combobox("select", data[0][[valueField]]);
                } else {
                    $(comboboxName).combobox("select", previousValue);
                }
            }
        });
}

//function GetDataDictItemValues(itemName) {
//    var data;
//    $.ajax({
//        type: "POST",
//        url: DATA_DICT_ITEM_URL,
//        async: false,
//        data: { itemName: itemName },
//        success: function(result) {
//            data = result;
//        },
//        error: function() {
//            Alert("错误", errorMsg, AlertType.Error);
//        },
//        dataType: "json"
//    });
//    return data;
//}

function BindDataDictItemToCombobox(comboboxName, itemName, initialText) {
    BindCombobox(comboboxName, DATA_DICT_ITEM_URL, { itemName: itemName }, "id", "value", initialText);
}

function RefreshCombobox(comboboxName) {
    $(comboboxName).combobox("reload");
};

function GenerateInputListByDataDictItem(containerName, itemName, inputType) {
    $.ajax({
        type: "POST",
        url: DATA_DICT_ITEM_URL,
        data: { itemName: itemName },
        async:false,
        success: function(array) {
            $.each(array,
                function(i) {
                    $(containerName)
                        .append(`<input class='magic-radio' id='${itemName}_${array[i]["id"]}' type='${inputType}' name='${itemName
                        }' /><label for='${itemName}_${array[i]["id"]}'>${array[i]["value"]}</label>`);
                });
            //value='${array[i]["value"]}'
        },
        error: function() {
            Alert("错误", errorMsg, AlertType.Error);
        },
        dataType: "json"
    });


}