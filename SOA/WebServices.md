
# WebService
Web Service也叫XML Web Service WebService是一种可以接收从Internet或者Intranet上的其它系统中传递过来的请求，轻量级的独立的通讯技术。是:通过SOAP在Web上提供的软件服务，使用WSDL文件进行说明，并通过UDDI进行注册。

* XML：(Extensible Markup Language)扩展型可标记语言。面向短期的临时数据处理、面向万维网络，是Soap的基础。

* Soap：(Simple Object Access Protocol)简单对象存取协议。是XML Web Service 的通信协议。当用户通过UDDI找到你的WSDL描述文档后，他通过可以SOAP调用你建立的Web服务中的一个或多个操作。SOAP是XML文档形式的调用方法的规范，它可以支持不同的底层接口，像HTTP(S)或者SMTP。

* WSDL：(Web Services Description Language) WSDL 文件是一个 XML 文档，用于说明一组 SOAP 消息以及如何交换这些消息。大多数情况下由软件自动生成和使用。

* UDDI (Universal Description, Discovery, and Integration) 是一个主要针对Web服务供应商和使用者的新项目。在用户能够调用Web服务之前，必须确定这个服务内包含哪些商务方法，找到被调用的接口定义，还要在服务端来编制软件，UDDI是一种根据描述文档来引导系统查找相应服务的机制。UDDI利用SOAP消息机制（标准的XML/HTTP）来发布，编辑，浏览以及查找注册信息。它采用XML格式来封装各种不同类型的数据，并且发送到注册中心或者由注册中心来返回需要的数据。

WebService是一种跨编程语言和跨操作系统平台的远程调用技术。

## 创建

新建一个空的ASP.NET项目作为WebService项目，在项目上右键，新建项，选择【Web服务(ASMX)】，如下：

![](..\assets\SOA\webservice_create1.png)

在刚添加的asmx文件上右键，选择【在浏览器中查看】，

![](..\assets\SOA\webservice_create2.png)

浏览器中呈现 WebMethod HelloWorld，即表明服务新建成功：

![](..\assets\SOA\webservice_create3.png)

## WebMethod

新增一个WebMethod，在浏览器中查看或者发布均可，代码如下：
```cs
/// <summary>
/// TestWebService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.ComponentModel.ToolboxItem(false)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class TestWebService : System.Web.Services.WebService
{
    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public Student PostData(string name, string pwd)
    {
        return new Student() { Name = name, Pwd = pwd };
    }
}

public class Student
{
    public string Name { get; set; }
    public string Pwd { get; set; }
}
```

页面直接调用效果如下显示：

![](..\assets\SOA\webservice_webmethod.gif)

## 在ASP.NET程序中调用WebService

针对前面小节中已经部署的WebService的调用为例，我们在ASP.NET应用程序中添加服务引用，在项目引用上右键添加服务引用，

![](..\assets\SOA\webservice_client1.png)

在【添加服务引用】窗口内点击【发现】按钮，寻找项目中存在的WebService，命名空间可以修改也可以选择默认，如下：

![](..\assets\SOA\webservice_client2.png)

引用成功后，会新增服务引用，如下：

![](..\assets\SOA\webservice_client3.png)

对应的调用代码：
```cs
//服务代理类
TestServiceReference.TestWebServiceSoapClient srv = new TestServiceReference.TestWebServiceSoapClient();

var res = srv.PostData("jack", "admin");
```

不跨域的话，还可以通过jquery ajax进行调用，项目结构和代码如下：

![](..\assets\SOA\webservice_ajax1.png)

```js
$.ajax({
    //注意跨域问题，不要进行跨域访问
    url: "/testwebservice.asmx/PostData",
    dataType: "json",
    type: "post",
    //request contentType必须指明是json格式，否则服务端不知道客户端期望json格式
    contentType: "application/json; charset=utf-8",
    //由于request contentType指明为json的关系，传参必须以json字符串的形式传递，而不是通常所写的js对象
    data: '{ "name": "杰克马","pwd":"123123" }',
    success: function (data) {
        debugger;
    },
    error: function (err) { }
});
```

也可以添加 免费WebService接口的引用，以添加天气预报为示例：

添加服务引用窗口选择【高级】选项，然后选择【添加Web引用】，如图所示：

![](..\assets\SOA\webservice_web_client1.png)

