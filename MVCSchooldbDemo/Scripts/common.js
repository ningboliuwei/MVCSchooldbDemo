function BindGrid(url, columns, title, sortName, queryData) {
    $("#dg")
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
            SortName: sortName, //根据某个字段给easyUI排序
            sortOrder: "asc",
            ctrlSelect: true,
            toolbar: "#tb",
            queryParams: queryData,
            remoteSort: false
        });

}

function DeleteRecord(url, confirmMessage, idName) {
    var rows = $("#dg").datagrid("getSelections");
    var ids = [];

    if (rows) {
        $.messager.confirm("问题",
            confirmMessage,
            function(result) {
                if (result) {
                    for (var i = 0; i < rows.length; i++) {
                        ids.push(rows[i][idName]);
                    }

                    $.ajax({
                        url: url,
                        type: "POST",
                        data: JSON.stringify(ids),
                        contentType: "application/json; charset=utf-8",
                        success: function() {
                            $("#dg").datagrid("reload");
                        }
                    });
                }
            });
    }
}

function ShowAddOrEditDialog(url, title) {
    $("#addOrEditDialog")
        .dialog({
            closed: true,
            title: title,
            href: url,
            width: 400
        });

    $("#addOrEditDialog").dialog("open");
}