<!-- TOC -->

- [WebAPI](#webapi)
    - [概念](#概念)
        - [什么是WebAPI](#什么是webapi)
        - [为什么用WebAPI](#为什么用webapi)
        - [功能简介](#功能简介)
        - [Web API vs MVC](#web-api-vs-mvc)
        - [Web API vs WCF](#web-api-vs-wcf)
    - [入门项目](#入门项目)
        - [创建项目](#创建项目)
        - [新建Model和Controller](#新建model和controller)
        - [前端模拟交互](#前端模拟交互)
    - [路由](#路由)
        - [路由表](#路由表)
    - [返回类型](#返回类型)
        - [void](#void)
        - [IHttpActionResult](#ihttpactionresult)
        - [HttpResponseMessage](#httpresponsemessage)
        - [自定义类型](#自定义类型)
    - [token验证](#token验证)
        - [安全问题](#安全问题)
        - [添加Owin包](#添加owin包)
        - [StartUp类](#startup类)
        - [添加验证类](#添加验证类)
        - [Authorize授权](#authorize授权)
        - [前端html请求](#前端html请求)

<!-- /TOC -->

<a id="markdown-webapi" name="webapi"></a>
# WebAPI

Web API是一个比较宽泛的概念。这里我们提到Web API特指ASP.NET Web API。

<a id="markdown-概念" name="概念"></a>
## 概念

<a id="markdown-什么是webapi" name="什么是webapi"></a>
### 什么是WebAPI
官方定义如下，强调两个关键点，即可以对接各种客户端（浏览器，移动设备），构建http服务的框架。

> ASP.NET Web API is a framework that makes it easy to build HTTP services that reach a broad range of clients, including browsers and mobile devices. ASP.NET Web API is an ideal platform for building RESTful applications on the .NET Framework.

Web API在ASP.NET完整框架中地位如下图，与SignalR一起同为构建Service的框架。

![](../assets/webapi/asp.net-webapi.png)

Web API负责构建http常规服务，而SingalR主要负责的是构建实时服务，例如股票，聊天室，在线游戏等实时性要求比较高的服务。

<a id="markdown-为什么用webapi" name="为什么用webapi"></a>
### 为什么用WebAPI
Web API最重要的是可以构建面向各种客户端的服务。

另外与WCF REST Service不同在于，Web API利用Http协议的各个方面来表达服务(例如 URI/request response header/caching/versioning/content format)，因此就省掉很多配置。

![](../assets/webapi/webapi-1.png)

当你遇到以下这些情况的时候，就可以考虑使用Web API了。
* 需要Web Service但是不需要SOAP
* 需要在已有的WCF服务基础上建立non-soap-based http服务
* 只想发布一些简单的Http服务，不想使用相对复杂的WCF配置
* 发布的服务可能会被带宽受限的设备访问
* 希望使用开源框架，关键时候可以自己调试或者自定义一下框架

<a id="markdown-功能简介" name="功能简介"></a>
### 功能简介
Web API的主要功能
1. 支持基于Http verb (GET, POST, PUT, DELETE)的CRUD (create, retrieve, update, delete)操作。通过不同的http动作表达不同的含义，这样就不需要暴露多个API来支持这些基本操作。
2. 请求的回复通过Http Status Code表达不同含义，并且客户端可以通过Accept header来与服务器协商格式，例如你希望服务器返回JSON格式还是XML格式。
3. 请求的回复格式支持 JSON，XML，并且可以扩展添加其他格式。
4. 原生支持OData。
5. 支持Self-host或者IIS host。
6. 支持大多数MVC功能，例如Routing/Controller/Action Result/Filter/Model Builder/IOC Container/Dependency Injection。

<a id="markdown-web-api-vs-mvc" name="web-api-vs-mvc"></a>
### Web API vs MVC
你可能会觉得Web API 与MVC很类似，他们有哪些不同之处呢？先上图，这就是他们最大的不同之处。

![](../assets/webapi/webapi-vs-mvc.png)

详细点说他们的区别：
* MVC主要用来构建网站，既关心数据也关心页面展示，而Web API只关注数据
* Web API支持格式协商，客户端可以通过Accept header通知服务器期望的格式
* Web API支持Self Host，MVC目前不支持
* Web API通过不同的http verb表达不同的动作(CRUD)，MVC则通过Action名字表达动作
* Web API内建于ASP.NET System.Web.Http命名空间下，MVC位于System.Web.Mvc命名空间下，因此model binding/filter/routing等功能有所不同
* 最后，Web API非常适合构建移动客户端服务

<a id="markdown-web-api-vs-wcf" name="web-api-vs-wcf"></a>
### Web API vs WCF
发布服务在Web API和WCF之间该如何取舍呢？这里提供些简单地判断规则，

* 如果服务需要支持One Way Messaging/Message Queue/Duplex Communication，选择WCF
* 如果服务需要在TCP/Named Pipes/UDP (wcf 4.5)，选择WCF
* 如果服务需要在http协议上，并且希望利用http协议的各种功能，选择Web API
* 如果服务需要被各种客户端(特别是移动客户端)调用，选择Web API

<a id="markdown-入门项目" name="入门项目"></a>
## 入门项目
<a id="markdown-创建项目" name="创建项目"></a>
### 创建项目
在项目模板列表中选择ASP.NET Web 应用程序，新建Web应用程序

命名项目"ProductsApp"，然后单击确定。

![](../assets/webapi/webapi-create.jpg)

在中新建 ASP.NET 项目对话框中，选择【Empty】空模板，勾选【Web API】选项引用。 单击 “确定”。

![](../assets/webapi/webapi-create2.jpg)

<a id="markdown-新建model和controller" name="新建model和controller"></a>
### 新建Model和Controller

新建Model：

![](../assets/webapi/webapi-create-prod1.jpg)

```cs
namespace ProductsApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}
```

新建控制器,选择Web API 控制器-空，如图：

![](../assets/webapi/webapi-create-controller1.jpg)

将控制器命名"ProductsController"。修改控制器内容如下：

![](../assets/webapi/webapi-create-controller2.jpg)

```cs
public class ProductsController : ApiController
{
    Product[] products = new Product[]
    {
        new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
        new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
        new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
    };

    public IEnumerable<Product> GetAllProducts()
    {
        return products;
    }

    public IHttpActionResult GetProduct(int id)
    {
        var product = products.FirstOrDefault((p) => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }
}
```

* GetAllProducts方法返回作为产品的整个列表IEnumerable<产品> 类型。
* GetProduct方法查找单个产品通过其 id。

<a id="markdown-前端模拟交互" name="前端模拟交互"></a>
### 前端模拟交互

选择Web节点下的Visual C#，然后选择HTML 页项。 将该页命名为"index.html"。

![](../assets/webapi/webapi-create-proj.jpg)

```html
<!DOCTYPE html>
<html>
<head>
    <title>Product App</title>
</head>
<body>

    <div>
        <h2>All Products</h2>
        <ul id="products" />
    </div>
    <div>
        <h2>Search by ID</h2>
        <input type="text" id="prodId" size="5" />
        <input type="button" value="Search" onclick="find();" />
        <p id="product" />
    </div>

    <!--此处使用的是Microsoft Ajax CDN-->
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.0.3.min.js"></script>
    <script>
        //注意此处开始需要有 斜杠'/'，否则会因为路径的问题导致数据获取有误
        var uri = '/api/products';

        $(document).ready(function () {
            // Send an AJAX request
            $.getJSON(uri)
                .done(function (data) {
                    // On success, 'data' contains a list of products.
                    $.each(data, function (key, item) {
                        // Add a list item for the product.
                        $('<li>', { text: formatItem(item) }).appendTo($('#products'));
                    });
                });
        });

        function formatItem(item) {
            return item.Name + ': $' + item.Price;
        }

        function find() {
            var id = $('#prodId').val();
            $.getJSON(uri + '/' + id)
                .done(function (data) {
                    $('#product').text(formatItem(data));
                })
                .fail(function (jqXHR, textStatus, err) {
                    $('#product').text('Error: ' + err);
                });
        }
    </script>
</body>
</html>
```

开始调试应用程序。 网页应如下所示：

![](../assets/webapi/webapi-res1.jpg)

<a id="markdown-路由" name="路由"></a>
## 路由

<a id="markdown-路由表" name="路由表"></a>
### 路由表

在 ASP.NET Web API 中，控制器是处理 HTTP 请求的类。 调用控制器的公共方法操作方法或只需操作。 

当 Web API 框架收到请求时，它将请求路由到某个操作。

若要确定要调用的操作，框架将使用路由表。 【WebApiConfig.cs】文件

```cs
routes.MapHttpRoute(
    name: "API Default",
    routeTemplate: "api/{controller}/{id}",
    defaults: new { id = RouteParameter.Optional }
);
```

路由表中的每个条目包含路由模板。 Web API 的默认路由模板"api / {controller} / {id}"。 

在此模板中， "api"是文本路径段和 {controller} 和 {id} 是占位符变量。

Web API 框架接收 HTTP 请求时，它尝试匹配根据一个路由表中的路由模板的 URI。 

如果没有路由匹配，客户端收到 404 错误。 例如，下面的 Uri 匹配的默认路由：

* / api/联系人
* /api/contacts/1
* /api/products/gizmo1

但是，下面的 URI 不匹配，因为它缺少"api"段：

* / contacts/1

> 在路由中使用"api"的原因是避免使用 ASP.NET MVC 路由冲突。 这样一来，可以具有"/contacts"转到 MVC 控制器，并"/api/contacts"转到 Web API 控制器。 当然，如果您不喜欢这种约定，您可以更改默认路由表。

一旦找到匹配的路由，Web API 选择控制器和操作：

* 若要查找控制器，Web API 将添加"控制器"的值 {controller} 变量。
* 若要查找操作，Web API 的 HTTP 方法，将查看，然后查找其名称以与该 HTTP 方法名称的操作。 例如，使用 GET 请求，Web API 查找操作以开头的"GET..."，如"GetContact"或"GetAllContacts"。 此约定仅适用于GET、 POST、 PUT 和 DELETE 方法。 可以通过在控制器上使用属性来启用其他 HTTP 方法。 我们将看到一个示例说明更高版本。
* 其他占位符变量在路由模板中，如 {id} 映射到操作参数。

让我们看一个示例，假设定义以下控制器：
```cs
public class ProductsController : ApiController
{
    public IEnumerable<Product> GetAllProducts() { }
    public Product GetProductById(int id) { }
    public HttpResponseMessage DeleteProduct(int id){ }
}
```

下面是一些可能的 HTTP 请求，以及每个调用的操作：

HTTP 方法 | URI 路径 | 操作 | 参数
--------|--------|----|---
GET | api/products | GetAllProducts | （无）
GET | api/products/4 | GetProductById | 4
DELETE | api/products/4 | DeleteProduct | 4

请注意， {id} 段的 URI，如果存在，将映射到id操作的操作参数。 

在此示例中，控制器定义了两个 GET 方法，另一个使用id参数，另一个不带任何参数。

以上，为默认的apil路由，相比较mvc的路由缺少了{action}段，在使用上来看并没有那么方便。

我们可以在路由中包含action操作名称来解决这个问题，修改【WebApiConfig.cs】文件新增action段如下：

```cs
routes.MapHttpRoute(
    name: "ActionApi",
    routeTemplate: "api/{controller}/{action}/{id}",
    defaults: new { id = RouteParameter.Optional }
);
```

在此路由模板中， {action} 参数名称在控制器上的操作方法。 

使用此样式的路由，使用属性来指定允许的 HTTP 方法。

例如，假设您的控制器具有以下方法：

```cs
public class ProductsController : ApiController
{
    [HttpGet]
    public string Details(int id);
}
```

在这种情况下，"api/products/Details/1"的 GET 请求将映射到的详细信息的方法。 

这种路由是类似于 ASP.NET MVC 中，并可能适用于 RPC 样式 API。


可以通过重写操作的名称ActionName属性。 在以下示例中，有两个操作映射到"api/products/thumbnail/id"。

一种支持 GET方法 和另一种支持 POST方法 ：

```cs
public class ProductsController : ApiController
{
    [HttpGet]
    [ActionName("Thumbnail")]
    public HttpResponseMessage GetThumbnailImage(int id);

    [HttpPost]
    [ActionName("Thumbnail")]
    public void AddThumbnailImage(int id);
}
```

若要防止为一个操作中调用一个action，使用NonAction属性。 NonAction表明该方法并非一个action，即使它符合路由规则。

```cs
// Not an action method.
[NonAction]  
public string GetPrivateData() { ... }
```

尝试访问会返回：No action was found on the controller 'ControllerName' that matches the name 'ActionName'.


<a id="markdown-返回类型" name="返回类型"></a>
## 返回类型

Web API 控制器操作可以返回以下任一项：

返回类型 | Web API 创建响应的方式
-----|----------------
void | 返回空 204 （无内容）
HttpResponseMessage | 转换为直接 HTTP 响应消息。
IHttpActionResult | 调用ExecuteAsync来创建HttpResponseMessage，然后将转换为 HTTP 响应消息。
其他类型 | 将序列化的返回值写入到响应正文中;返回 200 （正常）。

<a id="markdown-void" name="void"></a>
### void
```cs
public class ValuesController : ApiController
{
    [HttpGet]
    public void Get()
    {
        int a = 1;
        int b = 2;
        int c = a + b;
        //.....................
    }
}
```

使用postman，测试接口：

![](../assets/webapi/return_void.png)

可以看到，void声明的接口，在请求成功的时候得不到返回值，而且会返回http的状态码为204，表示没有返回值。

<a id="markdown-ihttpactionresult" name="ihttpactionresult"></a>
### IHttpActionResult
IHttpActionResult是WebApi最常用的一种返回值类型，常用的方式有：

Json(T content)、Ok()、 Ok(T content)、NotFound()、Content(HttpStatusCode statusCode, T value)、BadRequest()、Redirect(string location)等

Json(T content)在WebApi的ApiController这个抽象类里面，为我们封装了Json(T content)这个方法，它的用法和MVC里面的JsonResult基本类似。

```cs
[HttpGet]
public IHttpActionResult getJson()
{
    var list = new List<userinfo>();
    list.Add(new UserInfo { Name="jeck",age=22 });
    list.Add(new UserInfo { Name = "poor", age = 23 });
    return Json<List<UserInfo>>(list);
}

public class UserInfo{
    public string Name { get; set; }
    public int age { get; set; }
}
```

测试结果：

![](../assets/webapi/return_json.png)

<a id="markdown-httpresponsemessage" name="httpresponsemessage"></a>
### HttpResponseMessage
HttpResponseMessage这个对象，表示向客户端返回一个http响应的消息对象（包含http状态码和需要返回客户端的消息）。

这个对象也有它独特的使用场景：需要向客户端返回HttpResponse时就要用到这个对象。

以导出为例，由于需要将导出的Excel文件输出到客户端浏览器，Webapi的服务端需要向Web的客户端输出文件流，

这个时候一般的IHttpActionResult对象不方便解决这个问题，于是HttpReponseMessage派上了用场。

```cs
[HttpGet]
public HttpResponseMessage DownloadFile()
{
    string fileName = HttpContext.Current.Server.MapPath("~/App_Data/1.xlsx");
    FileStream fs = new FileStream(fileName, FileMode.Open);

    HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.OK);
    resp.Content = new StreamContent(fs);
    string mimetype = MimeMapping.GetMimeMapping(fileName);
    resp.Content.Headers.ContentType = new MediaTypeHeaderValue(mimetype);
    resp.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
    {
        FileName = HttpUtility.UrlEncode(Path.GetFileName(fileName))
    };

    return resp;
}
```

通过浏览器访问即可下载

<a id="markdown-自定义类型" name="自定义类型"></a>
### 自定义类型
WebApi会自动序列化你自定义任何返回类型，然后将序列化的值写到响应正文里，状态码统一返回200。

```cs
static List<Product> listProducts = new List<Product>();
/// <summary>
/// 使用 x-www-form-urlencoded 键值对方式发送数据，使用$.ajax()默认类型发送请求
/// </summary>
/// <param name="pro"></param>
/// <returns></returns>
public IHttpActionResult SaveProduct(Product pro)
{
    if (null != pro)
    {
        listProducts.Add(pro);
    }
    return Ok(listProducts);
}
```

<a id="markdown-token验证" name="token验证"></a>
## token验证

<a id="markdown-安全问题" name="安全问题"></a>
### 安全问题

基于前面新的webapi，可以通过PostMan进行测试：

获取所有产品信息

http://localhost:10034/api/Products/GetAllProducts

![](../assets/webapi/get_all.png)


获取指定id的product

http://localhost:10034/api/Products/GetProduct?id=1

![](../assets/webapi/get_one.png)

可以看到这个产品的API是公开访问的，没有任何验证，这样不是很安全，下一步我将加上token验证。

<a id="markdown-添加owin包" name="添加owin包"></a>
### 添加Owin包

打开NuGet包管理器控制台

![](../assets/webapi/nuget_console.png)


在控制台依次输入如下指令安装扩展包：

```
Install-Package Microsoft.AspNet.WebApi.Owin
Install-Package Microsoft.Owin.Host.SystemWeb
Install-Package Microsoft.AspNet.Identity.Owin
Install-Package Microsoft.Owin.Cors
Install-Package EntityFramework
```

如下所示：
```
每个程序包都由其所有者许可给你。Microsoft 不负责第三方程序包，也不授予其许可证。一些程序包可能包括受其他许可证约束的依赖项。单击程序包源(源) URL 可确定任何依赖项。

程序包管理器控制台主机版本 3.0.0.0

键入 "get-help NuGet" 可查看所有可用的 NuGet 命令。

PM> Install-Package Microsoft.AspNet.WebApi.Owin
正在尝试收集与目标为“.NETFramework,Version=v4.5”的项目“ProductApp”有关的程序包“Microsoft.AspNet.WebApi.Owin.5.2.7”的相关依赖项信息
正在尝试解析程序包“Microsoft.AspNet.WebApi.Owin.5.2.7”的依赖项，DependencyBehavior 为“Lowest”
正在解析操作以安装程序包“Microsoft.AspNet.WebApi.Owin.5.2.7”
已解析操作以安装程序包“Microsoft.AspNet.WebApi.Owin.5.2.7”
已从“packages.config”中删除程序包“Microsoft.AspNet.WebApi.Client.5.2.3”
已从 ProductApp 成功卸载“Microsoft.AspNet.WebApi.Client.5.2.3”
已从“packages.config”中删除程序包“Microsoft.AspNet.WebApi.Client.zh-Hans.5.2.3”
已从 ProductApp 成功卸载“Microsoft.AspNet.WebApi.Client.zh-Hans.5.2.3”
已从“packages.config”中删除程序包“Microsoft.AspNet.WebApi.Core.5.2.3”
已从 ProductApp 成功卸载“Microsoft.AspNet.WebApi.Core.5.2.3”
已从“packages.config”中删除程序包“Microsoft.AspNet.WebApi.Core.zh-Hans.5.2.3”
已从 ProductApp 成功卸载“Microsoft.AspNet.WebApi.Core.zh-Hans.5.2.3”
程序包“Microsoft.AspNet.WebApi.Client.5.2.7”已存在于文件夹“D:\Codes\dotNet\WebApiDemo\packages”中
已将程序包“Microsoft.AspNet.WebApi.Client.5.2.7”添加到“packages.config”
已将“Microsoft.AspNet.WebApi.Client 5.2.7”成功安装到 ProductApp
程序包“Microsoft.AspNet.WebApi.Client.zh-Hans.5.2.7”已存在于文件夹“D:\Codes\dotNet\WebApiDemo\packages”中
已将程序包“Microsoft.AspNet.WebApi.Client.zh-Hans.5.2.7”添加到“packages.config”
已将“Microsoft.AspNet.WebApi.Client.zh-Hans 5.2.7”成功安装到 ProductApp
程序包“Microsoft.AspNet.WebApi.Core.5.2.7”已存在于文件夹“D:\Codes\dotNet\WebApiDemo\packages”中
已将程序包“Microsoft.AspNet.WebApi.Core.5.2.7”添加到“packages.config”
已将“Microsoft.AspNet.WebApi.Core 5.2.7”成功安装到 ProductApp
程序包“Microsoft.AspNet.WebApi.Core.zh-Hans.5.2.7”已存在于文件夹“D:\Codes\dotNet\WebApiDemo\packages”中
已将程序包“Microsoft.AspNet.WebApi.Core.zh-Hans.5.2.7”添加到“packages.config”
已将“Microsoft.AspNet.WebApi.Core.zh-Hans 5.2.7”成功安装到 ProductApp
程序包“Owin.1.0.0”已存在于文件夹“D:\Codes\dotNet\WebApiDemo\packages”中
已将程序包“Owin.1.0.0”添加到“packages.config”
已将“Owin 1.0.0”成功安装到 ProductApp
正在将程序包“Microsoft.Owin.2.0.2”添加到文件夹“D:\Codes\dotNet\WebApiDemo\packages”
已将程序包“Microsoft.Owin.2.0.2”添加到文件夹“D:\Codes\dotNet\WebApiDemo\packages”
已将程序包“Microsoft.Owin.2.0.2”添加到“packages.config”
已将“Microsoft.Owin 2.0.2”成功安装到 ProductApp
程序包“Microsoft.AspNet.WebApi.Owin.5.2.7”已存在于文件夹“D:\Codes\dotNet\WebApiDemo\packages”中
已将程序包“Microsoft.AspNet.WebApi.Owin.5.2.7”添加到“packages.config”
已将“Microsoft.AspNet.WebApi.Owin 5.2.7”成功安装到 ProductApp
正在从文件夹“D:\Codes\dotNet\WebApiDemo\packages”中删除程序包“Microsoft.AspNet.WebApi.Client.5.2.3”
已从文件夹“D:\Codes\dotNet\WebApiDemo\packages”中删除程序包“Microsoft.AspNet.WebApi.Client.5.2.3”
正在从文件夹“D:\Codes\dotNet\WebApiDemo\packages”中删除程序包“Microsoft.AspNet.WebApi.Client.zh-Hans.5.2.3”
已从文件夹“D:\Codes\dotNet\WebApiDemo\packages”中删除程序包“Microsoft.AspNet.WebApi.Client.zh-Hans.5.2.3”
正在从文件夹“D:\Codes\dotNet\WebApiDemo\packages”中删除程序包“Microsoft.AspNet.WebApi.Core.5.2.3”
已从文件夹“D:\Codes\dotNet\WebApiDemo\packages”中删除程序包“Microsoft.AspNet.WebApi.Core.5.2.3”
正在从文件夹“D:\Codes\dotNet\WebApiDemo\packages”中删除程序包“Microsoft.AspNet.WebApi.Core.zh-Hans.5.2.3”
已从文件夹“D:\Codes\dotNet\WebApiDemo\packages”中删除程序包“Microsoft.AspNet.WebApi.Core.zh-Hans.5.2.3”
PM> Install-Package Microsoft.Owin.Host.SystemWeb
正在尝试收集与目标为“.NETFramework,Version=v4.5”的项目“ProductApp”有关的程序包“Microsoft.Owin.Host.SystemWeb.4.0.1”的相关依赖项信息
正在尝试解析程序包“Microsoft.Owin.Host.SystemWeb.4.0.1”的依赖项，DependencyBehavior 为“Lowest”
正在解析操作以安装程序包“Microsoft.Owin.Host.SystemWeb.4.0.1”
已解析操作以安装程序包“Microsoft.Owin.Host.SystemWeb.4.0.1”
已从“packages.config”中删除程序包“Microsoft.Owin.2.0.2”
已从 ProductApp 成功卸载“Microsoft.Owin.2.0.2”
程序包“Microsoft.Owin.4.0.1”已存在于文件夹“D:\Codes\dotNet\WebApiDemo\packages”中
已将程序包“Microsoft.Owin.4.0.1”添加到“packages.config”
已将“Microsoft.Owin 4.0.1”成功安装到 ProductApp
程序包“Microsoft.Owin.Host.SystemWeb.4.0.1”已存在于文件夹“D:\Codes\dotNet\WebApiDemo\packages”中
已将程序包“Microsoft.Owin.Host.SystemWeb.4.0.1”添加到“packages.config”
已将“Microsoft.Owin.Host.SystemWeb 4.0.1”成功安装到 ProductApp
正在从文件夹“D:\Codes\dotNet\WebApiDemo\packages”中删除程序包“Microsoft.Owin.2.0.2”
已从文件夹“D:\Codes\dotNet\WebApiDemo\packages”中删除程序包“Microsoft.Owin.2.0.2”
PM> Install-Package Microsoft.AspNet.Identity.Owin
正在尝试收集与目标为“.NETFramework,Version=v4.5”的项目“ProductApp”有关的程序包“Microsoft.AspNet.Identity.Owin.2.2.2”的相关依赖项信息
正在尝试解析程序包“Microsoft.AspNet.Identity.Owin.2.2.2”的依赖项，DependencyBehavior 为“Lowest”
正在解析操作以安装程序包“Microsoft.AspNet.Identity.Owin.2.2.2”
已解析操作以安装程序包“Microsoft.AspNet.Identity.Owin.2.2.2”
程序包“Microsoft.AspNet.Identity.Core.2.2.2”已存在于文件夹“D:\Codes\dotNet\WebApiDemo\packages”中
已将程序包“Microsoft.AspNet.Identity.Core.2.2.2”添加到“packages.config”
已将“Microsoft.AspNet.Identity.Core 2.2.2”成功安装到 ProductApp
程序包“Microsoft.Owin.Security.3.0.1”已存在于文件夹“D:\Codes\dotNet\WebApiDemo\packages”中
已将程序包“Microsoft.Owin.Security.3.0.1”添加到“packages.config”
已将“Microsoft.Owin.Security 3.0.1”成功安装到 ProductApp
程序包“Microsoft.Owin.Security.Cookies.3.0.1”已存在于文件夹“D:\Codes\dotNet\WebApiDemo\packages”中
已将程序包“Microsoft.Owin.Security.Cookies.3.0.1”添加到“packages.config”
已将“Microsoft.Owin.Security.Cookies 3.0.1”成功安装到 ProductApp
程序包“Microsoft.Owin.Security.OAuth.3.0.1”已存在于文件夹“D:\Codes\dotNet\WebApiDemo\packages”中
已将程序包“Microsoft.Owin.Security.OAuth.3.0.1”添加到“packages.config”
已将“Microsoft.Owin.Security.OAuth 3.0.1”成功安装到 ProductApp
程序包“Microsoft.AspNet.Identity.Owin.2.2.2”已存在于文件夹“D:\Codes\dotNet\WebApiDemo\packages”中
已将程序包“Microsoft.AspNet.Identity.Owin.2.2.2”添加到“packages.config”
已将“Microsoft.AspNet.Identity.Owin 2.2.2”成功安装到 ProductApp
PM> Install-Package Microsoft.Owin.Cors
正在尝试收集与目标为“.NETFramework,Version=v4.5”的项目“ProductApp”有关的程序包“Microsoft.Owin.Cors.4.0.1”的相关依赖项信息
正在尝试解析程序包“Microsoft.Owin.Cors.4.0.1”的依赖项，DependencyBehavior 为“Lowest”
正在解析操作以安装程序包“Microsoft.Owin.Cors.4.0.1”
已解析操作以安装程序包“Microsoft.Owin.Cors.4.0.1”
程序包“Microsoft.AspNet.Cors.5.0.0”已存在于文件夹“D:\Codes\dotNet\WebApiDemo\packages”中
已将程序包“Microsoft.AspNet.Cors.5.0.0”添加到“packages.config”
已将“Microsoft.AspNet.Cors 5.0.0”成功安装到 ProductApp
程序包“Microsoft.Owin.Cors.4.0.1”已存在于文件夹“D:\Codes\dotNet\WebApiDemo\packages”中
已将程序包“Microsoft.Owin.Cors.4.0.1”添加到“packages.config”
已将“Microsoft.Owin.Cors 4.0.1”成功安装到 ProductApp
PM> Install-Package EntityFramework
正在尝试收集与目标为“.NETFramework,Version=v4.5”的项目“ProductApp”有关的程序包“EntityFramework.6.2.0”的相关依赖项信息
正在尝试解析程序包“EntityFramework.6.2.0”的依赖项，DependencyBehavior 为“Lowest”
正在解析操作以安装程序包“EntityFramework.6.2.0”
已解析操作以安装程序包“EntityFramework.6.2.0”
程序包“EntityFramework.6.2.0”已存在于文件夹“D:\Codes\dotNet\WebApiDemo\packages”中
已将程序包“EntityFramework.6.2.0”添加到“packages.config”
正在执行脚本文件“D:\Codes\dotNet\WebApiDemo\packages\EntityFramework.6.2.0\tools\init.ps1”
正在执行脚本文件“D:\Codes\dotNet\WebApiDemo\packages\EntityFramework.6.2.0\tools\install.ps1”

Type 'get-help EntityFramework' to see all available Entity Framework commands.
已将“EntityFramework 6.2.0”成功安装到 ProductApp
PM> 
```

<a id="markdown-startup类" name="startup类"></a>
### StartUp类

在项目根目录下添加Owin“Startup”类

```cs
// Owin启动项类
[assembly: OwinStartup(typeof(ProductApp.Startup))]
namespace ProductApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(20),//过期时间
                Provider = new SimpleAuthorizationServerProvider()
            };
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
```

使用Owin的Setup类，就不需要MVC的Global类了，删除之。

<a id="markdown-添加验证类" name="添加验证类"></a>
### 添加验证类

在项目下添加 SimpleAuthorizationServerProvider 类，用于用户的授权认证。

```cs
public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
{
    public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
    {
        context.Validated();
    }

    public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
    {

        context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

        //模拟登陆验证未通过
        if (!context.UserName.Equals("admin") || !context.Password.Equals("123"))
        {
            context.SetError("验证失败", "用户名或密码错误");
            return;
        }


        var identity = new ClaimsIdentity(context.Options.AuthenticationType);
        identity.AddClaim(new Claim("sub", context.UserName));
        identity.AddClaim(new Claim("role", "user"));

        context.Validated(identity);
    }
}
```

在ASP.NET Web API中启用OAuth的Access Token验证非常简单，只需在相应的Controller或Action加上[Authorize]标记

<a id="markdown-authorize授权" name="authorize授权"></a>
### Authorize授权

针对action增加Authorize特性

```cs
[Authorize]
public IEnumerable<Product> GetAllProducts()
{
    return products;
}
```

增加验证后，通过PostMan模拟请求，被服务器拒绝授权：

![](../assets/webapi/get_all_no_token.png)

需要先通过授权拿到token令牌，请求~/token，并附加username和password信息获取token

```cs
TokenEndpointPath = new PathString("/token"),
```

![](../assets/webapi/get_token.png)

如上图所示，通过验证获取服务器生成的 【access_token】，有效时间在【StartUp】类【ConfigureOAuth】方法配置

在新的请求header中附加token信息，在Headers中新增【Authorization】，值为：【bearer token】

```
Authorization:bearer Gqq4cdp4E3MxX0GFWnnqMG_Qs6csz7v7....
```


![](../assets/webapi/get_all_by_token.png)


根据指定id返回数据也是同样的道理：

![](../assets/webapi/get_one_by_token.png)

这样我们就完成了简单的WEB API的token验证

<a id="markdown-前端html请求" name="前端html请求"></a>
### 前端html请求

构造前端html页面如下：
```html
<button id="btnAllProduct">获取数据</button>
<button id="btnToken">登陆授权</button>
<button id="btnGetByToken">附加token的请求</button>
<div style="color:green;" id="suc_div"></div>
<div style="color:red;" id="err_div"></div>
```

模拟ajax未附加token的请求如下：
```js
// Headers未附加token令牌的请求
$('#btnAllProduct').click(function () {
    $.ajax({
        url: 'http://localhost:10034/api/Products/GetAllProducts',
        type: 'POST',
        dataType: 'json',
        success: function (resp) {
        },
        error: function (err) {
            $('#err_div').append(err.status + err.responseText);
            $('#err_div').append('<br/>');
        }
    });
});
```

无访问授权，返回401

![](../assets/webapi/ajax_401.png)

模拟用户登录授权，返回token令牌，保存至浏览器localStorage
```js
// 密码验证授权，获取token令牌
$('#btnToken').click(function () {
    $.ajax({
        url: 'http://localhost:10034/token',
        type: 'POST',
        dataType: 'json',
        data: {
            grant_type: 'password',
            username: 'admin',
            password: '123'
        },
        success: function (resp) {
            // 获取服务器返回的token并保存至localStorage
            localStorage.access_token = resp.access_token;
            $('#suc_div').append('access_token:' + resp.access_token);
            $('#suc_div').append('<br/>');
        },
        error: function (err) {
            $('#err_div').append(err.status + err.responseText);
        }
    });
});
```

验证通过，服务器返回token信息：

![](../assets/webapi/ajax_get_token.png)

使用获取的token令牌，添加至Headers部分，重新请求数据：

```js
// Headers附加token请求数据
$('#btnGetByToken').click(function () {
    $.ajax({
        url: 'http://localhost:10034/api/Products/GetAllProducts',
        type: 'POST',
        headers: {
            Authorization: 'bearer ' + localStorage.access_token//bearer后面有个空格！！！
        },
        dataType: 'json',
        success: function (resp) {
            $.each(resp, function (i, t) {
                $('#suc_div').append(`Id:${t.Id},Name:${t.Name},Category:${t.Category}`);
                $('#suc_div').append('<br/>');
            });
        },
        error: function (err) {
            $('#err_div').append(err.status + err.responseText);
            $('#suc_div').append('<br/>');
        }
    });
});
```

![](../assets/webapi/ajax_get_by_token.png)

通过ajax请求附加token信息，除了上面使用的Headers配置，还可以使用beforeSend回调进行设置：

```js
function GetDateForServiceCustomer(userId) {
    $.ajax({
        url: 'http://*******/api',
        data: {
        },
        beforeSend: function(request) {
            request.setRequestHeader("Authorization", token);
        },
        dataType: 'JSON',
        type: 'GET',
        success: function (resp) {
        },
        error: function () {
        }
    });
}
```

以上，完成了简单的WebApi身份验证。

---

> https://www.cnblogs.com/guyun/p/4589115.html

> https://docs.microsoft.com/zh-cn/aspnet/web-api/overview/getting-started-with-aspnet-web-api/tutorial-your-first-web-api

webapi 用户验证

> https://www.cnblogs.com/landeanfen/p/5287064.html#_label3_0

webapi token

> https://www.cnblogs.com/relax/p/4956441.html