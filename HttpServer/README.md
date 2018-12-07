<!-- TOC -->

- [SOA](#soa)
    - [定义](#定义)
    - [如何实现SOA](#如何实现soa)
    - [WebService](#webservice)
    - [WCF](#wcf)

<!-- /TOC -->
<a id="markdown-soa" name="soa"></a>
# SOA

<a id="markdown-定义" name="定义"></a>
## 定义
关于SOA的定义，目前主要有以下三个：
1. W3C的定义：SOA是一种应用程序架构，在这种架构中，所有功能都定义为独立的服务，这些服务带有定义明确的可调用接口，能够以定义好的顺序调用这些服务来形成业务流程。
2. Service-architecture.com的定义：服务是精确定义、封装完善、独立于其他服务所处环境和状态的函数。SOA本质上是服务的集合，服务之间彼此通信，这种通信可能是简单的数据传送，也可能是两个或更多的服务协调进行某些活动。服务之间需要某些方法进行连接。
3. Gartner的定义：SOA是一种C/S架构的软件设计方法，应用由服务和服务使用者组成，SOA与大多数通用的C/S架构模型不同之处，在于它着重强调构件的松散耦合，并使用独立的标准接口。

SOA分为广义的SOA和狭义的SOA。

广义的SOA是指一种新的企业应用架构和企业IT基础架构，它可以使企业实现跨应用，跨部门，跨企业甚至跨 行业之间的离散系统实现互连。（注意：这里所指的服务并不单单是Web Service,它可以是以Web Service实现 ，也可以以业务方式实现，甚至是书面口头承诺实现）。

而狭义的SOA是指一种软件架构，它可以根据需求通过网络对松散耦合的粗粒度应用组件进行分布式部署、组合和使用。服务层是SOA的基础，可以直接被应用调用，从而有效控制系统中与软件代理交互的人为依赖性。

<a id="markdown-如何实现soa" name="如何实现soa"></a>
## 如何实现SOA
目前Web Service越来越流行，并成为实现SOA的一种手段。

Web Service使应用功能通过标准化接口（WSDL）提供，使用标准化语言（XML）进行描述，并可基于标准化传输方式（HTTP和JMS）、采用标准化 协议（SOAP）进行调用，并使用XML SCHEMA方式对数据进行描述。

你也可以不采用Web服务来创建SOA应用，但是这种标准的重要性日益增加、应用日趋普遍。

<a id="markdown-webservice" name="webservice"></a>
## WebService
1.Web Service是跨平台的，应用程序经常需要从运行在IBM主机上的程序中获取数据，然后把数据发送到主机或UNIX应用程序中去。即使在同一个平台上， 不同软件厂商生产的各种软件也常常需要集成起来。通过WebService，应用程序可以用标准的方法把功能和数据“暴露”出来，供其它应用程序使用。
2. Web Service是无语言限制的，你可以使用.NET,JAVA,PHP,VB......等多种语言开发并进行相互调用。
3. 使用SOAP时数据是以ASCII文本的方式传输，调用很方便，数据容易通过防火墙而实现无缝连接。

<a id="markdown-wcf" name="wcf"></a>
## WCF
WCF是微软为了实现各个开发平台之间的无疑缝连接而开发一种崭新工具，它是为分布式处理而开发。WCF将DCOM、Remoting、Web Service、WSE、MSMQ、AJAX服务、TCP开发集成在一起，从而降低了分布式系统开发者的学习曲线，并统一了开发标准。

1. 开发的统一性。WCF是对于ASMX， Remoting，Enterprise Service，WSE，MSMQ，TCP开发等技术的整合。WCF是由托管代码编写，无论你是使用TCP通讯，Remoting通讯还是Web Service ，我们都可以使用统一的模式进行开发，利用WCF来创建面向服务的应用程序。
2. WCF能够实现多方互操作。它是使用 SOAP通信机制，这就保证了系统之间的互操作性，即使是运行不同开发语言，也可以跨进程、跨机器甚至于跨平台的通信。例如：使用J2EE的服务器(如WebSphere，WebLogic)，应用程序可以在Windows操作系统进行调用，也可以运行在其他的 操作系统，如Sun Solaris，HP Unix，Linux等等。
3. 提供高效的安全与可信赖度，它可以使用不同的安全认证将WS-Security，WS-Trust和WS-SecureConversation等添加到SOAP消息中。在SOAP的header中增加了WS-ReliableMessaging允许 可信赖的端对端通信。而建立在WS-Coordination和WS-AtomicTransaction之上的基于SOAP格式交换的信息，则支持两阶段的事务提交(two-phase commit transactions)。
4. WCF支持多支消息交换模式，如请求-应答，单工，双工等等。另外WCF还支持对等网——利用啮合网络址，客户端能在没有中心控制的情况下找到彼此并实现相互通信。

总的来说，WCF是实现SOA的的一个优秀选择，利用WCF能够实现跨平台，跨语言的无缝连接，从而实现Web服务的相互调用。

---

参考引用：

[SOA概念](http://www.cnblogs.com/leslies2/archive/2011/01/26/1934162.html)

