<!-- TOC -->

- [WebAPI](#webapi)
    - [概念](#概念)
        - [什么是WebAPI](#什么是webapi)
        - [为什么用WebAPI](#为什么用webapi)
        - [功能简介](#功能简介)
        - [Web API vs MVC](#web-api-vs-mvc)
        - [Web API vs WCF](#web-api-vs-wcf)
    - [实际操作](#实际操作)
        - [创建项目](#创建项目)
        - [创建模型和控制器](#创建模型和控制器)

<!-- /TOC -->

<a id="markdown-webapi" name="webapi"></a>
# WebAPI

Web API是一个比较宽泛的概念。这里我们提到Web API特指ASP.NET Web API。

这篇文章中我们主要介绍Web API的主要功能以及与其他同类型框架的对比，最后通过一些相对复杂的实例展示如何通过Web API构建http服务，同时也展示了Visual Studio构建.net项目的各种强大。

<a id="markdown-概念" name="概念"></a>
## 概念

<a id="markdown-什么是webapi" name="什么是webapi"></a>
### 什么是WebAPI
官方定义如下，强调两个关键点，即可以对接各种客户端（浏览器，移动设备），构建http服务的框架。

> ASP.NET Web API is a framework that makes it easy to build HTTP services that reach a broad range of clients, including browsers and mobile devices. ASP.NET Web API is an ideal platform for building RESTful applications on the .NET Framework.


Web API在ASP.NET完整框架中地位如下图，与SignalR一起同为构建Service的框架。Web API负责构建http常规服务，而SingalR主要负责的是构建实时服务，例如股票，聊天室，在线游戏等实时性要求比较高的服务。

<a id="markdown-为什么用webapi" name="为什么用webapi"></a>
### 为什么用WebAPI
Web API最重要的是可以构建面向各种客户端的服务。另外与WCF REST Service不同在于，Web API利用Http协议的各个方面来表达服务(例如 URI/request response header/caching/versioning/content format)，因此就省掉很多配置。

![](..\assets\webapi\webapi-1.png)

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

![](..\assets\webapi\webapi-vs-mvc.png)

详细点说他们的区别：
* MVC主要用来构建网站，既关心数据也关心页面展示，而Web API只关注数据
* Web API支持格式协商，客户端可以通过Accept header通知服务器期望的格式
* Web API支持Self Host，MVC目前不支持
* Web API通过不同的http verb表达不同的动作(CRUD)，MVC则通过Action名字表达动作
* Web API内建于ASP.NET System.Web.Http命名空间下，MVC位于System.Web.Mvc命名空间下，因此model binding/filter/routing等功能有所不同
* 最后，Web API非常适合构建移动客户端服务

<a id="markdown-web-api-vs-wcf" name="web-api-vs-wcf"></a>
### Web API vs WCF
ASP.NET网站上有很多简单的Web API实例，看看贴图和实例代码你就明白怎么用了。这里我们通过一个稍微复杂一点的实例来展示下Web API的功能。

<a id="markdown-实际操作" name="实际操作"></a>
## 实际操作
<a id="markdown-创建项目" name="创建项目"></a>
### 创建项目
在模板窗格中，选择已安装的模板展开Visual C# 节点。 下Visual C#，选择Web。 在项目模板列表中选择ASP.NET Web 应用程序。 命名项目"ProductsApp"，然后单击确定。
![](..\assets\webapi\webapi-create.jpg)

在中新建 ASP.NET 项目对话框中，选择空模板。 下"添加文件夹和核心引用"，检查Web API。 单击 “确定”。

![](..\assets\webapi\webapi-create2.jpg)

<a id="markdown-创建模型和控制器" name="创建模型和控制器"></a>
### 创建模型和控制器
新建Model：

![](..\assets\webapi\webapi-create-prod1.jpg)

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

新建控制器,在添加基架对话框中，选择Web API 控制器-空，如图：
![](..\assets\webapi\webapi-create-controller1.jpg)

将控制器命名"ProductsController"。修改控制器内容如下：

![](..\assets\webapi\webapi-create-controller2.jpg)

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

选择Web节点下的Visual C#，然后选择HTML 页项。 将该页命名为"index.html"。

![](..\assets\webapi\webapi-create-proj.jpg)

```html
<!DOCTYPE html>
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

















































> https://www.cnblogs.com/guyun/p/4589115.html

