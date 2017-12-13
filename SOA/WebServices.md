
# WebService
Web服务技术（Web Services）是架构一个平台独立的，低耦合的，自包含的Web服务实例（Web Service）的技术框架，常用于开发分布式互操作的应用程序。

是一种在 Web 上部署并可以被任何应用程序或其他服务调用的服务。主要特点：
1. 互操作性：任何的 Web Service都可以与其他 Web Service进行交互
2. 普遍性：Web Service使用 HTTP 和 XML 进行通信
3. 松散耦合

我们可以简单的理解为就是一个对外的接口。假若我们是服务端，我们写好了个webservice，然后把它给了客户（同时我们给了他们调用规则），客户就可以在从服务端获取信息时处于一个相对透明的状态。即使客户不了解（也不需要）其过程，他们只获取数据。

通过SOAP在Web上提供的软件服务，使用WSDL文件进行说明，并通过UDDI进行注册。

webservice传递的数据只能是序列化的数据，典型的就是xml数据。

* XML：(Extensible Markup Language)扩展型可标记语言。面向短期的临时数据处理、面向万维网络，是Soap的基础。
* Soap：(Simple Object Access Protocol)简单对象存取协议。是XML Web Service 的通信协议。

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
TestServiceReference.TestWebServiceSoapClient client = new TestServiceReference.TestWebServiceSoapClient();

var res = client.PostData("jack", "admin");
```

不跨域的话，还可以通过jquery ajax进行调用，如下：
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

## SOAP
SOAP（Simple Object Access Protocol ）简单对象访问协议，它是在分散或分布式的环境中交换信息的简单的协议，是一个基于XML的协议，它包括四个部分：

1. 封装：封装定义了一个描述消息中的内容是什么，是谁发送的，谁应当接受并处理它以及如何处理它们的框架
2. 编码规则：用于表示应用程序需要使用的数据类型的实例
3. RPC表示：表示远程过程调用和应答的协定
4. SOAP绑定：使用底层协议交换信息。

