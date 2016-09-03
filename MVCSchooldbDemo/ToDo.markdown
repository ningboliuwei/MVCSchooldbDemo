1. 查询 √
2. 分页（页码不对）——需要在返回的 JSON 中加入 total，还有 rows 序列化后的结果。解决办法：使用匿名类 √
3. 明细在不同记录间切换——Pagination 不适合使用，换成 “上一条”，“下一条” 按钮
4. 弹出对话框居中 √
5. 分页导航条总在最底下——将 table 标签中设置 width = 100% 即可 √
6. 将排序与分页的功能抽取出来放到公共库中——已放入 DBHelper 类中，作为 SortingAndPaging() 方法。√
7. 动态生成根据关键字过滤记录——DBHelper.FilterByKeywords()  通过 system.linq.dynamic 中字符串的 Contains 实现 2016-07-30 √
8. 为对话框加上表格线 —— 代码示例：（.editorLayout {padding: 20px;}.editorTable {	width: 90%;	border-collapse: collapse;	margin: 10px;}	.editorTable td, th {	border: 1px dotted #ccc;	padding: 5px;}
）
 2016-07-31 √
9. 在 common.js 中添加用于显示消息框与确认框的函数 √
10. 添加，修改都需要确认框 √
11. 将添加，修改，删除操作都放到 common.js 中——目前完成一半，Student/Index 中的完成，需要迁移到 common.js 中 2016-07-30 √
12. Add，Edit，Details 对话框的按钮也抽取为统一布局——一半完成，点击按钮的事件需要重新弄
13. 添加新记录后，$ is not defined 错误——通过将 submit 方法改为 Post 方法（在 common.js中）解决。主要要将 .ajax 的 datatype 设为 "html"（使 success: 后的回调可以正确执行，原因是通过 chrome f12 得到的 response 是 html 类型的）。（原代码： Confirm('确认', '确认修改记录吗？', function () { Submit('@Url.Action("Edit", "Student")', '修改记录遇到问题！') });）2016-07-30 22:02 √
14. 解决 “清空搜索条件” 后将下拉菜单置为初始值的问题。—— 注意 data[0]["text"] 的使用 2016-07-30 √
15. 将 edit 与 create 的发送数据方式进行修改 —— 已改为 POST 2016-07-30 √
16. 修改为 POST 后，对 form 中数据没有验证 —— 已搞定（ValidateForm()） 2016-07-30 √
17. 确认框乱码 —— 已搞定。解决方案：将 common.js 存为 UTF-8 编码（文件 → 高级保存） 2016-07-30 √
18. 上传图片
19. editor 中加 tab
20. 为搜索部分增加 table 布局 —— 搞定 2016-07-31 √
21. 增加了 site.css 中的很多样式 —— 2016-07-31 √
22. 为 editorDialog 增加了标题 —— 通过 ViewBag 2016-07-31 √
23. 在 DBHelper 中增加了 FindByKeyword() 方法，用于用一种统一的方式获得 model 2016-07-31 √
24. 解决了 datagrid height 100% 下不显示分页条的问题 原因：不要给 toolbar 的 css 中的 table 设置 100% 宽度 2016-07-31 √
25. 解决了问题 24 后，新增记录又不行了。—— 搞定 2016-07-31 √
26. 使用 uploadify 上传文件—— 部分搞定，可以上传。 2016-08-01 √ ==== 需要增加数据库中文件信息添加功能，需要上传完毕后立即显示图片。需要更换上传按钮的图——做到一半_ ==== 基本重构。
27. 需要增加附件信息记录功能（代码与数据库）——以照片为例 同28 2016-08-04 √
28. 增加学生记录时要把上传的附件信息也给记录下来 ==== Student 记录仅记录附件的 GUID，上传时会在 UploadFile 表中增加一条新的记录，通过 GUID 获取具体信息  2016-08-04 √
29. 将 LayoutEditorCreate 等 Layout 中的 Submit 中的数据也给抽取出来 ====== 1. 进一步分离了 Layout 与 Editor 与 Create / Edit/ Details 页面，将对于表单数据的获取与显示放在了 Editor 中 2. 特别注意：easyui 自带的 jquery 版本很老，利用 nuget 下载最新版本 3. 注意 Partal 也可以传入 Model，需要判断是否为空 √ 2016-08-06
30. 将学号设置为不可编辑
31. Details 表中通过点击 “上一条” 与 “下一条” 进行切换。注意：切换的全集应该是根据关键词过滤出的结果。===== 需要把 Sorting 和 Paging 再分离开来。 ========== 搞定，利用 ViewBag 以及 static 变量进行传递 ======= 进一步实现无刷新  2016-08-11 √
32. 重构了 GetList() 及 FilterByKeywords，将所有查询参数打包为一个 string 参数，更加灵活。 2016-08-07 √
33. 利用 GetList() 方法返回的查询结果 JSON 字符串，获取查询结果中 Id 的集合。通过泛型委托，对代码进行重构。代码如下：
        public static List<T2>
    GetListFromResultString<T1, T2>
        (Func<T1, T2>
            func, string resultString)
            {
            return
            JsonConvert.DeserializeObject<List<T1>>(JObject.Parse(resultString)["rows"].ToString())
                    .Select(func)
                    .ToList();
        }

    StudentController 中：
    ViewBag.Ids = DBHelper.GetListFromResultString<StudentInfo, long>(s => s.Id, result);
    ============== 2016-08-07 √
34. 在 CSS 中将字体设置为微软雅黑 2016-08-07 √
35. 解决 Editor 打开刷新的问题========= 1. 为 dialog 设置初始 width 和 height 2. 将 dialog 的 cache 属性设置为 false 3. 使用 $("#editor").dialog("open").dialog("refresh");
36. 点击创建按钮后，会出现两个 Confirm  ======  common.js 中的 ShowEditor() 中的 dialog('open') 后的 dialog('refresh') 造成，去掉即可。
37. Editor 中加 Tab  ============ 搞定  2016-08-11 √
38. Combobox 绑定数据。在 Combobox 中增加一个默认项（如 全部） =======  详见 common.js 中的 BindComboBox ，尤其是 [textfield] 与 [valueField]，中括号作用是将其作为变量而不是常量（键值）。另外注意 loadsuccess 的作用
39. 点击 Combobox 时获取最新数据  ========== 在 select 标签外加上 div 标签，并在 onclick 事件中重新绑定 combobox 的数据 ====== （并加上额外项（全部类型等））已解决
    代码
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

40. 若碰到 datagrid 的 resizing 问题（很多 exception），很大概率是因为在 Index.cshtml 中的 添加/删除/修改 几个按钮的 onclick 事件中调用 Add() 等方法把 editor 名当做 datagrid 在用了，注意一下。（也就是注意这几个函数的参数对不对）
41. 添加与编辑用户时，保存的密码为 MD5 加密 ======== 2016-08-23 √
42. 添加系统登录界面 =========== 未授权的访问都跳转到 ~/Home/Login，方法：1. 在 Web.config 的 <system.web> 节中增加：
    <authentication mode="Forms">
      <forms loginUrl="~/Home/Login" timeout="2880"></forms>
    </authentication>

    登录成功后使用代码：FormsAuthentication.SetAuthCookie(account, false);
    ========== 2016-08-25 √

43. 使用了 font awesome =========== 2016-08-24 √
44. 增加了导航栏上几个按钮（仿力软） ================ 半成品
45. 增加了注销功能  =========== 要是有延时效果就更好了 2016-08-25 √
46. 增加 PatientInfo 相关一系列 ================= 2016-09-01 √
47. 增加 DataDictItemInfo 一系列 ================== 2016-09-01 √
48. 通过自定义 T4 模板生成相应代码  ============== 2016-09-02 √
49. 用户管理的『编辑』失效  ============== 由不同的 Editor 中同名的 btnOK 与 btnCancel 导致。需要将不同 editor 中的两个按钮的 id 设为不一样的  =========== 基本解决，需要进一步测试（并考虑修改已有代码，及重构优化 ======= 以修改 Views 及 Controller 模板）
50. Editor 中的确定按钮等保持最下方 ========= 通过 postition:absolute 以及 bottom:0 实现。 ============== 2016-09-03 √
51. datagrid 中日期格式不对 =============== 通过 common.js 中的 FormatHelper 实现 ======= 2016-09-03 √
52. combobox 设置为只读 ============ editable : false in BindComboBox in common.js =========== 2016-09-03 √
53. 点击 combobox 后，会自动选中第一项。




