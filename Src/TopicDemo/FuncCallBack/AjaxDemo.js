/*
http
200 OK
404 not found
500 服务器端错误


1、request header
2、request 正文
3、response header
4、response 正文
*/
/*
1）创建XMLHttpRequest对象
var req = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHTTP");

2）发送AJAX请求
GET请求：
req.open('GET', './Handlers/AjaxHandler.ashx?username=zhangsan&sex=boy', true);
req.send(null);
POST请求：
req.open('POST', './Handlers/AjaxHandler.ashx', true);
req.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
req.send('username=zhangsan&sex=boy');

3）接收AJAX响应
req.onreadystatechange = function () {
    if (req.readyState == 4 && req.status == 200) {
        alert('响应消息的正文内容：' + req.responseText);
    }
};
其中，readyState可以取如下值：
0 (未初始化，XMLHttpRequest对象已经创建，但尚未初始化，还没有调用open方法)
1 (已经调用send方法，正在发送HTTP请求) 
2 (send方法调用结束，已经接收到全部HTTP响应消息) 
3 (正在解析响应内容，但状态和响应头还不可用) 
4 (完成)

status的取值：
1**	信息，服务器收到请求，需要请求者继续执行操作
2**	成功，操作被成功接收并处理
3**	重定向，需要进一步的操作以完成请求
4**	客户端错误，请求包含语法错误或无法完成请求
5**	服务器错误，服务器在处理请求的过程中发生了错误
*/
/*
option = {
    url:'./Handlers/AjaxHandler.ashx?username=zhangsan&sex=boy',
    type:'GET',
    succ:function(data){},
    err:function(err){}
};

option = {
    url:'./Handlers/AjaxHandler.ashx',
    type:'POST',
    data:{
        username:'zhangsan',
        sex:'boy'
    },
    succ:function(data){},
    err:function(err){}
};
*/
function myAjax(option) {
    //1）创建XMLHttpRequest对象
    var req = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHTTP");

    //2）发送AJAX请求
    req.open(option.type, option.url);
    if (option.type == "POST") {
        req.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        req.send(params2Str(option.data));
    }
    else {
        req.send(null);
    }

    //3）接收AJAX响应
    req.onreadystatechange = function () {
        if (req.readyState == 4 && req.status == 200) {
            //alert('响应消息的正文内容：' + req.responseText);
            option.succ(req.response);
        } else {
            option.err(req.response);
        }
    };
}

function params2Str(obj) {
    var arr = [];
    for (var it in obj) {
        arr.push(it + "=" + obj[it]);
    }
    return arr.join("&");
}