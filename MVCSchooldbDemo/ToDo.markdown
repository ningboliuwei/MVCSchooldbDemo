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
13. 添加新记录后，$ is not defined 错误
14. 解决 “清空搜索条件” 后将下拉菜单置为初始值的问题。—— 注意 data[0]["text"] 的使用 2016-07-30 √