粘贴免费WebService地址确定，添加引用即可：

> http://www.webxml.com.cn/WebServices/WeatherWebService.asmx

![](..\assets\SOA\webservice_web_client2.png)

完成引用后，即自动创建文件夹【Web References】及对应的web服务：

![](..\assets\SOA\webservice_web_client3.png)

调用也很简单，如下：
```cs
cn.com.webxml.www.WeatherWebService srv = new cn.com.webxml.www.WeatherWebService();
var cityWeather = srv.getWeatherbyCityName("芜湖");
```

另有云聚数据可以提供类似服务，免费用户都有一定的访问限制，具体可以查阅官网提供的文档。

> http://www.36wu.com/Service

## SOAP
SOAP（Simple Object Access Protocol ）简单对象访问协议，它是在分散或分布式的环境中交换信息的简单的协议，是一个基于XML的协议，它包括四个部分：

1. 封装：封装定义了一个描述消息中的内容是什么，是谁发送的，谁应当接受并处理它以及如何处理它们的框架
2. 编码规则：用于表示应用程序需要使用的数据类型的实例
3. RPC表示：表示远程过程调用和应答的协定
4. SOAP绑定：使用底层协议交换信息。

### 使用SOAP头自定义身份验证
身份验证和授权是控制用户访问所有Web应用程序时的两道安全机制，第一道是如何标识身份，第二道是如何分配权限。这里，我们来着重讨论一下在ASP.NET Web服务框架中如何实现身份验证机制。

在ASP.NET Web服务框架中，常见的身份验证的类型有三种：

1. IIS身份验证
2. ASP.NET身份验证
3. 自定义SOAP头身份验证

我们使用自定义头身份验证实现一个简单的身份验证功能,添加验证处理类 【SoapHeaderHelper.cs】，项目结构和代码如下：

![](..\assets\SOA\webservice_soap_header1.png)

```cs
/// <summary>
/// 自定义SOAP头
/// </summary>
public class SoapHeaderHelper : SoapHeader
{
    public string UserID { get; set; }
    public string UserPwd { get; set; }

    /// <summary>
    /// 简单验证消息
    /// out 参数 传递的不是副本，而是对象的引用
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public bool IsValid(out string msg)
    {
        if (UserID != "admin" || UserPwd != "admin")
        {
            msg = "无权访问";
            return false;
        }
        msg = string.Empty;
        return true;
    }
}
```

在需要验证的服务添加特性【SoapHeader】标签，指定验证的实例，如下：
```cs
/// <summary>
/// TestWebService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.ComponentModel.ToolboxItem(false)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class TestWebService : System.Web.Services.WebService
{
    /// <summary>
    /// 简单验证实例 必须为public
    /// </summary>
    public SoapHeaderHelper simpleValid = new SoapHeaderHelper();

    /// <summary>
    /// 根据编号查询图书信息
    /// 使用特性 [SoapHeader("simpleValid")] 进行简单验证
    /// </summary>
    /// <param name="bookID"></param>
    /// <returns></returns>
    [WebMethod(Description = "根据编号查询图书信息")]
    [SoapHeader("simpleValid")]
    public Book QueryBook(int bookID)
    {
        string msg = string.Empty;
        if (!simpleValid.IsValid(out msg))
        {
            return new Book() { Name = msg, Author = msg };
        }

        if (bookID == 1)
        {
            return new Book() { Name = "C#编程基础", Author = "张三" };
        }
        if (bookID == 2)
        {
            return new Book() { Name = "JAVA编程基础", Author = "李四" };
        }
        return new Book() { Name = "未知", Author = "未知" };
    }
}

/// <summary>
/// 图书类
/// </summary>
public class Book
{
    public string Name { get; set; }
    public string Author { get; set; }
}
```

在ASP.NET应用程序调用处，更新服务引用后，调用时添加验证信息，如下：
```cs
//服务代理
TestServiceReference.TestWebServiceSoapClient srv = new TestServiceReference.TestWebServiceSoapClient();

//验证对象
TestServiceReference.SoapHeaderHelper valid = new TestServiceReference.SoapHeaderHelper();
valid.UserID = "admin";
valid.UserPwd = "admin";

var res = srv.QueryBook(valid, 1);
```

参考引用：

[WebService到底是什么？](http://blog.csdn.net/wooshn/article/details/8069087)