# 安装
## 启用或关闭windows应用

### 打开控制面板

![](../assets/IIS/1.png) 

### 选择程序

![](../assets/IIS/2.png)

### 打开或关闭windows功能

![](../assets/IIS/3.png)

## 启用IIS功能

### 如下图所示勾选功能

![](../assets/IIS/4.png)

### 启用功能成功

![](../assets/IIS/5.png)

# 打开IIS

## 方法1 开始菜单-管理工具-IIS

![](../assets/IIS/6.png)

## 方法2 运行命令中输入 inetmgr（推荐，高逼格）

![](../assets/IIS/7.png)

## 方法3 桌面上右键点击此电脑，管理

![](../assets/IIS/8.png)

## 进入IIS管理器

![](../assets/IIS/9.png)

# 简单测试

## 默认Default Web Site

默认网站的端口为80，可在浏览器中测试：
>http://localhost

>http://localhost:80

>http://127.0.0.1

出现如下界面表示启用IIS成功

![](../assets/IIS/10.png)

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

![](../assets/IIS/11.png)

网站名称随意，与文件目录名称无关，应用程序池可新建也可使用默认添加的。

物理路径一定要选择上一步保存的【D:\release\myWeb\】。

端口号默认80已经被默认网站占用，需要重新设置，简单引申下【端口号】。

 从1-65535，其中从1-1023叫知名端口号，也叫源端口号。这些端口是被tcp和udp定义了的端口，从1024-49151叫做已注册端口号，被一些公司用于自己的某种协议。49152-65535叫动态分配端口号，是我们随便可以用的。

![](../assets/IIS/12.png)

### 添加默认文档

IIS管理器中选择刚刚添加的网站，添加默认文档

![](../assets/IIS/13.png)

添加文件名称，与添加的静态页面文件名称保持一致

![](../assets/IIS/14.png)

### 浏览测试

浏览器中输入地址测试

>http://localhost:8081

返回默认文档 1.html成功！

![](../assets/IIS/15.png)

# MVC项目的发布
## 使用文件系统发布
设置MVC项目为启动项

![](../assets/IIS/mvc1.png)

UI项目右键选择【发布】选项

![](../assets/IIS/output_pub.gif)

编译成功，发布成功

![](../assets/IIS/output_print.png)

生成bin包到指定目录

![](../assets/IIS/output_dir.png)

## 添加站点

添加一个站点，注意物理路径对应上一步生成的bin包路径

![](../assets/IIS/mvc_publish.png)

直接访问即可 
>http://localhost:8082

## 发布到IIS网站调试
发布到IIS的网站如何进入调试，与IDE自带的IIS Express调试不同

MVC项目上右键选择【属性】-【Web】选项，将服务器修改为"本地IIS"，
项目URL修改为对应目录

![](../assets/IIS/debug_iis.png)

直接调试（F5）即可进入调试状态

# 疑难杂症
##　启用windows功能报错 0x80070057
环境是win10专业版，参数错误

![](../assets/IIS/0x80070057.png)

尝试解决方案有：
- 打开windows update服务，更新到最新，无效；
- 修改注册表项IsConvergedUpdateStackEnabled，无法保存；


