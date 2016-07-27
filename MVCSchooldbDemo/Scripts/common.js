//��� Tab �ĺ��������Ѵ�����ѡ�����е� Tab
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


function BindGrid(url, columns, title, sortName, queryData) {
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
            pageSize: 40,
            pageList: [10, 20, 40],
            rownumbers: true,
            SortName: sortName, //����ĳ���ֶθ�easyUI����
            sortOrder: "asc",
            ctrlSelect: true,
            toolbar: "#tb",
            queryParams: queryData,
            remoteSort: false
        });

}

function DeleteRecord(url, confirmMessage, idName) {
    var rows = $("#grid").datagrid("getSelections");
    var ids = [];

    if (rows) {
        $.messager.confirm("����",
            confirmMessage,
            function (result) {
                if (result) {
                    for (var i = 0; i < rows.length; i++) {
                        ids.push(rows[i][idName]);
                    }

                    $.ajax({
                        url: url,
                        type: "POST",
                        data: JSON.stringify(ids),
                        contentType: "application/json; charset=utf-8",
                        success: function () {
                            $("#grid").datagrid("reload");
                        }
                    });
                }
            });
    }
}

function ShowEditor(url, title) {
    $("#editor")
        .dialog({
            closed: true,
            title: title,
            href: url,
            width: 400
        });

    $("#editor").dialog("open");
}

function CloseEditor() {
    $("#editor").dialog("close");
}

function RefreshGrid() {
    $("#grid").datagrid("reload");
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
                $.messager.alert('����', message, 'error');
            }
        });
}

function ReadonlyControls(controlNames) {
    $(controlNames).attr('readonly', true);
}