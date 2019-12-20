// 封装根据id获取dom元素方法
function getEm(id) {
    return document.getElementById(id);
}

/*调用示例
myAjax({
    url: '/Handler/HomeHandler.ashx/Add',
    method: 'POST',
    data: {
        name: name,
        pwd: pwd
    },
    success: function (data) {
        if (data == 'true') {
            alert('注册成功');
        } else {
            alert('未注册成功');
        }
    },
    error: function (err) {
        alert('请求发生异常');
        console.error(err);
    }
});
*/

/*
参数说明：
option = {
    url:'',//请求的地址
    method:'POST',//请求类型 POST或GET
    data:{},//传输的数据
    success:function(data){},//成功响应后的回调函数
    error:function(err){},//失败后的回调函数
}
*/

function myAjax(option) {
    //1、创建XMLHttpRequest对象
    var req = new XMLHttpRequest();

    //默认为GET方式
    option.method = option.method || "GET";

    //2、发送AJAX请求
    if (option.method == "GET") {
        req.open('GET', option.url, true);
        req.send(null);
    } else {
        req.open('POST', option.url, true);
        req.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        req.send(formatParams(option.data));
    }

    //3、接收AJAX响应
    /*
    其中，readyState可以取如下值：
    0 (未初始化，XMLHttpRequest对象已经创建，但尚未初始化，还没有调用open方法)
    1 (已经调用send方法，正在发送HTTP请求)
    2 (send方法调用结束，已经接收到全部HTTP响应消息)
    3 (正在解析响应内容，但状态和响应头还不可用)
    4 (完成)
    */
    req.onreadystatechange = function () {
        if (req.readyState == XMLHttpRequest.DONE && req.status == 200) {
            option.success(req.response);
        } else if (req.readyState == XMLHttpRequest.DONE) {
            option.error(req.response);
        }
    }
}

//格式化参数
function formatParams(data) {
    var arr = [];
    for (var name in data) {
        // encodeURIComponent方法在编码单个URIComponent（指请求参数）应当是最常用的，它可以将参数中的中文、特殊字符进行转义，而不会影响整个URL。
        arr.push(encodeURIComponent(name) + "=" + encodeURIComponent(data[name]));
    }
    arr.push(("v=" + Math.random()).replace(".", ""));
    return arr.join("&");
}

// 根据key值获取url中的参数value
function getQueryString(name) {
    let reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    let r = window.location.search.substr(1).match(reg);
    if (r != null) {
        return unescape(r[2]);
    };
    return null;
}

// 根据响应返回内容得到js对象
function getObj(resp) {
    if (typeof resp === 'string') {
        return JSON.parse(resp);
    }
    return resp;
}