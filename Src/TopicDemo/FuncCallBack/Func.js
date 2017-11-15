/*
JavaScript函数和回调
*/

/* 1、第一种定义方式
通过函数定义式  我们的调用必须在声明之后
*/
foo('jack');//Uncaught TypeError: foo is not a function
var foo = function (name) {
    console.log(name);
}

/* 2、第二种定义方式
无所谓function的定义位置
*/
tee('lucy');
function tee(name) {
    console.log(name);
}

/*
回调
*/
var callback = function () { console.log('我是回调函数打印出来的：callback'); };
function parent(cb) {
    cb();
}
//将函数callback作为参数传递给parent
parent(callback);

//简化后的代码：
/*
function parent() {
    var callback = function () { console.log('我是回调函数打印出来的：callback'); };
    callback();
}
*/

//回调之提升，有参回调
var small = function (data) { console.warn('hello 你好 ' + data); };
function big(cb) {
    cb('树先生');
}
big(small);