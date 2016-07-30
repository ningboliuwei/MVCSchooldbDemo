1. 查询 √
2. 分页（页码不对）——需要在返回的 JSON 中加入 total，还有 rows 序列化后的结果。解决办法：使用匿名类 √ 
3. 明细在不同记录间切换——Pagination 不适合使用，换成 “上一条”，“下一条” 按钮
4. 弹出对话框居中 √
5. 分页导航条总在最底下——将 table 标签中设置 width = 100% 即可 √
6. 将排序与分页的功能抽取出来放到公共库中——已放入 DBHelper 类中，作为 SortingAndPaging() 方法。√
7. 动态生成根据关键字过滤记录——DBHelper.FilterByKeywords() 2016-07-30 √ 通过 system.linq.dynamic 中字符串的 Contains 实现
8. 为对话框加上表格线 ×（暂不实现）
9. 在 common.js 中添加用于显示消息框与确认框的函数 √
10. 添加，修改都需要确认框 √
11. 将添加，修改，删除操作都放到 common.js 中——目前完成一半，Student/Index 中的完成，需要迁移到 common.js 中
12. Add，Edit，Details 对话框的按钮也抽取为统一布局——一半完成，点击按钮的事件需要重新弄
13. 添加新记录后，$ is not defined 错误——通过将 submit 方法改为 Post 方法（在 common.js中）解决。主要要将 .ajax 的 datatype 设为 "html"（使 success: 后的回调可以正确执行，原因是通过 chrome f12 得到的 response 是 html 类型的）。2016-07-30 22:02√ （原代码： Confirm('确认', '确认修改记录吗？', function () { Submit('@Url.Action("Edit", "Student")', '修改记录遇到问题！') });）
14. 解决 “清空搜索条件” 后将下拉菜单置为初始值的问题。—— 注意 data[0]["text"] 的使用 2016-07-30 √
15. 将 edit 与 create 的发送数据方式进行修改 —— 已改为 POST 2016-07-30 √
16. 修改为 POST 后，对 form 中数据没有验证 —— 已搞定（ValidateForm()） 2016-07-30 √
17. 确认框乱码 —— 已搞定。解决方案：将 common.js 存为 UTF-8 编码（文件 → 高级保存），


