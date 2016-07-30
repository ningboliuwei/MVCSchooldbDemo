
var AlertType = { Error: 'error', Question: 'question', Info: 'info', Warning: 'warning' }//������ ICON ���ͣ���ö�٣�

//��� Tab �ĺ��������Ѵ�����ѡ�����е� Tab
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

//�� easyui datagrid ����ʾ����
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
            pageSize: 20,
            pageList: [20, 40, 80],
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

//��ʾ�����
function Alert(title, msg, icon) {
    $.messager.alert(title, msg, icon);
}

//��ʾȷ�Ͽ�
function Confirm(title, msg, callback) {
    $.messager.confirm(title,
        msg,
        function (result) {
            if (result) {
                callback();
            }
        });
}

//��ʾ�༭�Ի���
function ShowEditor(url, title) {
    $('#editor')
        .dialog({
            closed: true,
            title: title,
            href: url
        });

    $('#editor').dialog('open');
}

//�رձ༭�Ի���
function CloseEditor() {
    $('#editor').dialog('close');
}

//ˢ�� datagrid
function RefreshGrid() {
    $('#grid').datagrid('reload');
}

//��ָ�� url �ύ
function Submit(url, errorMsg) {
    $('#ff')
        .form('submit',
        {
            url: url,
            onSubmit: function () {
                return $(this).form('enableValidation').form('validate');
            },
            success: function () {
                $(function () {
                    RefreshGrid();
                    CloseEditor();
                });
            },
            error: function () {
                Alert('����', errorMsg, AlertType.Error);
            }
        });
}

function Post(url, data, errorMsg) {
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        success: function () {
            $(function () {
                RefreshGrid();
                CloseEditor();
            });
        },
        error: function () {
            Alert('����', errorMsg, AlertType.Error);
        },
        dataType: "html"
    });
}

function ReadonlyControls(controlNames) {
    $(controlNames).attr('readonly', true);
}

function Delete(url, idName) {
    var rows = $('#grid').datagrid('getSelections');

    if (rows.length === 0) {
        Alert('����', '������ѡ��һ����¼��', AlertType.Error);
    } else {
        Confirm('ȷ��', 'ȷ��ɾ��ѡ�еļ�¼��',
            function () {
                var ids = [];

                for (var i = 0; i < rows.length; i++) {
                    ids.push(rows[i][idName]);
                }

                $.ajax({
                    url: url,
                    type: 'POST',
                    data: JSON.stringify(ids),
                    contentType: 'application/json; charset=utf-8',
                    success: function () {
                        RefreshGrid();
                    }
                });
            });
    }
}

//�򿪱༭�Ի���
function Edit(url, editorTitle) {
    var rows = $('#grid').datagrid('getSelections');

    if (rows.length === 0) {
        Alert('����', '����ѡ��һ����¼��', AlertType.Error);
    } else if (rows.length > 1) {
        Alert('����', '��ֻѡ��һ����¼��', AlertType.Error);
        return;
    } else {
        var id = rows[0].Id;

        ShowEditor(url + '/' + id, editorTitle);
    }
}

//����ӶԻ���
function Add(url, editorTitle) {
    ShowEditor(url, editorTitle);
}

//����ϸ�Ի���
function Details(url, editorTitle) {
    var rows = $('#grid').datagrid('getSelections');

    if (rows.length === 0) {
        Alert('����', '����ѡ��һ����¼��', AlertType.Error);
    } else if (rows.length > 1) {
        Alert('����', '��ֻѡ��һ����¼��', AlertType.Error);
        return;
    } else {
        var id = rows[0].Id;

        ShowEditor(url + '/' + id, editorTitle);
    }
}


