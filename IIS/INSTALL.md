# 安装
## 启用或关闭windows应用

### 打开控制面板

![](../assets/iis/1.png) 

### 选择程序

![](../assets/iis/2.png)

### 打开或关闭windows功能

![](../assets/iis/3.png)

## 启用IIS功能

### 如下图所示勾选功能

![](../assets/iis/4.png)

### 启用功能成功

![](../assets/iis/5.png)

# 打开IIS

## 方法1 开始菜单-管理工具-IIS

![](../assets/iis/6.png)

## 方法2 运行命令中输入 inetmgr（推荐，高逼格）

![](../assets/iis/7.png)

## 方法3 桌面上右键点击此电脑，管理

![](../assets/iis/8.png)

## 进入IIS管理器

![](../assets/iis/9.png)

# 简单测试

## 默认Default Web Site

默认网站的端口为80，可在浏览器中测试：
>http://localhost

>http://localhost:80

>http://127.0.0.1

出现如下界面表示启用IIS成功

![](../assets/iis/10.png)

## 简单静态页面测试

### 新建html页面
新建一个html静态页面，内容如下：
``` html
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>测试页面</title>
    <style type="text/css">
        .center {
            text-align: center;
        }
    </style>
</head>
<body>

    <header class="center">HTML测试页面</header>
    <h1>标题一</h1>
    <hr />
    <p>段落</p>
    <footer class="center">Copyright</footer>
</body>
</html>

```

文件保存物理路径为 D:\release\myWeb\1.html

### 添加网站

IIS管理器添加一个网站

![](../assets/iis/11.png)

网站名称随意，与文件目录名称无关，应用程序池可新建也可使用默认添加的。

物理路径一定要选择上一步保存的【D:\release\myWeb\】。

端口号默认80已经被默认网站占用，需要重新设置，简单引申下【端口号】。

端口号支持0-65535，通常我们从5000往后开始用，小于5000的端口不建议使用，否则容易发生端口冲突！

![](../assets/iis/12.png)

### 添加默认文档

IIS管理器中选择刚刚添加的网站，添加默认文档

![](../assets/iis/13.png)

添加文件名称，与添加的静态页面文件名称保持一致

![](../assets/iis/14.png)

### 浏览测试

浏览器中输入地址测试

>http://localhost:8081

返回默认文档 1.html成功！

![](../assets/iis/15.png)

