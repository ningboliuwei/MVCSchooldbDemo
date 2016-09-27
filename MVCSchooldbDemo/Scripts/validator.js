//扩展 validatebox 验证规则
$.extend($.fn.validatebox.defaults.rules,
{
    //验证汉字
    chinese: {
        validator: function(value) {
            return /^[\u0391-\uFFE5]+$/.test(value);
        },
        message: "只能输入汉字"
    },
    //移动手机号码验证
    mobilephone: { //value值为文本框中的值
        validator: function(value) {
            return /^0?(13[0-9]|15[012356789]|17[0678]|18[0-9]|14[57])[0-9]{8}$/.test(value);
        },
        message: "手机号码格式不正确."
    },
    //国内邮编验证
    zipcode: {
        validator: function(value) {
            return /^[1-9]\d{5}$/.test(value);
        },
        message: "邮编格式不正确"
    },
    idcard: { // 验证身份证
        validator: function(value) {
            return /^(^\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$/.test(value);
        },
        message: "身份证号码格式不正确"
    },
    minLength: {
        validator: function(value, param) {
            return value.length >= param[0];
        },
        message: "请输入至少（2）个字符."
    },
    length: {
        validator: function(value, param) {
            const len = $.trim(value).length;
            return len >= param[0] && len <= param[1];
        },
        message: "输入内容长度必须介于{0}和{1}之间."
    },
    phone: { // 验证电话号码
        validator: function(value) {
            return /^((\d2,3)|(\d{3}\-))?(0\d2,3|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$/i.test(value);
        },
        message: "格式不正确,请使用下面格式：020-88888888"
    },
    numeric: { // 验证整数或小数
        validator: function(value) {
            return /^\d+(\.\d+)?$/i.test(value);
        },
        message: "请输入数字，并确保格式正确"
    },
    integer: { // 验证整数 可正负数
        validator: function(value) {
            return /^([+]?[0-9])|([-]?[0-9])+\d*$/i.test(value);
        },
        message: "请输入整数"
    },
    age: { // 验证年龄
        validator: function(value) {
            return /^(?:[1-9][0-9]?|1[01][0-9]|120)$/i.test(value);
        },
        message: "年龄必须是 0 到 120 之间的整数"
    },
    english: { // 验证英语
        validator: function(value) {
            return /^[A-Za-z]+$/i.test(value);
        },
        message: "请输入英文"
    },
    username: { // 验证用户名
        validator: function(value) {
            return /^[a-zA-Z][a-zA-Z0-9_]{5,15}$/i.test(value);
        },
        message: "用户名格式不正确（字母开头，长度在 6 ~ 16 之间，只能使用字母、数字或下划线）"
    },
    name: { // 验证姓名，可以是中文或英文
        validator: function(value) {
            return /^[\Α-\￥]+$/i.test(value) | /^\w+[\w\s]+\w+$/i.test(value);
        },
        message: "只能接受字母与汉字"
    },
    date: { // 验证日期
        validator: function(value) {
            //格式yyyy-MM-dd或yyyy-M-d
            return /^(?:(?!0000)[0-9]{4}([-]?)(?:(?:0?[1-9]|1[0-2])\1(?:0?[1-9]|1[0-9]|2[0-8])|(?:0?[13-9]|1[0-2])\1(?:29|30)|(?:0?[13578]|1[02])\1(?:31))|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)([-]?)0?2\2(?:29))$/i.test(value);
        },
        message: "日期格式不正确"
    }
});