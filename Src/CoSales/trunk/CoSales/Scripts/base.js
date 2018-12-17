/*
用于从ajax获取数据的解析
有可能服务端返回的是字符串，也有可能是对象
add by wangyuanwei 2018-12-6
*/
function getObjRes(data) {
    if (typeof data == 'string') {
        return JSON.parse(data);
    }
    return data;
}

/*  
方便根据id获取dom对象的调用
直接使用 getEmp("xxxx")
*/
function getEm(id) {
    return document.getElementById(id);
}

function getEmValue(id) {
    return document.getElementById(id).value;
}

/*           以下为myAjax的封装                       */
//调用示例
//myAjax({
//    url: '../Handler/HomeHandler.ashx/Add',
//    type: 'POST',
//    data: {
//        name: name,
//        pwd: pwd
//    },
//    success: function (data) {
//        if (data == 'true') {
//            alert('注册成功');
//        } else {
//            alert('未注册成功');
//        }
//    },
//    error: function (err) {
//        alert('请求发生异常');
//        console.error(err);
//    }
//});

/*
参数说明：
option = {
    url:'',//请求的地址
    type:'POST',//请求类型 POST或GET
    data:{},//传输的数据
    success:function(data){},//成功响应后的回调函数
    error:function(err){},//失败后的回调函数
}
*/
function myAjax(option) {
    //1、创建XMLHttpRequest对象 XHR
    var req = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHTTP");

    //默认为GET方式
    option.type = option.type || "GET";

    //2、发送AJAX请求
    if (option.type == "GET") {
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
        if (req.readyState == 4 && req.status == 200) {
            option.success(req.response);
        } else if (req.readyState == 4) {
            option.error(req.response);
        }
    }
}

//格式化参数
function formatParams(data) {
    var arr = [];
    for (var name in data) {
        // encodeURIComponent方法在编码单个URIComponent（指请求参数）应当是最常用的，它可以讲参数中的中文、特殊字符进行转义，而不会影响整个URL。
        arr.push(encodeURIComponent(name) + "=" + encodeURIComponent(data[name]));
    }
    arr.push(("v=" + Math.random()).replace(".", ""));
    return arr.join("&");
}

// 从url中获取参数值
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
