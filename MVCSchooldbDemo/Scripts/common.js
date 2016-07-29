
var AlertType = { Error: 'error', Question: 'question', Info: 'info', Warning: 'warning' }

//添加 Tab 的函数，若已存在则选中已有的 Tab
function AddTab(title, url) {
    if ($('#tabs').tabs('exists', title)) {
        $('#tabs').tabs('select', title);
    } else {
        $('#tabs')
            .tabs('add',
            {
                title: title,
                href: url,
                closable: true
            });
    }
}

function BindGrid(url, columns, title, sortName, sortOrder, queryData) {
    $('#grid')
        .datagrid({
            url: url,
            columns: columns,
            title: title,
            fitColumns: true,
            iconCls: 'icon-view',
            width: '100%',
            nowrap: true,
            autoRowHeight: false,
            striped: true,
            collapsible: true,
            pagination: true,
            pageSize: 5,
            pageList: [5, 20, 40],
            rownumbers: true,
            ctrlSelect: true,
            toolbar: '#tb',
            queryParams: queryData,
            remoteSort: true,
            method: 'post',
            sortName: sortName,
            sortOrder: sortOrder

        });
}

function DeleteRecord(url, idName) {
   
}

function Alert(title, msg, icon) {
    $.messager.alert(title, msg, icon);
}

function Confirm(title, msg, callback) {
    $.messager.confirm(title,
        msg,
        function (result) {
            if (result) {
                callback();
            }
        });
}

function ShowEditor(url, title) {
    $('#editor')
        .dialog({
            closed: true,
            title: title,
            href: url
        });

    $('#editor').dialog('open');
}

function CloseEditor() {
    $('#editor').dialog('close');
}

function RefreshGrid() {
    $('#grid').datagrid('reload');
}

function Submit(url, message) {
    $('#ff')
        .form('submit',
        {
            url: url,
            onSubmit: function () {
                return $(this).form('enableValidation').form('validate');
            },
            success: function () {
                CloseEditor();
                RefreshGrid();
            },
            error: function () {
                Alert('错误', message, AlertType.Error);
            }
        });
}

function ReadonlyControls(controlNames) {
    $(controlNames).attr('readonly', true);
}

