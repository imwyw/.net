/****************************************ajax的封装**************************************************/
/*
option = {
    url:'../Handler/HomeHandler.ashx/Login?name=jack&pwd=1',//请求的连接
    type:'GET',//请求方式 GET/POST
    data:{name:'xxx',pwd:'xxx'},//post方式传递的参数
}
*/
function myAjax(option) {
    //1、创建XMLHttpRequest对象
    var req = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHTTP");

    //2、发送ajax请求
    if (option.type == "GET") {
        req.open("GET", option.url, true);
        req.send(null);
    } else {
        req.open('POST', option.url, true);
        req.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        req.send(formatParams(option.data));
    }

    //3、接收Ajax的响应
    req.onreadystatechange = function () {
        if (req.readyState == 4 && req.status == 200) {
            option.success(req.response);
        } else if (req.readyState == 4) {
            option.error(req.response);
        }
    }
}

//格式化参数，用于POST传递方式
function formatParams(data) {
    var arr = [];
    for (var name in data) {
        arr.push(encodeURIComponent(name) + "=" + encodeURIComponent(data[name]));
    }
    arr.push(("v=" + Math.random()).replace(".", ""));
    return arr.join("&");
}

function getAjax(u, succ, err) {
    myAjax({
        url: u,
        type: 'GET',
        success: succ,
        error: err
    });
}

function postAjax(u, obj, succ, err) {
    myAjax({
        url: u,
        data: obj,
        type: 'POST',
        success: succ,
        error: err
    });
}

/****************************************ajax的封装 end **************************************************/

function getEm(id) {
    return document.getElementById(id);
}